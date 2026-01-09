namespace DonateHope.Core.DTOs.CampaignRatingDTOs;

public class CampaignRatingCreateRequestDto
{
    public double RatingPoint { get; set; }
    public string? Feedback { get; set; }
    public Guid? CampaignId { get; set; }
}