using DonateHope.Core.DTOs.CampaignReportDTOs;
using DonateHope.Core.Errors;
using DonateHope.Core.Mappers;
using DonateHope.Core.ServiceContracts.CampaignReportsServiceContracts;
using DonateHope.Domain.RepositoryContracts;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace DonateHope.Core.Services.CampaignReportServices;

public class CampaignReportRetrieveService(
    ILogger<CampaignReportRetrieveService> logger,
    ICampaignReportsRepository campaignReportsRepository,
    CampaignReportMapper campaignReportMapper
    ) : ICampaignReportRetrieveService
{
    private readonly ILogger<CampaignReportRetrieveService> _logger = logger;
    private readonly ICampaignReportsRepository _campaignReportsRepository = campaignReportsRepository;
    private readonly CampaignReportMapper _campaignReportMapper = campaignReportMapper;

    public async Task<Result<CampaignReportGetResponseDto>> GetCampaignReportByIdAsync(Guid campaignReportId)
    { 
        var campaignReportResult = await _campaignReportsRepository.GetCampaignReportById(campaignReportId);
        if (campaignReportResult.IsFailed)
        {
            _logger.LogWarning(
                "Failed to retrieve campaign report {CampaignReportId}. Error: {ErrorMessage}", 
                campaignReportId,
                campaignReportResult.Errors.First().Message
                );
            return new ProblemDetailsError(campaignReportResult.Errors.First().Message);
        }
        
        _logger.LogInformation("Successfully retrieved campaign report {CampaignReportId}", campaignReportId);
        return _campaignReportMapper.MapCampaignReportToCampaignReportGetResponseDto(campaignReportResult.Value);
    }
}