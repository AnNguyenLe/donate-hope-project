using DonateHope.Core.DTOs.CampaignContributionDTOs;
using FluentResults;

namespace DonateHope.Core.ServiceContracts.CampaignContributionsServiceContracts;

public interface ICampaignContributionDeleteService
{
    Task<Result<CampaignContributionDeleteResponseDto>> DeleteCampaignContributionAsync(Guid campaignContributionId, Guid deletedBy, string reasonForDeletion);
}