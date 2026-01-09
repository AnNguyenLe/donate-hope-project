namespace DonateHope.Core.DTOs.CampaignContributionDTOs;

public class CampaignContributionGetResponseDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string? UnitOfMeasurement { get; set; }
    public string? ContributionMethod { get; set; }
    public string? DonatorName { get; set; }
    public string? Message  { get; set; }
    public Guid? CampaignId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
}