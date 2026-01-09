using DonateHope.Core.DTOs.CampaignReportDTOs;
using FluentResults;

namespace DonateHope.Core.ServiceContracts.CampaignReportsServiceContracts;

public interface ICampaignReportCreateService
{
    Task<Result<CampaignReportGetResponseDto>> CreateCampaignReportAsync(
        CampaignReportCreateRequestDto campaignReportCreateRequestDto,
        Guid userId
        );
}