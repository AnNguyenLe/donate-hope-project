using DonateHope.Domain.IdentityEntities;
using DonateHope.Domain.Traceables;

namespace DonateHope.Domain.Entities;

public class CampaignContribution : ITraceable
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string? UnitOfMeasurement { get; set; }
    public string? ContributionMethod { get; set; }
    public string? DonatorName { get; set; }
    public string? Message { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Guid? DeletedBy { get; set; }
    public string? ReasonForDeletion { get; set; }
    public Guid UserId { get; set; }
    public AppUser? User { get; set; }
    public Guid CampaignId { get; set; }
    public Campaign? Campaign { get; set; }
}
