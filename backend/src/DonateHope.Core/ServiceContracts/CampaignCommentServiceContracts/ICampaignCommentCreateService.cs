using DonateHope.Core.DTOs.CampaignCommentDTOs;
using DonateHope.Domain.Entities;
using FluentResults;

namespace DonateHope.Core.ServiceContracts.CampaignCommentServiceContracts;

public interface ICampaignCommentCreateService
{
    Task<Result<CampaignComment>> CreateCampaignCommentAsync(
        CampaignCommentCreateRequestDto campaignCommentCreateRequestDto,
    Guid userId
    );
}
