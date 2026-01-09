namespace DonateHope.Core.DTOs.CampaignRatingDTOs;

public class CampaignRatingUpdateRequestDto
{
    public Guid Id { get; init; }
    public double RatingPoint { get; set; }
    public string? Feedback { get; set; }
}