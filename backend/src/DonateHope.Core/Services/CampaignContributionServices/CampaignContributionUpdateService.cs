using DonateHope.Core.DTOs.CampaignContributionDTOs;
using DonateHope.Core.Errors;
using DonateHope.Core.Mappers;
using DonateHope.Core.ServiceContracts.CampaignContributionsServiceContracts;
using DonateHope.Domain.RepositoryContracts;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace DonateHope.Core.Services.CampaignContributionServices;

public class CampaignContributionUpdateService (
    ILogger<CampaignContributionUpdateService> logger,
    CampaignContributionMapper campaignContributionMapper,
    ICampaignContributionsRepository campaignContributionsRepository
    ) : ICampaignContributionUpdateService
{
    private readonly ILogger<CampaignContributionUpdateService> _logger = logger;
    private readonly CampaignContributionMapper _campaignContributionMapper = campaignContributionMapper;
    private readonly ICampaignContributionsRepository _campaignContributionsRepository = campaignContributionsRepository;

    public async Task<Result<CampaignContributionGetResponseDto>> UpdateCampaignContributionAsync(
        CampaignContributionUpdateRequestDto updateRequestDto,
        Guid userId
    )
    {
        var queryResult = await _campaignContributionsRepository.GetCampaignContributionById(updateRequestDto.Id);

        if (queryResult.IsFailed || queryResult.ValueOrDefault is null)
        {
            _logger.LogWarning("Failed to retrieve campaign contribution {CampaignContributionId}", updateRequestDto.Id);
            return new ProblemDetailsError("Campaign contribution not found.");
        }

        var currentCampaignContribution = queryResult.Value;
        
        if (userId != currentCampaignContribution.UserId)
        {
            _logger.LogWarning(
                "User id {UserId} is unauthorized to update campaign contribution {CampaignContributionId}",
                userId,
                updateRequestDto.Id
                );
            return new ProblemDetailsError("You are unauthorized to update this campaign contribution.");
        }
        
        var updatedCampaignContribution = _campaignContributionMapper.MapCampaignContributionUpdateRequestDtoToCampaignContribution(updateRequestDto);
        updatedCampaignContribution.CreatedAt = currentCampaignContribution.CreatedAt;
        updatedCampaignContribution.CreatedBy = currentCampaignContribution.CreatedBy;
        updatedCampaignContribution.CampaignId = currentCampaignContribution.CampaignId;
        
        updatedCampaignContribution.UpdatedAt = DateTime.UtcNow;
        updatedCampaignContribution.UpdatedBy = userId;
        
        var updateResult = await _campaignContributionsRepository.UpdateCampaignContribution(updatedCampaignContribution);
        if (updateResult.IsFailed)
        {
            _logger.LogWarning(
                "Failed to update campaign contribution {CampaignContributionId}, Error message: {ErrorMessage}",
                updateRequestDto.Id,
                updateResult.Errors.First().Message
                );
            return new ProblemDetailsError("Failed to update campaign contribution.");
        }

        _logger.LogInformation("Successfully updated campaign contribution {CampaignContributionId}", updateRequestDto.Id);
        return _campaignContributionMapper.MapCampaignContributionToCampaignContributionGetResponseDto(updatedCampaignContribution);
    }
}