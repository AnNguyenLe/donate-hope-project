namespace DonateHope.Core.DTOs.CampaignRatingDTOs;

public class CampaignRatingDeleteResponseDto
{
    public Guid Id { get; set; }
    public double RatingPoint { get; set; }
    public string? Feedback { get; set; }
    public Guid? CampaignId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Guid? DeletedBy { get; set; }
    public string? ReasonForDeletion { get; set; }
}