using DonateHope.Core.DTOs.CampaignDTOs;
using FluentResults;

namespace DonateHope.Core.ServiceContracts.CampaignsServiceContracts;

public interface ICampaignUpdateService
{
    Task<Result> UpdateCampaignAsync(CampaignUpdateRequestDto updateRequestDto, Guid userId);
}
