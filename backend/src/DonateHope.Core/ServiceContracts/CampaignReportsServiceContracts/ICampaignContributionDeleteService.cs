using DonateHope.Core.DTOs.CampaignReportDTOs;
using FluentResults;

namespace DonateHope.Core.ServiceContracts.CampaignReportsServiceContracts;

public interface ICampaignReportDeleteService
{
    Task<Result<CampaignReportDeleteResponseDto>> DeleteCampaignReportAsync(Guid campaignReportId, Guid deletedBy, string reasonForDeletion);
}