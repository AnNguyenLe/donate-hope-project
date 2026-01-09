using DonateHope.Domain.Entities;

namespace DonateHope.Core.DTOs.CampaignDTOs;

public class CampaignGetResponseDto
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
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
    public CampaignStatus? CampaignStatus { get; set; }
    public Guid UserId { get; set; }
}
