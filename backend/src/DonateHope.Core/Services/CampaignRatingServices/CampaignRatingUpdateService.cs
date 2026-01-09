using DonateHope.Core.DTOs.CampaignRatingDTOs;
using DonateHope.Core.Errors;
using DonateHope.Core.Mappers;
using DonateHope.Core.ServiceContracts.CampaignRatingsServiceContracts;
using DonateHope.Domain.RepositoryContracts;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace DonateHope.Core.Services.CampaignRatingServices;

public class CampaignRatingUpdateService (
    ILogger<CampaignRatingUpdateService> logger,
    CampaignRatingMapper campaignRatingMapper,
    ICampaignRatingsRepository campaignRatingsRepository
    ) : ICampaignRatingUpdateService
{
    private readonly ILogger<CampaignRatingUpdateService> _logger = logger;
    private readonly CampaignRatingMapper _campaignRatingMapper = campaignRatingMapper;
    private readonly ICampaignRatingsRepository _campaignRatingsRepository = campaignRatingsRepository;

    public async Task<Result<CampaignRatingGetResponseDto>> UpdateCampaignRatingAsync(
        CampaignRatingUpdateRequestDto updateRequestDto,
        Guid userId
    )
    {
        var queryResult = await _campaignRatingsRepository.GetCampaignRatingById(updateRequestDto.Id);

        if (queryResult.IsFailed || queryResult.ValueOrDefault is null)
        {
            _logger.LogWarning("Failed to retrieve campaign rating {CampaignRatingId}", updateRequestDto.Id);
            return new ProblemDetailsError("Campaign rating not found.");
        }

        var currentCampaignRating = queryResult.Value;
        
        if (userId != currentCampaignRating.UserId)
        {
            _logger.LogWarning(
                "User id {UserId} is unauthorized to update campaign rating {CampaignRatingId}",
                userId,
                updateRequestDto.Id
                );
            return new ProblemDetailsError("You are unauthorized to update this campaign rating.");
        }
        
        var updatedCampaignRating = _campaignRatingMapper.MapCampaignRatingUpdateRequestDtoToCampaignRating(updateRequestDto);
        updatedCampaignRating.CreatedAt = currentCampaignRating.CreatedAt;
        updatedCampaignRating.CreatedBy = currentCampaignRating.CreatedBy;
        updatedCampaignRating.CampaignId = currentCampaignRating.CampaignId;
        
        updatedCampaignRating.UpdatedAt = DateTime.UtcNow;
        updatedCampaignRating.UpdatedBy = userId;
        
        var updateResult = await _campaignRatingsRepository.UpdateCampaignRating(updatedCampaignRating);
        if (updateResult.IsFailed)
        {
            _logger.LogWarning(
                "Failed to update campaign rating {CampaignRatingId}, Error message: {ErrorMessage}",
                updateRequestDto.Id,
                updateResult.Errors.First().Message
            );
            return new ProblemDetailsError("Failed to update campaign rating.");
        }
        
        _logger.LogInformation("Successfully updated campaign rating {CampaignRating}", updateRequestDto.Id);
        return _campaignRatingMapper.MapCampaignRatingToCampaignRatingGetResponseDto(updatedCampaignRating);
    }
}