using DonateHope.Domain.Entities;

namespace DonateHope.Domain.EntityExtensions;

public static class CampaignCommentExtensions
{
    public static CampaignComment OnNewCampaignCommentCreating(this CampaignComment campaignComment, Guid ownerId)
    {
        var now = DateTime.UtcNow;
        campaignComment.Id = Guid.NewGuid();
        campaignComment.CreatedAt = now;
        campaignComment.CreatedBy = ownerId;
        campaignComment.UpdatedAt = now;
        campaignComment.UserId = ownerId;
        return campaignComment;
    }
}
