using DonateHope.Core.DTOs.CampaignCommentDTOs;
using FluentResults;

namespace DonateHope.Core.ServiceContracts.CampaignCommentServiceContracts;

public interface ICampaignCommentRetrieveService
{
    Task<Result<CampaignCommentGetResponseDto>> GetCampaignCommentById(Guid campaignCommentId);

    Task<Result<(IEnumerable<CampaignCommentGetResponseDto> Comments, int TotalCount)>> GetCommentsByCampaignId(Guid campaignId, int page = 1, int pageSize = 6);
}
