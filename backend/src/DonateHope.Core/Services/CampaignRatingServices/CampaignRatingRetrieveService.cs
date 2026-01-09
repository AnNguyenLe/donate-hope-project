using DonateHope.Core.DTOs.CampaignRatingDTOs;
using DonateHope.Core.Errors;
using DonateHope.Core.Mappers;
using DonateHope.Core.ServiceContracts.CampaignRatingsServiceContracts;
using DonateHope.Domain.RepositoryContracts;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace DonateHope.Core.Services.CampaignRatingServices;

public class CampaignRatingRetrieveService(
    ILogger<CampaignRatingRetrieveService> logger,
    ICampaignRatingsRepository campaignRatingsRepository,
    CampaignRatingMapper campaignRatingMapper
    ) : ICampaignRatingRetrieveService
{
    private readonly ILogger<CampaignRatingRetrieveService> _logger = logger;
    private readonly ICampaignRatingsRepository _campaignRatingsRepository = campaignRatingsRepository;
    private readonly CampaignRatingMapper _campaignRatingMapper = campaignRatingMapper;

    public async Task<Result<CampaignRatingGetResponseDto>> GetCampaignRatingByIdAsync(Guid campaignRatingId)
    {
      
        var campaignRatingResult = await _campaignRatingsRepository.GetCampaignRatingById(campaignRatingId);
        if (campaignRatingResult.IsFailed)
        {
            _logger.LogWarning(
                "Failed to retrieve campaign rating {CampaignRatingId}. Error: {ErrorMessage}", 
                campaignRatingId,
                campaignRatingResult.Errors.First().Message
            );
            return new ProblemDetailsError(campaignRatingResult.Errors.First().Message);
        }
        
        _logger.LogInformation("Successfully retrieved campaign rating {CampaignRatingId}", campaignRatingId);
        return _campaignRatingMapper.MapCampaignRatingToCampaignRatingGetResponseDto(campaignRatingResult.Value);
    }
}