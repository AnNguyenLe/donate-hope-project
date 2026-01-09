using Asp.Versioning;
using DonateHope.Core.Common.ExtensionMethods;
using DonateHope.Core.ConfigurationOptions.AppServer;
using DonateHope.Core.DTOs.CampaignRatingDTOs;
using DonateHope.Core.Mappers;
using DonateHope.Core.ServiceContracts.CampaignRatingsServiceContracts;
using DonateHope.Domain.IdentityEntities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DonateHope.WebAPI.Controllers.v1.CampaignRating;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/campaign-rating")]
[ApiController]
public class CampaignRatingController(
    ICampaignRatingCreateService campaignRatingCreateService,
    ICampaignRatingRetrieveService campaignRatingRetrieveService,
    ICampaignRatingUpdateService campaignRatingUpdateService,
    ICampaignRatingDeleteService campaignRatingDeleteService,
    IValidator<CampaignRatingCreateRequestDto> campaignRatingCreateRequestValidator,
    IValidator<CampaignRatingUpdateRequestDto> campaignRatingUpdateRequestValidator,
    UserManager<AppUser> userManager,
    IOptions<MyAppServerConfiguration> myAppServerConfiguration,
    CampaignRatingMapper campaignRatingMapper
    ) : CustomControllerBase
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly CampaignRatingMapper _campaignRatingMapper = campaignRatingMapper;
    private readonly MyAppServerConfiguration _app = myAppServerConfiguration.Value;
    private readonly ICampaignRatingCreateService _campaignRatingCreateService = campaignRatingCreateService;
    private readonly ICampaignRatingRetrieveService _campaignRatingRetrieveService = campaignRatingRetrieveService;
    private readonly ICampaignRatingUpdateService _campaignRatingUpdateService = campaignRatingUpdateService;
    private readonly ICampaignRatingDeleteService _campaignRatingDeleteService = campaignRatingDeleteService;
    private readonly IValidator<CampaignRatingCreateRequestDto> _campaignRatingCreateValidator = campaignRatingCreateRequestValidator;
    private readonly IValidator<CampaignRatingUpdateRequestDto> _campaignRatingUpdateValidator = campaignRatingUpdateRequestValidator;

    [HttpPost("create", Name = nameof(CreateCampaignRating))]
    public async Task<ActionResult<CampaignRatingGetResponseDto>> CreateCampaignRating(
        [FromBody] CampaignRatingCreateRequestDto createRequest)
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
        
        var modelValidationResult = await _campaignRatingCreateValidator.ValidateAsync(createRequest);
        if (!modelValidationResult.IsValid)
        {
            return modelValidationResult.Errors.ToValidatingDetailedBadRequest(
                title: "Failed to update campaign rating.",
                detail: "Make sure all the required fields are properly entered.");
        }
        
        var result = await _campaignRatingCreateService.CreateCampaignRatingAsync(
            createRequest,
            parsedUserId
            );

        if (result.IsFailed)
        {
            return result.Errors.ToDetailedBadRequest();
        }

        var campaignRating = result.Value;
        
        return CreatedAtRoute(
            nameof(GetCampaignRating),
            new { id = campaignRating.Id },
            campaignRating
            );
    }

    [HttpGet("{id}", Name = nameof(GetCampaignRating))]
    public async Task<ActionResult<CampaignRatingGetResponseDto>> GetCampaignRating([FromRoute] Guid id)
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null)
        {
            return BadRequestProblemDetails("Unable to identify user");
        }
        var result = await _campaignRatingRetrieveService.GetCampaignRatingByIdAsync(id);

        if (result.IsFailed)
        {
            return result.Errors.ToDetailedBadRequest();
        }
        return result.Value;
    }

    [HttpPut("{id}", Name = nameof(UpdateCampaignRating))]
    public async Task<ActionResult<CampaignRatingGetResponseDto>> UpdateCampaignRating(
        [FromRoute] string id,
        [FromBody] CampaignRatingUpdateRequestDto updateRequestDto
    )
    {
        if (!Guid.TryParse(id, out var campaignRatingId))
        {
            return BadRequestProblemDetails("Invalid ID format");
        }

        if (campaignRatingId != updateRequestDto.Id)
        {
            return BadRequestProblemDetails("Campaign Rating ID does not match.");
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
        
        var modelValidationResult = await _campaignRatingUpdateValidator.ValidateAsync(updateRequestDto);
        if (!modelValidationResult.IsValid)
        {
            return modelValidationResult.Errors.ToValidatingDetailedBadRequest(
                title: "Failed to update campaign rating.",
                detail: "Make sure all the required fields are properly entered.");
        }

        var updatedResult = await _campaignRatingUpdateService.UpdateCampaignRatingAsync(
            updateRequestDto,
            parsedUserId
        );

        if (updatedResult.IsFailed)
        {
            return updatedResult.Errors.ToDetailedBadRequest();
        }
        
        return CreatedAtRoute(
            nameof(UpdateCampaignRating),
            new { id = campaignRatingId },
            updatedResult.Value);
    }

    [HttpDelete("{id}", Name = nameof(DeleteCampaignRating))]
    public async Task<ActionResult<CampaignRatingDeleteResponseDto>> DeleteCampaignRating(
        [FromRoute] string id
        )
    {
        if (!Guid.TryParse(id, out var campaignRatingId))
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
        
        var result = await _campaignRatingDeleteService.DeleteCampaignRatingAsync(
            campaignRatingId,
            deletedBy
            );

        if (result.IsFailed)
        {
            return result.Errors.ToDetailedBadRequest();
        }
        return result.Value;
    }
}