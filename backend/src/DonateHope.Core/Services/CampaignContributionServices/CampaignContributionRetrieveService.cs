using DonateHope.Core.DTOs.CampaignContributionDTOs;
using DonateHope.Core.Errors;
using DonateHope.Core.Mappers;
using DonateHope.Core.ServiceContracts.CampaignContributionsServiceContracts;
using DonateHope.Domain.RepositoryContracts;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace DonateHope.Core.Services.CampaignContributionServices;

public class CampaignContributionRetrieveService(
    ILogger<CampaignContributionRetrieveService> logger,
    ICampaignContributionsRepository campaignContributionsRepository,
    CampaignContributionMapper campaignContributionMapper
    ) : ICampaignContributionRetrieveService
{
    private readonly ILogger<CampaignContributionRetrieveService> _logger = logger;
    private readonly ICampaignContributionsRepository _campaignContributionsRepository = campaignContributionsRepository;
    private readonly CampaignContributionMapper _campaignContributionMapper = campaignContributionMapper;

    public async Task<Result<CampaignContributionGetResponseDto>> GetCampaignContributionByIdAsync(Guid campaignContributionId)
    { 
        var campaignContributionResult = await _campaignContributionsRepository.GetCampaignContributionById(campaignContributionId);
        if (campaignContributionResult.IsFailed)
        {
            _logger.LogWarning(
                "Failed to retrieve campaign contribution {CampaignContributionId}. Error: {ErrorMessage}", 
                campaignContributionId,
                campaignContributionResult.Errors.First().Message
                );
            return new ProblemDetailsError(campaignContributionResult.Errors.First().Message);
        }
        
        _logger.LogInformation("Successfully retrieved campaign contribution {CampaignContributionId}", campaignContributionId);
        return _campaignContributionMapper.MapCampaignContributionToCampaignContributionGetResponseDto(campaignContributionResult.Value);;
    }

    public async Task<Result<IEnumerable<CampaignContributionGetResponseDto>>> GetCampaignContributionsByCampaignIdAsync(
        Guid campaignId)
    {
        var campaignContributionsResult = await _campaignContributionsRepository.GetCampaignContributionsByCampaignId(campaignId);
        if (campaignContributionsResult.IsFailed)
        {
            _logger.LogWarning(
                "Failed to retrieve campaign contributions for campaign {campaignId}. Error: {ErrorMessage}",
                campaignId,
                campaignContributionsResult.Errors.First().Message
                );
            return new ProblemDetailsError(campaignContributionsResult.Errors.First().Message);
        }

        var campaignContributionDtos = campaignContributionsResult.Value
            .Select(_campaignContributionMapper.MapCampaignContributionToCampaignContributionGetResponseDto)
            .ToArray();
        
        _logger.LogInformation("Successfully retrieved {Count} contributions for campaign {campaignId}",
            campaignContributionDtos.Length,
            campaignId);
        
        return campaignContributionDtos;
    }
}