using System.Linq.Expressions;
using DonateHope.Core.DTOs.CampaignDTOs;
using DonateHope.Domain.Entities;
using FluentResults;

namespace DonateHope.Core.ServiceContracts.CampaignsServiceContracts;

public interface ICampaignRetrieveService
{
    Task<Result<IEnumerable<CampaignGetResponseDto>>> GetCampaigns();
    Task<Result<CampaignGetResponseDto>> GetCampaignByIdAsync(Guid campaignId);
    Task<Result<IEnumerable<CampaignGetResponseDto>>> FilterCampaigns(string keyword);
    Task<Result<IEnumerable<CampaignGetResponseDto>>> GetTopHighestRatingCampaigns();
}
