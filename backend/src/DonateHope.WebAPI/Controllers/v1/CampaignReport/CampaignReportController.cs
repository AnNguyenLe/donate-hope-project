using Asp.Versioning;
using DonateHope.Core.Common.ExtensionMethods;
using DonateHope.Core.ConfigurationOptions.AppServer;
using DonateHope.Core.DTOs.CampaignReportDTOs;
using DonateHope.Core.Mappers;
using DonateHope.Core.ServiceContracts.CampaignReportsServiceContracts;
using DonateHope.Domain.IdentityEntities;
using FluentResults;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DonateHope.WebAPI.Controllers.v1.CampaignReport;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/campaign-report")]
[ApiController]
public class CampaignReportController(
    ICampaignReportCreateService campaignReportCreateService,
    ICampaignReportRetrieveService campaignReportRetrieveService,
    ICampaignReportUpdateService campaignReportUpdateService,
    ICampaignReportDeleteService campaignReportDeleteService,
    IValidator<CampaignReportDeleteRequestDto> campaignReportDeleteRequestValidator,
    IValidator<CampaignReportUpdateRequestDto> campaignReportUpdateRequestValidator,
    UserManager<AppUser> userManager,
    IOptions<MyAppServerConfiguration> myAppServerConfiguration,
    CampaignReportMapper campaignReportMapper
    ) : CustomControllerBase
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly CampaignReportMapper _campaignReportMapper = campaignReportMapper;
    private readonly MyAppServerConfiguration _app = myAppServerConfiguration.Value;
    private readonly ICampaignReportCreateService _campaignReportCreateService = campaignReportCreateService;
    private readonly ICampaignReportRetrieveService _campaignReportRetrieveService = campaignReportRetrieveService;
    private readonly ICampaignReportUpdateService _campaignReportUpdateService = campaignReportUpdateService;
    private readonly ICampaignReportDeleteService _campaignReportDeleteService = campaignReportDeleteService;
    private readonly IValidator<CampaignReportUpdateRequestDto> _campaignReportUpdateValidator = campaignReportUpdateRequestValidator;
    private readonly IValidator<CampaignReportDeleteRequestDto> _campaignReportDeleteValidator = campaignReportDeleteRequestValidator;

    [HttpPost("create", Name = nameof(CreateCampaignReport))]
    public async Task<ActionResult<CampaignReportGetResponseDto>> CreateCampaignReport(
        [FromBody] CampaignReportCreateRequestDto createRequest)
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null)
        {
            return BadRequestProblemDetails("Unable to identify user");
        }

        if (!Guid.TryParse(userId, out var parsedUserId))
        {
            return BadRequestProblemDetails("Unable to identify user");
        }
        
        var result = await _campaignReportCreateService.CreateCampaignReportAsync(
            createRequest,
            parsedUserId
            );

        if (result.IsFailed)
        {
            return result.Errors.ToDetailedBadRequest();
        }

        var campaignReport = result.Value;
        
        return CreatedAtRoute(
            nameof(GetCampaignReport),
            new { id = campaignReport.Id },
            campaignReport
            );
    }

    [HttpGet("{id}", Name = nameof(GetCampaignReport))]
    public async Task<ActionResult<CampaignReportGetResponseDto>> GetCampaignReport([FromRoute] Guid id)
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null)
        {
            return BadRequestProblemDetails("Unable to identify user");
        }
        var result = await _campaignReportRetrieveService.GetCampaignReportByIdAsync(id);

        if (result.IsFailed)
        {
            return result.Errors.ToDetailedBadRequest();
        }
        return result.Value;
    }

    [HttpPut("{id}", Name = nameof(UpdateCampaignReport))]
    public async Task<ActionResult<CampaignReportGetResponseDto>> UpdateCampaignReport(
        [FromRoute] string id,
        [FromBody] CampaignReportUpdateRequestDto updateRequestDto
    )
    {
        var modelValidationResult = await _campaignReportUpdateValidator.ValidateAsync(updateRequestDto);
        if (!modelValidationResult.IsValid)
        {
            return modelValidationResult.Errors.ToValidatingDetailedBadRequest(
                title: "Failed to update campaign report.",
                detail: "Make sure all the required fields are properly entered.");
        }
        
        if (!Guid.TryParse(id, out var campaignReportId))
        {
            return BadRequestProblemDetails("Invalid ID format");
        }

        if (campaignReportId != updateRequestDto.Id)
        {
            return BadRequestProblemDetails("Campaign report ID does not match.");
        }
        
        var userId = _userManager.GetUserId(User);

        if (userId is null)
        {
            return BadRequestProblemDetails("Unable to identify user.");
        }

        if (!Guid.TryParse(userId, out var parsedUserId))
        {
            return BadRequestProblemDetails("Unable to identify user.");
        }

        var updatedResult = await _campaignReportUpdateService.UpdateCampaignReportAsync(
            updateRequestDto,
            parsedUserId
        );

        if (updatedResult.IsFailed)
        {
            return updatedResult.Errors.ToDetailedBadRequest();
        }

        return NoContent();
    }

    [HttpDelete("{id}", Name = nameof(DeleteCampaignReport))]
    public async Task<ActionResult<CampaignReportDeleteResponseDto>> DeleteCampaignReport(
        [FromRoute] string id,
        [FromBody] CampaignReportDeleteRequestDto reasonForDeletionRequestDto)
    {
        var modelValidationResult = await _campaignReportDeleteValidator.ValidateAsync(reasonForDeletionRequestDto);
        
        if (!modelValidationResult.IsValid)
        {
            return modelValidationResult.Errors.ToValidatingDetailedBadRequest(
                title: "Failed to delete campaign report.",
                detail: "Make sure all the required fields are properly entered."
            );
        }
        
        if (!Guid.TryParse(id, out var campaignReportId))
        {
            return BadRequestProblemDetails("Invalid ID format");
        }
        var userId = _userManager.GetUserId(User);
        
        if (userId is null)
        {
            return BadRequestProblemDetails("Unable to identify user");
        }

        if (!Guid.TryParse(userId, out var deletedBy))
        {
            return BadRequestProblemDetails("Invalid user identification");
        }
        
        var result = await _campaignReportDeleteService.DeleteCampaignReportAsync(
            campaignReportId,
            deletedBy,
            reasonForDeletionRequestDto.ReasonForDeletion
            );

        if (result.IsFailed)
        {
            return result.Errors.ToDetailedBadRequest();
        }
        return NoContent();
    }
}