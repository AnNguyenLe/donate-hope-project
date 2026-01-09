namespace DonateHope.Core.DTOs.CampaignRatingDTOs;

public class CampaignRatingGetResponseDto
{
    public Guid Id { get; set; }
    public double RatingPoint { get; set; }
    public string? Feedback { get; set; }
    public Guid? CampaignId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
}