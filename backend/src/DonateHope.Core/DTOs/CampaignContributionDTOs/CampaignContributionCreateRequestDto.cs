namespace DonateHope.Core.DTOs.CampaignContributionDTOs;

public class CampaignContributionCreateRequestDto
{
    public decimal Amount { get; set; }
    public string? UnitOfMeasurement { get; set; }
    public string? ContributionMethod { get; set; }
    public string? DonatorName { get; set; }
    public string? Message { get; set; }
    public Guid? CampaignId { get; set; }
}