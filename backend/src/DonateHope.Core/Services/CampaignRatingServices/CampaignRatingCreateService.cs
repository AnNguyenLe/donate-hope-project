using DonateHope.Core.DTOs.CampaignRatingDTOs;
using DonateHope.Core.Errors;
using DonateHope.Core.Mappers;
using DonateHope.Core.ServiceContracts.CampaignRatingsServiceContracts;
using DonateHope.Core.Services.CampaignContributionServices;
using DonateHope.Domain.EntityExtensions;
using DonateHope.Domain.RepositoryContracts;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace DonateHope.Core.Services.CampaignRatingServices;

public class CampaignRatingCreateService(
    ILogger<CampaignContributionCreateService> logger,
    ICampaignRatingsRepository campaignRatingsRepository,
    ICampaignsRepository campaignsRepository,
    CampaignRatingMapper campaignRatingMapper
    ) : ICampaignRatingCreateService
{
    private readonly ILogger<CampaignContributionCreateService> _logger = logger;
    private readonly ICampaignRatingsRepository _campaignRatingsRepository = campaignRatingsRepository;
    private readonly ICampaignsRepository _campaignsRepository = campaignsRepository;
    private readonly CampaignRatingMapper _campaignRatingMapper = campaignRatingMapper;

    public async Task<Result<CampaignRatingGetResponseDto>> CreateCampaignRatingAsync(
        CampaignRatingCreateRequestDto campaignRatingCreateRequestDto,
        Guid userId
    )
    {
        var campaignRating = _campaignRatingMapper
            .MapCampaignRatingCreateRequestDtoToCampaignRating(campaignRatingCreateRequestDto)
            .OnCampaignRatingCreating(userId);
        
        var campaign = await _campaignsRepository.GetCampaignById(campaignRating.CampaignId);
        if (campaign.ValueOrDefault is null)
        {
            _logger.LogWarning("Campaign not found for Id: {CampaignId}", campaignRating.CampaignId);
            return new ProblemDetailsError("Campaign not found.");
        }
        
        var queryResult = await _campaignRatingsRepository.AddCampaignRating(campaignRating);
        if (queryResult.IsFailed)
        {
            _logger.LogWarning(
                "Failed to create campaign rating {CampaignRatingId}. Error: {ErrorMessage}", 
                campaignRating.Id,
                queryResult.Errors.First().Message
                );
            return new ProblemDetailsError(
                "Unexpected error(s) during the campaign rating creating process. Please contact support team."
            );
        }
        
        var totalAffectedRows = queryResult.ValueOrDefault;
        if (totalAffectedRows == 0)
        {
            _logger.LogWarning("No row affected when attempting to create campaign rating for campaignId: {CampaignId}", campaignRating.CampaignId);
            return new ProblemDetailsError("Failed to create campaign rating.");
        }
        
        _logger.LogInformation(
            "Successfully created campaign contribution {CampaignContributionId} for campaign {CampaignId}", 
            campaignRating.Id,
            campaignRating.CampaignId
            );
        return _campaignRatingMapper.MapCampaignRatingToCampaignRatingGetResponseDto(campaignRating);
    }
}