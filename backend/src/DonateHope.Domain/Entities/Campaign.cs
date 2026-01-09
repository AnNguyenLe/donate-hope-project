using DonateHope.Domain.IdentityEntities;
using DonateHope.Domain.Traceables;

namespace DonateHope.Domain.Entities;

public class Campaign : ITraceable, IActiveStatus
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    public string? Summary { get; set; }
    public string? Description { get; set; }
    public decimal GoalAmount { get; set; }
    public decimal AchievedAmount { get; set; }
    public string? UnitOfMeasurement { get; set; }
    public string? GoalStatus { get; set; }
    public DateTime? ExpectingStartDate { get; set; }
    public DateTime? ExpectingEndDate { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int NumberOfRatings { get; set; }
    public double AverageRatingPoint { get; set; }
    public decimal SpendingAmount { get; set; }
    public string? ProofsUrl { get; set; }
    public bool IsPublished { get; set; }
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
    public Guid UserId { get; set; }
    public AppUser? User { get; set; }
    public Guid? CampaignStatusId { get; set; }
    public CampaignStatus? CampaignStatus { get; set; }
    public List<CampaignRating>? CampaignRatings { get; set; }
    public List<CampaignComment>? CampaignComments { get; set; }
    public List<CampaignContribution>? CampaignContributions { get; set; }
    public List<CampaignReport>? CampaignReports { get; set; }
    public List<CampaignLog>? CampaignLogs { get; set; }
}
