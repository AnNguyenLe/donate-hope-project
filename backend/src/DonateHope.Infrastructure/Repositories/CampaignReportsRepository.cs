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

public class CampaignReportsRepository(
    IDbConnectionFactory dbConnectionFactory,
    ApplicationDbContext applicationDbContext
    ) : ICampaignReportsRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;
    private readonly ApplicationDbContext _dbContext = applicationDbContext;
    public async Task<Result<int>> AddCampaignReport(CampaignReport campaignReport)
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();
        var sqlCommand = """
                         INSERT INTO campaign_reports
                             (
                                id,
                                from_date_time,
                                to_date_time,
                                title,
                                subtitle,
                                summary,
                                detail,
                                amount,
                                unit_of_measurement,
                                documents_url,
                                created_at,
                                updated_at,
                                created_by,
                                updated_by,
                                is_deleted,
                                campaign_id
                             )
                         VALUES
                             (
                                @Id,
                                @FromDateTime,
                                @ToDateTime,
                                @Title,
                                @Subtitle,
                                @Summary,
                                @Detail,
                                @Amount,
                                @UnitOfMeasurement,
                                @DocumentsUrl,
                                @CreatedAt,
                                @UpdatedAt,
                                @CreatedBy,
                                @UpdatedBy,
                                @IsDeleted,
                                @CampaignId
                             )
                         """;
        var totalAffectedRows = await dbConnection.ExecuteAsync(sqlCommand, campaignReport);
        if (totalAffectedRows == 0)
        {
            return new ProblemDetailsError("Failed to add campaign report.");
        }
        
        return totalAffectedRows;
    }

    public async Task<Result<int>> DeleteCampaignReport(
        Guid campaignReportId,
        Guid deletedBy,
        string reasonForDeletion
        )
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();
        var sqlCommand = """
                         UPDATE campaign_reports
                         SET
                            is_deleted = @isDeleted,
                            deleted_at = @deletedAt,
                            deleted_by = @deletedBy,
                            reason_for_deletion = @reasonForDeletion
                         WHERE id = @CampaignReportId
                         """;
        var totalAffectedRows = await dbConnection.ExecuteAsync(
            sqlCommand,
            new
            {
                isDeleted = true,
                deletedAt = DateTime.UtcNow,
                deletedBy,
                reasonForDeletion,
                CampaignReportId = campaignReportId
            });
        if (totalAffectedRows == 0)
        {
            return new ProblemDetailsError("Something wrong trying to delete this record.");
        }

        return totalAffectedRows;
    }
    
    /// <summary>
    /// USING THIS WITH CAUTION! Your data will be deleted permanently and will not be able to recovered!
    /// </summary>
    public async Task<Result<int>> DeleteCampaignReportPermanently(Guid campaignReportId)
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();
        var sqlCommand = """
                         DELETE campaign_reports
                         WHERE id = @CampaignReportId;
                         """;
        var totalAffectedRows = await dbConnection.ExecuteAsync(sqlCommand, new { CampaignReportId = campaignReportId });
        if (totalAffectedRows == 0)
        {
            return new ProblemDetailsError(
                $"Unable to permanently delete campaign_report with ID: {campaignReportId}"
            );
        }
        return totalAffectedRows;
    }

    public IQueryable<CampaignReport> GetCampaignReports(Expression<Func<CampaignReport, bool>> predicate)
    {
        return _dbContext.CampaignReports.Where(predicate);
    }

    public async Task<Result<CampaignReport>> GetCampaignReportById(Guid campaignReportId)
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();
        var queryResult = await _dbContext
            .CampaignReports.Where(cr => cr.Id == campaignReportId && cr.IsDeleted == false)
            .FirstOrDefaultAsync();
        if (queryResult is null)
        {
            return new ProblemDetailsError($"CampaignReport with ID: {campaignReportId} not found.");
        }

        return queryResult;
    }

    public async Task<Result<int>> UpdateCampaignReport(CampaignReport updateCampaignReport)
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();
        var sqlCommand = """
                         UPDATE campaign_reports
                         SET
                             from_date_time = @FromDateTime,
                             to_date_time = @ToDateTime,
                             title = @Title,
                             subtitle = @Subtitle,
                             summary = @Summary,
                             detail = @Detail,
                             amount = @Amount,
                             unit_of_measurement = @UnitOfMeasurement,
                             documents_url = @DocumentsUrl,
                             updated_at = @UpdatedAt,
                             updated_by = @UpdatedBy
                         WHERE 
                             id = @Id;
                         """;
        var totalAffectedRows = await dbConnection.ExecuteAsync(sqlCommand, updateCampaignReport);
        if (totalAffectedRows == 0)
        {
            return new ProblemDetailsError("Unable to update campaign_report.");
        }
        return totalAffectedRows;
    }
}