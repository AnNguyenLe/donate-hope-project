using DonateHope.Core.DTOs.CampaignCommentDTOs;
using DonateHope.Core.Errors;
using DonateHope.Core.Mappers;
using DonateHope.Core.ServiceContracts.CampaignCommentServiceContracts;
using DonateHope.Domain.RepositoryContracts;
using Microsoft.Extensions.Logging;
using FluentResults;

namespace DonateHope.Core.Services.CampaignCommentsServices;

public class CampaignCommentUpdateService(
    ICampaignCommentsRepository campaignCommentsRepository,
    ILogger<CampaignCommentUpdateService> logger,
    CampaignCommentMapper campaignCommentMapper
) : ICampaignCommentUpdateService
{
    private readonly ILogger<CampaignCommentUpdateService> _logger = logger;
    private readonly CampaignCommentMapper _campaignCommentMapper = campaignCommentMapper;
    private readonly ICampaignCommentsRepository _campaignCommentsRepository = campaignCommentsRepository;

    public async Task<Result<CampaignCommentGetResponseDto>> UpdateCampaignCommentAsync(
        CampaignCommentUpdateRequestDto updateCommentRequestDto,
        Guid userId
    )
    {
        var queryResult = await _campaignCommentsRepository.GetCampaignCommentById(updateCommentRequestDto.Id);

        if (queryResult.IsFailed || queryResult.ValueOrDefault is null)
        {
            _logger.LogWarning("Failed to retrieve campaign comment {CampaignCommentId}", updateCommentRequestDto.Id);
            return new ProblemDetailsError("Campaign comment not found.");
        }

        var currentCampaignComment = queryResult.Value;
        // Check if the comment is deleted
        if (currentCampaignComment.IsDeleted)
        {
            _logger.LogWarning(
                "Attempt to update deleted campaign comment {CampaignCommentId}",
                updateCommentRequestDto.Id
            );
            return new ProblemDetailsError("Cannot update a deleted campaign comment.");
        }


        if (userId != currentCampaignComment.UserId)
        {
            _logger.LogWarning(
                "User id {UserId} is unauthorized to update campaign contribution {CampaignCommentId}",
                userId,
                updateCommentRequestDto.Id
                );
            return new ProblemDetailsError("You are unauthorized to update this campaign comment.");
        }

        var updatedCampaignComment = _campaignCommentMapper.MapCampaignCommentUpdateRequestDtoToCampaignComment(
            updateCommentRequestDto
        );
       
        updatedCampaignComment.UpdatedAt = DateTime.UtcNow;
        updatedCampaignComment.UpdatedBy = userId;

        var updateResult = await _campaignCommentsRepository.UpdateCampaignComment(updatedCampaignComment);
        if (updateResult.IsFailed)
        {
            return new ProblemDetailsError("Failed to update campaign comment.");
        }

        return Result.Ok();

    }

}
