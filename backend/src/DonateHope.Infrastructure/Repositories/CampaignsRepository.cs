using System.Linq.Expressions;
using System.Text.Json;
using Dapper;
using DonateHope.Core.Errors;
using DonateHope.Domain.Entities;
using DonateHope.Domain.RepositoryContracts;
using DonateHope.Infrastructure.Data;
using DonateHope.Infrastructure.DbContext;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace DonateHope.Infrastructure.Repositories;

public class CampaignsRepository(
    IDbConnectionFactory dbConnectionFactory,
    ApplicationDbContext applicationDbContext
) : ICampaignsRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;
    private readonly ApplicationDbContext _dbContext = applicationDbContext;

    public async Task<Result<int>> AddCampaign(Campaign campaign)
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();
        var sqlCommand = """
                INSERT INTO campaigns 
                    (
                        id, 
                        title, 
                        subtitle, 
                        summary, 
                        description, 
                        goal_amount,
                        achieved_amount,
                        unit_of_measurement,
                        goal_status,
                        expecting_start_date, 
                        expecting_end_date, 
                        number_of_ratings,
                        average_rating_point,
                        spending_amount,
                        proofs_url,
                        is_published,
                        created_at, 
                        updated_at,
                        created_by, 
                        updated_by, 
                        is_active,
                        is_deleted,
                        user_id
                    )
                VALUES 
                    (
                        @Id, 
                        @Title, 
                        @Subtitle, 
                        @Summary, 
                        @Description, 
                        @GoalAmount,
                        @AchievedAmount,
                        @UnitOfMeasurement, 
                        @GoalStatus,
                        @ExpectingStartDate, 
                        @ExpectingEndDate,
                        @NumberOfRatings,
                        @AverageRatingPoint,
                        @SpendingAmount,
                        @ProofsUrl,
                        @IsPublished,
                        @CreatedAt,
                        @UpdatedAt,
                        @CreatedBy,
                        @UpdatedBy,
                        @IsActive,
                        @IsDeleted,
                        @UserId
                    );
            """;

        var totalAffectedRows = await dbConnection.ExecuteAsync(sqlCommand, campaign);

        if (totalAffectedRows == 0)
        {
            return new ProblemDetailsError("Add new campaign failed.");
        }

        return totalAffectedRows;
    }

    public async Task<Result<int>> DeleteCampaign(
        Guid campaignId,
        Guid deletedBy,
        string reasonForDeletion
    )
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();

        var sqlCommand = """
                UPDATE campaigns
                SET
                    is_deleted = @isDeleted,
                    deleted_at = @deletedAt,
                    deleted_by = @deletedBy,
                    is_active = @isActive,
                    active_status_note = @activeStatusNote
                WHERE id = @campaignId
            """;

        var totalAffectedRows = await dbConnection.ExecuteAsync(
            sqlCommand,
            new
            {
                isDeleted = true,
                deletedAt = DateTime.UtcNow,
                deletedBy,
                isActive = false,
                activeStatusNote = reasonForDeletion,
                campaignId
            }
        );

        if (totalAffectedRows == 0)
        {
            return new ProblemDetailsError("Something wrong trying to delete this record.");
        }

        return totalAffectedRows;
    }

    /// <summary>
    /// USING THIS WITH CAUTION! Your data will be deleted permanently and will not be able to recoveredz!
    /// </summary>
    public async Task<Result<int>> DeleteCampaignPermanently(Guid campaignId)
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();

        var sqlCommand = """
                DELETE campaigns
                WHERE id = @campaignId;
            """;

        var totalAffectedRows = await dbConnection.ExecuteAsync(sqlCommand, new { campaignId });

        if (totalAffectedRows == 0)
        {
            return new ProblemDetailsError(
                $"Unable to permanently delete campaign with ID: {campaignId}"
            );
        }

        return totalAffectedRows;
    }

    public async Task<Result<List<Campaign>>> GetCampaigns(
        Expression<Func<Campaign, bool>> predicate
    )
    {
        return await _dbContext
            .Campaigns.Where(predicate)
            .OrderByDescending(campaign => campaign.CreatedAt)
            .ToListAsync();
    }

    public async Task<Result<List<Campaign>>> GetCampaigns()
    {
        return await _dbContext.Campaigns.ToListAsync();
    }

    public async Task<Result<Campaign>> GetCampaignById(Guid campaignId)
    {
        var queryResult = await _dbContext
            .Campaigns.Where(c => c.Id == campaignId)
            .FirstOrDefaultAsync();

        if (queryResult is null)
        {
            return new ProblemDetailsError($"Campaign with ID: {campaignId} not found.");
        }

        return queryResult;
    }

    public async Task<Result<int>> UpdateCampaign(Campaign updatedCampaign)
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();

        var sqlCommand = """
                UPDATE campaigns
                SET
                    title = @Title, 
                    subtitle = @Subtitle, 
                    summary = @Summary,
                    description = @Description, 
                    goal_amount = @GoalAmount,
                    unit_of_measurement = @UnitOfMeasurement,
                    spending_amount = @SpendingAmount,
                    expecting_start_date = @ExpectingStartDate,
                    expecting_end_date = @ExpectingEndDate,
                    proofs_url = @ProofsUrl,
                    updated_at = @UpdatedAt,
                    updated_by = @UpdatedBy
                WHERE
                    id = @Id;
            """;

        var totalAffectedRows = await dbConnection.ExecuteAsync(sqlCommand, updatedCampaign);

        if (totalAffectedRows == 0)
        {
            return new ProblemDetailsError("Unable to update the campaign.");
        }

        return totalAffectedRows;
    }
}
