using DonateHope.Core.DTOs.CampaignReportDTOs;
using DonateHope.Core.Errors;
using DonateHope.Core.Mappers;
using DonateHope.Core.ServiceContracts.CampaignReportsServiceContracts;
using DonateHope.Domain.RepositoryContracts;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace DonateHope.Core.Services.CampaignReportServices;

public class CampaignReportUpdateService (
    ILogger<CampaignReportUpdateService> logger,
    CampaignReportMapper campaignReportMapper,
    ICampaignReportsRepository campaignReportsRepository
    ) : ICampaignReportUpdateService
{
    private readonly ILogger<CampaignReportUpdateService> _logger = logger;
    private readonly CampaignReportMapper _campaignReportMapper = campaignReportMapper;
    private readonly ICampaignReportsRepository _campaignReportsRepository = campaignReportsRepository;

    public async Task<Result<CampaignReportGetResponseDto>> UpdateCampaignReportAsync(
        CampaignReportUpdateRequestDto updateRequestDto,
        Guid userId
    )
    {
        var queryResult = await _campaignReportsRepository.GetCampaignReportById(updateRequestDto.Id);

        if (queryResult.IsFailed || queryResult.ValueOrDefault is null)
        {
            _logger.LogWarning("Failed to retrieve campaign report {CampaignReportId}", updateRequestDto.Id);
            return new ProblemDetailsError("Campaign report not found.");
        }

        var currentCampaignReport = queryResult.Value;
        
        var updatedCampaignReport = _campaignReportMapper.MapCampaignReportUpdateRequestDtoToCampaignReport(updateRequestDto);
        updatedCampaignReport.CreatedAt = currentCampaignReport.CreatedAt;
        updatedCampaignReport.CreatedBy = currentCampaignReport.CreatedBy;
        updatedCampaignReport.CampaignId = currentCampaignReport.CampaignId;
        
        updatedCampaignReport.UpdatedAt = DateTime.UtcNow;
        updatedCampaignReport.UpdatedBy = userId;
        
        var updateResult = await _campaignReportsRepository.UpdateCampaignReport(updatedCampaignReport);
        if (updateResult.IsFailed)
        {
            _logger.LogWarning(
                "Failed to update campaign report {CampaignReportId}, Error message: {ErrorMessage}",
                updateRequestDto.Id,
                updateResult.Errors.First().Message
                );
            return new ProblemDetailsError("Failed to update campaign report.");
        }

        _logger.LogInformation("Successfully updated campaign report {CampaignReportId}", updateRequestDto.Id);
        return _campaignReportMapper.MapCampaignReportToCampaignReportGetResponseDto(updatedCampaignReport);
    }
}