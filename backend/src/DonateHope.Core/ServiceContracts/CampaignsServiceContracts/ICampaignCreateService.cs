using DonateHope.Core.DTOs.CampaignDTOs;
using FluentResults;

namespace DonateHope.Core.ServiceContracts.CampaignsServiceContracts;

public interface ICampaignCreateService
{
    Task<Result<CampaignGetResponseDto>> CreateCampaignAsync(
        CampaignCreateRequestDto campaignCreateRequestDto,
        Guid userId
    );
}
