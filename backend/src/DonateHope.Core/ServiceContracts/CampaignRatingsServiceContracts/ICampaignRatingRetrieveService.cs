using DonateHope.Core.DTOs.CampaignRatingDTOs;
using FluentResults;

namespace DonateHope.Core.ServiceContracts.CampaignRatingsServiceContracts;

public interface ICampaignRatingRetrieveService
{
    Task<Result<CampaignRatingGetResponseDto>> GetCampaignRatingByIdAsync(Guid campaignRatingId);
}