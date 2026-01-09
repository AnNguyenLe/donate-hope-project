using DonateHope.Core.DTOs.CampaignContributionDTOs;
using FluentResults;

namespace DonateHope.Core.ServiceContracts.CampaignContributionsServiceContracts;

public interface ICampaignContributionUpdateService
{
    Task<Result<CampaignContributionGetResponseDto>> UpdateCampaignContributionAsync(CampaignContributionUpdateRequestDto updateRequestDto, Guid userId);
}