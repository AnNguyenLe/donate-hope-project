using DonateHope.Core.DTOs.CampaignReportDTOs;
using FluentResults;

namespace DonateHope.Core.ServiceContracts.CampaignReportsServiceContracts;

public interface ICampaignReportUpdateService
{
    Task<Result<CampaignReportGetResponseDto>> UpdateCampaignReportAsync(CampaignReportUpdateRequestDto updateRequestDto, Guid userId);
}