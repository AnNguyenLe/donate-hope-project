namespace DonateHope.Core.DTOs.CampaignContributionDTOs;

public class CampaignContributionUpdateRequestDto : CampaignContributionCreateRequestDto
{
    public Guid Id { get; init; }
    public decimal Amount { get; set; }
    public string? UnitOfMeasurement { get; set; }
    public string? ContributionMethod { get; set; }
}