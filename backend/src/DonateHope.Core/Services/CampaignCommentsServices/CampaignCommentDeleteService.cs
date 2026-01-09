using DonateHope.Core.DTOs.CampaignCommentDTOs;
using DonateHope.Core.Errors;
using DonateHope.Core.Mappers;
using DonateHope.Core.ServiceContracts.CampaignCommentServiceContracts;
using DonateHope.Domain.RepositoryContracts;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace DonateHope.Core.Services.CampaignCommentServices;

public class CampaignCommentDeleteService(
    ILogger<CampaignCommentDeleteService> logger,
    ICampaignCommentsRepository campaignCommentRepository,
    CampaignCommentMapper campaignCommentMapper
    ) : ICampaignCommentDeleteService
{
    private readonly ILogger<CampaignCommentDeleteService> _logger = logger;
    private readonly ICampaignCommentsRepository _campaignCommentsRepository = campaignCommentRepository;
    private readonly CampaignCommentMapper _campaignCommentMapper = campaignCommentMapper;

    public async Task<Result> DeleteCampaignCommentAsync(
        Guid campaignCommentId, Guid deletedBy
        )
    {
        var queryResult = await _campaignCommentsRepository.GetCampaignCommentById(campaignCommentId);
        if (queryResult.IsFailed || queryResult.ValueOrDefault is null)
        {
            _logger.LogWarning(
                "Failed to retrieve campaign Comment {CampaignCommentId}. Error: {ErrorMessage}",
                campaignCommentId,
                queryResult.Errors.First().Message
                );
            return new ProblemDetailsError(queryResult.Errors.First().Message);
        }

        var deletedCampaignComment = queryResult.Value;

        if (deletedCampaignComment.CreatedBy != deletedBy)
        {
            _logger.LogWarning(
                "Unauthorized attempt to delete campaign comment {CampaignCommentId} by user {UserId}",
                campaignCommentId, deletedBy
            );
            return new ProblemDetailsError("You do not have permission to delete this comment.");
        }
        if (deletedCampaignComment.IsDeleted)
        {
            _logger.LogWarning("The campaign comment {CampaignCommentId} is already marked as deleted.", deletedCampaignComment.Id);
            return new ProblemDetailsError("This campaign comment does not exist.");
        }

        var deletedResult = await _campaignCommentsRepository.DeleteCampaignComment(
            campaignCommentId, deletedBy
        );

        if (deletedResult.IsFailed)
        {
            _logger.LogWarning(
                "Failed to delete campaign comment {CampaignCommentId}. Error: {ErrorMessage}",
                campaignCommentId,
                deletedResult.Errors.First().Message
                );
            return new ProblemDetailsError("Failed to delete campaign comment.");
        }

        _logger.LogInformation(
            "Successfully deleted campaign comment {CampaignCommentId}", campaignCommentId);
        return Result.Ok();
    }
}