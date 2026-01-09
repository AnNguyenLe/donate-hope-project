using DonateHope.Domain.Entities;

namespace DonateHope.Domain.EntityExtensions;

public static class CampaignContributionExtensions
{
    public static CampaignContribution OnCampaignContributionCreating(this CampaignContribution campaignContribution, Guid ownerId)
    {
        var now = DateTime.UtcNow;
        campaignContribution.Id = Guid.NewGuid();
        campaignContribution.CreatedAt = now;
        campaignContribution.CreatedBy = ownerId;
        campaignContribution.UpdatedAt = now;
        campaignContribution.UpdatedBy = ownerId;
        campaignContribution.UserId = ownerId;

        return campaignContribution;
    }
}