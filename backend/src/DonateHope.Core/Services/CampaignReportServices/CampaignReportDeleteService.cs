using DonateHope.Core.DTOs.CampaignReportDTOs;
using DonateHope.Core.Errors;
using DonateHope.Core.Mappers;
using DonateHope.Core.ServiceContracts.CampaignReportsServiceContracts;
using DonateHope.Domain.RepositoryContracts;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace DonateHope.Core.Services.CampaignReportServices;

public class CampaignReportDeleteService(
    ILogger<CampaignReportDeleteService> logger,
    ICampaignReportsRepository campaignReportsRepository,
    CampaignReportMapper campaignReportMapper
    ) : ICampaignReportDeleteService
{
    private readonly ILogger<CampaignReportDeleteService> _logger = logger;
    private readonly ICampaignReportsRepository _campaignReportsRepository = campaignReportsRepository;
    private readonly CampaignReportMapper _campaignReportMapper = campaignReportMapper;

    public async Task<Result<CampaignReportDeleteResponseDto>> DeleteCampaignReportAsync(
        Guid campaignReportId,
        Guid deletedBy,
        string reasonForDeletion 
        )
    {
        var queryResult = await _campaignReportsRepository.GetCampaignReportById(campaignReportId);
        if (queryResult.IsFailed || queryResult.ValueOrDefault is null)
        {
            _logger.LogWarning(
                "Failed to retrieve campaign report {CampaignReportId}. Error: {ErrorMessage}",
                campaignReportId,
                queryResult.Errors.First().Message
                );
            return new ProblemDetailsError(queryResult.Errors.First().Message);
        }
        
        var deletedCampaignReport = queryResult.Value;
        if (deletedCampaignReport.IsDeleted)
        {
            _logger.LogWarning("The campaign report {CampaignReportId} is already marked as deleted.", deletedCampaignReport.Id);
            return new ProblemDetailsError("This campaign report does not exist.");
        }
        
        var deletedResult = await _campaignReportsRepository.DeleteCampaignReport(
            campaignReportId,
            deletedBy,
            reasonForDeletion
        );

        if (deletedResult.IsFailed)
        {
            _logger.LogWarning(
                "Failed to delete campaign report {CampaignReportId}. Error: {ErrorMessage}",
                campaignReportId,
                deletedResult.Errors.First().Message
                );
            return new ProblemDetailsError("Failed to delete campaign report.");
        }

        return Result.Ok();
    }
}