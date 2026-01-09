using DonateHope.Domain.Entities;
using DonateHope.Domain.Traceables;
using Microsoft.AspNetCore.Identity;

namespace DonateHope.Domain.IdentityEntities;

public class AppUser : IdentityUser<Guid>, ITraceable, IActiveStatus, IBannedStatus
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? RefreshTokenHash { get; set; }
    public DateTime? RefreshTokenExpiryDateTime { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Guid? DeletedBy { get; set; }
    public string? ReasonForDeletion { get; set; }
    public bool IsActive { get; set; }
    public string? ActiveStatusNote { get; set; }
    public bool IsBanned { get; set; }
    public string? BannedStatusNote { get; set; }
    public List<CampaignRating>? CampaignRatings { get; set; }
    public List<Campaign>? Campaigns { get; set; }
    public List<CampaignComment>? CampaignComments { get; set; }
    public List<CampaignContribution>? CampaignContributions { get; set; }
}
