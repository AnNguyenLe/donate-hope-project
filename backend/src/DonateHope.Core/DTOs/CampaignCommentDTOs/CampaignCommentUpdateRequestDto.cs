using DonateHope.Domain.Entities;
using DonateHope.Domain.IdentityEntities;

namespace DonateHope.Core.DTOs.CampaignCommentDTOs;

public class CampaignCommentUpdateRequestDto
{
    public Guid Id { get; set; }
    public string? Content { get; set; }
}
