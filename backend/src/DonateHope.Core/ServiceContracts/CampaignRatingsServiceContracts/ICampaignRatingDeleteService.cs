using DonateHope.Core.DTOs.CampaignRatingDTOs;
using FluentResults;

namespace DonateHope.Core.ServiceContracts.CampaignRatingsServiceContracts;

public interface ICampaignRatingDeleteService
{
    Task<Result<CampaignRatingDeleteResponseDto>> DeleteCampaignRatingAsync(Guid campaignRatingId, Guid deletedBy);
}