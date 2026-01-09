using DonateHope.Core.DTOs.CampaignContributionDTOs;
using DonateHope.Core.DTOs.CampaignRatingDTOs;
using FluentResults;

namespace DonateHope.Core.ServiceContracts.CampaignRatingsServiceContracts;

public interface ICampaignRatingUpdateService
{
    Task<Result<CampaignRatingGetResponseDto>> UpdateCampaignRatingAsync(CampaignRatingUpdateRequestDto updateRequestDto, Guid userId);
}