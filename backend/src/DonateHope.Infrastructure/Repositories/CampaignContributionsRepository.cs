using System.Linq.Expressions;
using Dapper;
using DonateHope.Core.Errors;
using DonateHope.Domain.Entities;
using DonateHope.Domain.RepositoryContracts;
using DonateHope.Infrastructure.Data;
using DonateHope.Infrastructure.DbContext;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace DonateHope.Infrastructure.Repositories;

public class CampaignContributionsRepository(
    IDbConnectionFactory dbConnectionFactory,
    ApplicationDbContext applicationDbContext
    ) : ICampaignContributionsRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;
    private readonly ApplicationDbContext _dbContext = applicationDbContext;
    public async Task<Result<int>> AddCampaignContribution(CampaignContribution campaignContribution)
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();
        using var transaction = dbConnection.BeginTransaction();
        
        var createCampaignContributionSqlCommand = """
                         INSERT INTO campaign_contributions
                             (
                                id,
                                amount,
                                unit_of_measurement,
                                contribution_method,
                                donator_name,
                                message,
                                created_at,
                                updated_at,
                                created_by,
                                updated_by,
                                is_deleted,
                                user_id,
                                campaign_id
                             )
                         VALUES
                             (
                                @Id,
                                @Amount,
                                @UnitOfMeasurement,
                                @ContributionMethod,
                                @DonatorName,
                                @Message,
                                @CreatedAt,
                                @UpdatedAt,
                                @CreatedBy,
                                @UpdatedBy,
                                @IsDeleted,
                                @UserId,
                                @CampaignId
                             )
                         """;
        var createCampaignContributionResult = await dbConnection.ExecuteAsync(createCampaignContributionSqlCommand, campaignContribution, transaction);
        if (createCampaignContributionResult == 0)
        {
            transaction.Rollback();
            return new ProblemDetailsError("Failed to add campaign contribution.");
        }
        
        var updateCampaignSqlCommand = """
                                    UPDATE campaigns
                                    SET achieved_amount = achieved_amount + @Amount
                                    WHERE id = @CampaignId;
                                    """;
        var updateCampaignResult = await dbConnection.ExecuteAsync(updateCampaignSqlCommand, 
            new
            {
                CampaignId = campaignContribution.CampaignId,
                Amount = campaignContribution.Amount,
            }, transaction);

        if (updateCampaignResult == 0)
        {
            transaction.Rollback();
            return new ProblemDetailsError("Failed to update campaign achieved amount.");
        }
        
        transaction.Commit();
        return createCampaignContributionResult;
    }

    public async Task<Result<int>> DeleteCampaignContribution(
        Guid campaignContributionId,
        Guid deletedBy,
        string reasonForDeletion
        )
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();
        using var transaction = dbConnection.BeginTransaction();
        
        var currentContribution = await dbConnection.QueryFirstOrDefaultAsync<CampaignContribution>(
            "SELECT amount, campaign_id as CampaignId FROM campaign_contributions WHERE id = @Id",
            new {Id = campaignContributionId},
            transaction
        );

        if (currentContribution == null)
        {
            return new ProblemDetailsError($"CampaignContribution with ID: {campaignContributionId} not found or already deleted.");
        }
        
        var deleteCampaignContributionSqlCommand = """
                         UPDATE campaign_contributions
                         SET
                            is_deleted = @isDeleted,
                            deleted_at = @deletedAt,
                            deleted_by = @deletedBy,
                            reason_for_deletion = @reasonForDeletion
                         WHERE id = @campaignContributionId
                         """;
        var deleteCampaignContributionResult = await dbConnection.ExecuteAsync(
            deleteCampaignContributionSqlCommand,
            new
            {
                isDeleted = true,
                deletedAt = DateTime.UtcNow,
                deletedBy,
                reasonForDeletion,
                campaignContributionId
            }, transaction);
        
        if (deleteCampaignContributionResult == 0)
        {
            transaction.Rollback();
            return new ProblemDetailsError("Something wrong trying to delete this record.");
        }
        
        var updateCampaignSqlCommand = """
                                       UPDATE campaigns
                                       SET achieved_amount = achieved_amount - @Amount
                                       WHERE id = @CampaignId;
                                       """;
        var updateCampaignResult = await dbConnection.ExecuteAsync(
            updateCampaignSqlCommand,
            new
                {
                    Amount = currentContribution.Amount,
                    CampaignId = currentContribution.CampaignId
                },transaction
            );
        
        if (updateCampaignResult == 0)
        {
            transaction.Rollback();
            return new ProblemDetailsError("Failed to update campaign achieved amount.");
        }

        transaction.Commit();
        return deleteCampaignContributionResult;
    }
    
    /// <summary>
    /// USING THIS WITH CAUTION! Your data will be deleted permanently and will not be able to recovered!
    /// </summary>
    public async Task<Result<int>> DeleteCampaignContributionPermanently(Guid campaignContributionId)
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();
        var sqlCommand = """
                         DELETE campaign_contributions
                         WHERE id = @campaignContributionId;
                         """;
        var totalAffectedRows = await dbConnection.ExecuteAsync(sqlCommand, new { campaignContributionId });
        if (totalAffectedRows == 0)
        {
            return new ProblemDetailsError(
                $"Unable to permanently delete campaign_contribution with ID: {campaignContributionId}"
            );
        }
        return totalAffectedRows;
    }

    public IQueryable<CampaignContribution> GetCampaignContributions(Expression<Func<CampaignContribution, bool>> predicate)
    {
        return _dbContext.CampaignContributions.Where(predicate);
    }

    public async Task<Result<CampaignContribution>> GetCampaignContributionById(Guid campaignContributionId)
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();
        var queryResult = await _dbContext
            .CampaignContributions.Where(cc => cc.Id == campaignContributionId && cc.IsDeleted == false)
            .FirstOrDefaultAsync();
        if (queryResult is null)
        {
            return new ProblemDetailsError($"CampaignContribution with ID: {campaignContributionId} not found.");
        }

        return queryResult;
    }

    public async Task<Result<int>> UpdateCampaignContribution(CampaignContribution updateCampaignContribution)
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();
        using var transaction = dbConnection.BeginTransaction();

        var currentContributionAmount = await dbConnection.QueryFirstOrDefaultAsync<CampaignContribution>(
            "SELECT amount FROM campaign_contributions WHERE id = @Id",
            new {Id = updateCampaignContribution.Id},
            transaction
        );
        if (currentContributionAmount == null)
        {
            return new ProblemDetailsError($"CampaignContribution with ID: {updateCampaignContribution.Id} not found or already deleted.");
        }
        
        var updateCampaignContributionCommand = """
                         UPDATE campaign_contributions
                         SET
                             amount = @Amount,
                             unit_of_measurement = @UnitOfMeasurement,
                             contribution_method = @ContributionMethod,
                             updated_at = @UpdatedAt,
                             updated_by = @UpdatedBy
                         WHERE 
                             id = @Id;
                         """;
        var updateCampaignContributionResult = await dbConnection.ExecuteAsync(updateCampaignContributionCommand, updateCampaignContribution, transaction);
        
        if (updateCampaignContributionResult == 0)
        {
            transaction.Rollback();
            return new ProblemDetailsError("Unable to update campaign_contribution.");
        }

        if (updateCampaignContribution.Amount != currentContributionAmount.Amount)
        {
            var currentAmount = currentContributionAmount.Amount;
            var updateCampaignCommand = """
                                        UPDATE campaigns
                                        SET achieved_amount = achieved_amount + @UpdateAmount - @CurrentAmount
                                        WHERE id = @CampaignId;
                                        """;
            var updateCampaignResult = await dbConnection.ExecuteAsync(updateCampaignCommand,
                new
                {
                    UpdateAmount = updateCampaignContribution.Amount,
                    CurrentAmount = currentContributionAmount.Amount,
                    CampaignId = updateCampaignContribution.CampaignId
                }, transaction);
            
            if (updateCampaignResult == 0)
            {
                transaction.Rollback();
                return new ProblemDetailsError("Failed to update campaign achieved amount.");
            }
        }
        
        transaction.Commit();
        return updateCampaignContributionResult;
    }

    public async Task<Result<IEnumerable<CampaignContribution>>> GetCampaignContributionsByCampaignId(Guid campaignId)
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();
        var sqlCommand = """
                         SELECT
                             cc.id AS Id,
                             cc.campaign_id AS CampaignId,
                             CASE
                                 WHEN cc.donator_name IS NULL OR cc.donator_name = '' THEN au.first_name || ' ' || au.last_name
                                 ELSE cc.donator_name
                             END AS DonatorName,
                             CASE
                                 WHEN cc.message IS NULL OR cc.message = '' THEN 'No message provided.'
                                 ELSE cc.message
                             END AS Message,
                             cc.contribution_method AS ContributionMethod,
                             cc.unit_of_measurement AS UnitOfMeasurement,
                             cc.amount AS Amount,
                             cc.created_at AS CreatedAt,
                             cc.created_by AS CreatedBy,
                             cc.updated_at AS UpdatedAt,
                             cc.updated_by AS UpdatedBy
                         FROM campaign_contributions cc
                         JOIN app_users au ON cc.user_id = au.id
                         WHERE cc.campaign_id = @CampaignId;
                         
                         """;
        var campaignContributions = (await dbConnection.QueryAsync<CampaignContribution>(
            sqlCommand,
            new { CampaignId = campaignId }
        )).ToArray();
        
        if (campaignContributions.Length == 0)
        {
            return new ProblemDetailsError($"No contributions found for campaign ID: {campaignId}");
        }
        return campaignContributions;
    }
}