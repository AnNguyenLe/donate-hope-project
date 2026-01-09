using Asp.Versioning;
using DonateHope.Core.Common.ExtensionMethods;
using DonateHope.Core.ConfigurationOptions.AppServer;
using DonateHope.Core.DTOs.CampaignContributionDTOs;
using DonateHope.Core.Mappers;
using DonateHope.Core.ServiceContracts.CampaignContributionsServiceContracts;
using DonateHope.Domain.IdentityEntities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DonateHope.WebAPI.Controllers.v1.CampaignContribution;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/campaign-contribution")]
[ApiController]
public class CampaignContributionController(
    ICampaignContributionCreateService campaignContributionCreateService,
    ICampaignContributionRetrieveService campaignContributionRetrieveService,
    ICampaignContributionUpdateService campaignContributionUpdateService,
    ICampaignContributionDeleteService campaignContributionDeleteService,
    IValidator<CampaignContributionCreateRequestDto> campaignContributionCreateRequestValidator,
    IValidator<CampaignContributionUpdateRequestDto> campaignContributionUpdateRequestValidator,
    IValidator<CampaignContributionDeleteRequestDto> campaignContributionDeleteRequestValidator,
    UserManager<AppUser> userManager,
    IOptions<MyAppServerConfiguration> myAppServerConfiguration,
    CampaignContributionMapper campaignContributionMapper
    ) : CustomControllerBase
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly CampaignContributionMapper _campaignContributionMapper = campaignContributionMapper;
    private readonly MyAppServerConfiguration _app = myAppServerConfiguration.Value;
    private readonly ICampaignContributionCreateService _campaignContributionCreateService = campaignContributionCreateService;
    private readonly ICampaignContributionRetrieveService _campaignContributionRetrieveService = campaignContributionRetrieveService;
    private readonly ICampaignContributionUpdateService _campaignContributionUpdateService = campaignContributionUpdateService;
    private readonly ICampaignContributionDeleteService _campaignContributionDeleteService = campaignContributionDeleteService;
    private readonly IValidator<CampaignContributionCreateRequestDto> _campaignContributionCreateValidator = campaignContributionCreateRequestValidator;
    private readonly IValidator<CampaignContributionUpdateRequestDto> _campaignContributionUpdateValidator = campaignContributionUpdateRequestValidator;
    private readonly IValidator<CampaignContributionDeleteRequestDto> _campaignContributionDeleteValidator = campaignContributionDeleteRequestValidator;

    [HttpPost("create", Name = nameof(CreateCampaignContribution))]
    public async Task<ActionResult<CampaignContributionGetResponseDto>> CreateCampaignContribution(
        [FromBody] CampaignContributionCreateRequestDto createRequest)
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
        
        var modelValidationResult = await _campaignContributionCreateValidator.ValidateAsync(createRequest);
        if (!modelValidationResult.IsValid)
        {
            return modelValidationResult.Errors.ToValidatingDetailedBadRequest(
                title: "Failed to update campaign contribution.",
                detail: "Make sure all the required fields are properly entered.");
        }
        
        var result = await _campaignContributionCreateService.CreateCampaignContributionAsync(
            createRequest,
            parsedUserId
            );

        if (result.IsFailed)
        {
            return result.Errors.ToDetailedBadRequest();
        }

        var campaignContribution = result.Value;
        
        return CreatedAtRoute(
            nameof(GetCampaignContribution),
            new { id = campaignContribution.Id },
            campaignContribution
            );
    }

    [HttpGet("{id}", Name = nameof(GetCampaignContribution))]
    public async Task<ActionResult<CampaignContributionGetResponseDto>> GetCampaignContribution([FromRoute] Guid id)
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null)
        {
            return BadRequestProblemDetails("Unable to identify user");
        }
        var result = await _campaignContributionRetrieveService.GetCampaignContributionByIdAsync(id);

        if (result.IsFailed)
        {
            return result.Errors.ToDetailedBadRequest();
        }
        return result.Value;
    }

    [HttpPut("{id}", Name = nameof(UpdateCampaignContribution))]
    public async Task<ActionResult<CampaignContributionGetResponseDto>> UpdateCampaignContribution(
        [FromRoute] string id,
        [FromBody] CampaignContributionUpdateRequestDto updateRequestDto
    )
    {
        if (!Guid.TryParse(id, out var campaignContributionId))
        {
            return BadRequestProblemDetails("Invalid ID format");
        }

        if (campaignContributionId != updateRequestDto.Id)
        {
            return BadRequestProblemDetails("Campaign Contribution ID does not match.");
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
        
        var modelValidationResult = await _campaignContributionUpdateValidator.ValidateAsync(updateRequestDto);
        if (!modelValidationResult.IsValid)
        {
            return modelValidationResult.Errors.ToValidatingDetailedBadRequest(
                title: "Failed to update campaign contribution.",
                detail: "Make sure all the required fields are properly entered.");
        }

        var updatedResult = await _campaignContributionUpdateService.UpdateCampaignContributionAsync(
            updateRequestDto,
            parsedUserId
        );

        if (updatedResult.IsFailed)
        {
            return updatedResult.Errors.ToDetailedBadRequest();
        }
        
        return CreatedAtRoute(
            nameof(UpdateCampaignContribution),
            new { id = campaignContributionId },
            updatedResult.Value);
    }

    [HttpDelete("{id}", Name = nameof(DeleteCampaignContribution))]
    public async Task<ActionResult<CampaignContributionDeleteResponseDto>> DeleteCampaignContribution(
        [FromRoute] string id,
        [FromBody] CampaignContributionDeleteRequestDto reasonForDeletionRequestDto)
    {
        var modelValidationResult = await _campaignContributionDeleteValidator.ValidateAsync(reasonForDeletionRequestDto);
        
        if (!modelValidationResult.IsValid)
        {
            return modelValidationResult.Errors.ToValidatingDetailedBadRequest(
                title: "Failed to delete campaign contribution.",
                detail: "Make sure all the required fields are properly entered."
            );
        }
        
        if (!Guid.TryParse(id, out var campaignContributionId))
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
        
        var result = await _campaignContributionDeleteService.DeleteCampaignContributionAsync(
            campaignContributionId,
            deletedBy,
            reasonForDeletionRequestDto.ReasonForDeletion
            );

        if (result.IsFailed)
        {
            return result.Errors.ToDetailedBadRequest();
        }
        return result.Value;
    }
}