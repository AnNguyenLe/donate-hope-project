using DonateHope.Core.DTOs.CampaignContributionDTOs;
using FluentResults;

namespace DonateHope.Core.ServiceContracts.CampaignContributionsServiceContracts;

public interface ICampaignContributionRetrieveService
{
    Task<Result<CampaignContributionGetResponseDto>> GetCampaignContributionByIdAsync(Guid campaignContributionId);
    Task<Result<IEnumerable<CampaignContributionGetResponseDto>>> GetCampaignContributionsByCampaignIdAsync(Guid campaignId);
}