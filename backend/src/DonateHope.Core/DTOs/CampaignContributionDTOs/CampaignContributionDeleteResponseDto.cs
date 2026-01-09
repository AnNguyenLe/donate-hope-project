namespace DonateHope.Core.DTOs.CampaignContributionDTOs;

public class CampaignContributionDeleteResponseDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string? UnitOfMeasurement { get; set; }
    public string? ContributionMethod { get; set; }
    public Guid? CampaignId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Guid? DeletedBy { get; set; }
    public string? ReasonForDeletion { get; set; }
}