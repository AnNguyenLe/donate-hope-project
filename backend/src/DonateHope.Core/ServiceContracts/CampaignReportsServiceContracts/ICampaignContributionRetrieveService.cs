using DonateHope.Core.DTOs.CampaignReportDTOs;
using FluentResults;

namespace DonateHope.Core.ServiceContracts.CampaignReportsServiceContracts;

public interface ICampaignReportRetrieveService
{
    Task<Result<CampaignReportGetResponseDto>> GetCampaignReportByIdAsync(Guid campaignReportId);
}