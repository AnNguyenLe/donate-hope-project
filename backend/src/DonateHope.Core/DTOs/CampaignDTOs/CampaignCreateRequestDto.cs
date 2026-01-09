namespace DonateHope.Core.DTOs.CampaignDTOs;

public class CampaignCreateRequestDto
{
    public string? Title { get; init; }
    public string? Subtitle { get; init; }
    public string? Summary { get; init; }
    public string? Description { get; init; }
    public decimal GoalAmount { get; init; }
    public string? UnitOfMeasurement { get; init; }
    public decimal SpendingAmount { get; init; }
    public DateTime? ExpectingStartDate { get; init; }
    public DateTime? ExpectingEndDate { get; init; }
    public string? ProofsUrl { get; init; }
    public bool IsPublished { get; init; }
}
