using DonateHope.Domain.Entities;
using DonateHope.Domain.IdentityEntities;
namespace DonateHope.Core.DTOs.CampaignCommentDTOs;

public class CampaignCommentCreateRequestDto
{
    public string? Content { get; set; }
    public Guid? CampaignId { get; set; }
}
