using DonateHope.Domain.IdentityEntities;
using DonateHope.Domain.Traceables;

namespace DonateHope.Domain.Entities;

public class CampaignComment : ITraceable, IBannedStatus
{
    public Guid Id { get; set; }
    public string? Content { get; set; }
    public DateTime? CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Guid? DeletedBy { get; set; }
    public string? ReasonForDeletion { get; set; }
    public bool IsBanned { get; set; }
    public string? BannedStatusNote { get; set; }
    public Guid? UserId { get; set; }
    public Guid? CampaignId { get; set; }
    public AppUser? User { get; set; }
    public Campaign? Campaign { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
