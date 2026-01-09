using DonateHope.Domain.Entities;

namespace DonateHope.Domain.EntityExtensions;

public static class CampaignRatingExtensions
{
    public static CampaignRating OnCampaignRatingCreating(this CampaignRating campaignRating, Guid ownerId)
    {
        var now = DateTime.UtcNow;
        campaignRating.Id = Guid.NewGuid();
        campaignRating.CreatedAt = now;
        campaignRating.CreatedBy = ownerId;
        campaignRating.UpdatedAt = now;
        campaignRating.UpdatedBy = ownerId;
        campaignRating.UserId = ownerId;

        return campaignRating;
    }
}