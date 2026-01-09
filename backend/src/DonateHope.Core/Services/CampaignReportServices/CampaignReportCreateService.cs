using DonateHope.Core.DTOs.CampaignReportDTOs;
using DonateHope.Core.Errors;
using DonateHope.Core.Mappers;
using DonateHope.Core.ServiceContracts.CampaignReportsServiceContracts;
using DonateHope.Domain.EntityExtensions;
using DonateHope.Domain.RepositoryContracts;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace DonateHope.Core.Services.CampaignReportServices;

public class CampaignReportCreateService(
    ILogger<CampaignReportCreateService> logger,
    ICampaignReportsRepository campaignReportsRepository,
    ICampaignsRepository campaignsRepository,
    CampaignReportMapper campaignReportMapper
    ) : ICampaignReportCreateService
{
    private readonly ILogger<CampaignReportCreateService> _logger = logger;
    private readonly ICampaignReportsRepository _campaignReportsRepository = campaignReportsRepository;
    private readonly ICampaignsRepository _campaignsRepository = campaignsRepository;
    private readonly CampaignReportMapper _campaignReportMapper = campaignReportMapper;

    public async Task<Result<CampaignReportGetResponseDto>> CreateCampaignReportAsync(
        CampaignReportCreateRequestDto campaignReportCreateRequestDto,
        Guid userId
    )
    {
        var campaignReport = _campaignReportMapper
            .MapCampaignReportCreateRequestDtoToCampaignReport(campaignReportCreateRequestDto)
            .OnCampaignReportCreating(userId);

        var campaign = await _campaignsRepository.GetCampaignById(campaignReport.CampaignId);
        if (campaign.ValueOrDefault is null)
        {
            _logger.LogWarning("Campaign not found for Id: {CampaignId}", campaignReport.CampaignId);
            return new ProblemDetailsError("Campaign not found.");
        }
        
        var queryResult = await _campaignReportsRepository.AddCampaignReport(campaignReport);
        if (queryResult.IsFailed)
        {
            _logger.LogWarning(
                "Failed to create campaign report {CampaignReportId}. Error: {ErrorMessage}",
                campaignReport.Id,
                queryResult.Errors.First().Message
                );
            return new ProblemDetailsError(
                "Unexpected error(s) during the campaign report creating process. Please contact support team."
            );
        }
        
        var totalAffectedRows = queryResult.ValueOrDefault;
        if (totalAffectedRows == 0)
        {
            _logger.LogWarning("No row affected when attempting to create campaign report for campaignId {CampaignId}.", campaignReport.CampaignId);
            return new ProblemDetailsError("Failed to create campaign report.");
        }
        
        _logger.LogInformation(
            "Successfully created campaign report {CampaignReportId} for campaign {CampaignId}", 
            campaignReport.Id,
            campaignReport.CampaignId
            );
        return _campaignReportMapper.MapCampaignReportToCampaignReportGetResponseDto(campaignReport);
    }
}