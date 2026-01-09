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


public class CampaignCommentsRepository(IDbConnectionFactory dbConnectionFactory, ApplicationDbContext applicationDbContext) : ICampaignCommentsRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;
    private readonly ApplicationDbContext _dbContext = applicationDbContext;

    public async Task<int> AddCampaignComment(CampaignComment campaignComment)
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();
        var sqlCommand = """
                INSERT INTO campaign_comments 
                    (
                        id,
                        content, 
                        created_at, 
                        updated_at, 
                        created_by, 
                        updated_by,
                        is_deleted,
                        deleted_at,
                        deleted_by,
                        is_banned, 
                        user_id,
                        campaign_id
                    )
                VALUES 
                    (
                        @Id,
                        @Content,  
                        @CreatedAt,     
                        @UpdatedAt,
                        @CreatedBy,
                        @UpdatedBy,
                        @IsDeleted,
                        @DeletedAt,
                        @DeletedBy,
                        @IsBanned,
                        @UserId,
                        @CampaignId
                    );
            """;

        return await dbConnection.ExecuteAsync(sqlCommand, campaignComment);
    }

    public async Task<(IEnumerable<CampaignComment> Comments, int TotalCount)> GetCommentsByCampaignId(Guid campaignId, int page = 1, int pageSize = 6)
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();

        var offset = (page - 1) * pageSize;

        var sqlCommand = """
    SELECT 
        c.id,
        c.content, 
        c.created_at AS CreatedAt, 
        c.updated_at AS UpdatedAt, 
        c.created_by AS CreatedBy, 
        c.updated_by AS UpdatedBy,
        c.is_deleted AS IsDeleted,
        c.deleted_at AS DeletedAt,
        c.deleted_by AS DeletedBy,
        c.is_banned AS IsBanned, 
        c.user_id AS UserId,
        c.campaign_id AS CampaignId,
        u.first_name AS FirstName,
        u.last_name AS LastName
    FROM 
        campaign_comments c
    JOIN 
        app_users u ON c.user_id = u.id
    WHERE 
        c.campaign_id = @CampaignId
        AND c.is_deleted = false
    ORDER BY 
        c.created_at DESC
    LIMIT @PageSize OFFSET @Offset;
    """;

        var result = await dbConnection.QueryAsync(sqlCommand, new { CampaignId = campaignId, PageSize = pageSize, Offset = offset });

        var comments = result.Select(r => new CampaignComment
        {
            Id = r.id,
            Content = r.content,
            CreatedAt = r.createdat,
            UpdatedAt = r.updatedat,
            CreatedBy = r.createdby,
            UpdatedBy = r.updatedby,
            IsDeleted = r.isdeleted ?? false,
            DeletedAt = r.deletedat,
            DeletedBy = r.deletedby,
            IsBanned = r.isbanned ?? false,
            UserId = r.userid,
            CampaignId = r.campaignid,
            FirstName = r.firstname,
            LastName = r.lastname
        });

        var countSql = "SELECT COUNT(*) FROM campaign_comments WHERE campaign_id = @CampaignId AND is_deleted = false";
        var totalCount = await dbConnection.ExecuteScalarAsync<int>(countSql, new { CampaignId = campaignId });

        return (comments, totalCount);
    }

    public async Task<Result<CampaignComment>> GetCampaignCommentById(Guid campaignCommentId)
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();
        var queryResult = await _dbContext
            .CampaignComments.Where(cc => cc.Id == campaignCommentId)
            .FirstOrDefaultAsync();
        if (queryResult is null)
        {
            return new ProblemDetailsError($"Campaign Comment with ID: {campaignCommentId} not found.");
        }

        return queryResult;
    }

    public async Task<Result<int>> UpdateCampaignComment(CampaignComment updateCampaignComment)
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();
        var sqlCommand = """
                         UPDATE campaign_comments
                         SET
                             content = @Content,
                             updated_at = @UpdatedAt,
                             updated_by = @UpdatedBy
                         WHERE 
                             id = @Id;
                             
                         """;
        var totalAffectedRows = await dbConnection.ExecuteAsync(sqlCommand, updateCampaignComment);
        if (totalAffectedRows == 0)
        {
            return new ProblemDetailsError("Unable to update campaign_contribution.");
        }
        return totalAffectedRows;
    }


    public async Task<Result<int>> DeleteCampaignComment(
        Guid campaignCommentId, Guid deletedBy
        )
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();
        var sqlCommand = """
                         UPDATE campaign_comments
                         SET
                            is_deleted = @isDeleted,
                            deleted_at = @deletedAt,
                            deleted_by = @deletedBy
                         WHERE id = @campaignCommentId
                         """;
        var totalAffectedRows = await dbConnection.ExecuteAsync(
            sqlCommand,
            new
            {
                isDeleted = true,
                deletedAt = DateTime.UtcNow,
                deletedBy,
                campaignCommentId
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
    public async Task<Result<int>> DeleteCampaignCommentPermanently(Guid campaignCommentId)
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();
        var sqlCommand = """
                         DELETE campaign_comments
                         WHERE id = @campaignCommentId;
                         """;
        var totalAffectedRows = await dbConnection.ExecuteAsync(sqlCommand, new { campaignCommentId });
        if (totalAffectedRows == 0)
        {
            return new ProblemDetailsError(
                $"Unable to permanently delete campaign_contribution with ID: {campaignCommentId}"
            );
        }
        return totalAffectedRows;
    }


}
