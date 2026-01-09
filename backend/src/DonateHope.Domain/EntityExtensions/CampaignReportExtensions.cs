using DonateHope.Domain.Entities;

namespace DonateHope.Domain.EntityExtensions;

public static class CampaignReportExtensions
{
    public static CampaignReport OnCampaignReportCreating(this CampaignReport campaignReport, Guid ownerId)
    {
        var now = DateTime.UtcNow;
        campaignReport.Id = Guid.NewGuid();
        campaignReport.CreatedAt = now;
        campaignReport.CreatedBy = ownerId;
        campaignReport.UpdatedAt = now;
        campaignReport.UpdatedBy = ownerId;

        return campaignReport;
    }
}