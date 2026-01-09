using DonateHope.Core.DTOs.CampaignCommentDTOs;
using FluentResults;

namespace DonateHope.Core.ServiceContracts.CampaignCommentServiceContracts;

public interface ICampaignCommentDeleteService
{
    Task<Result> DeleteCampaignCommentAsync(Guid campaignCommentId, Guid deletedBy);
}