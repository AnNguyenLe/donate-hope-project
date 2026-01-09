using Asp.Versioning;
using DonateHope.Core.Common.Authorization;
using DonateHope.Core.Common.ExtensionMethods;
using DonateHope.Core.ConfigurationOptions.AppServer;
using DonateHope.Core.DTOs.CampaignContributionDTOs;
using DonateHope.Core.DTOs.CampaignDTOs;
using DonateHope.Core.Mappers;
using DonateHope.Core.ServiceContracts.CampaignContributionsServiceContracts;
using DonateHope.Core.ServiceContracts.CampaignsServiceContracts;
using DonateHope.Domain.IdentityEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DonateHope.WebAPI.Controllers.v1.Campaign;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/campaign")]
[ApiController]
public class CampaignController(
    UserManager<AppUser> userManager,
    CampaignMapper campaignMapper,
    IOptions<MyAppServerConfiguration> myAppServerConfiguration,
    ICampaignCreateService campaignCreateService,
    ICampaignRetrieveService campaignRetrieveService,
    ICampaignUpdateService campaignUpdateService,
    ICampaignContributionRetrieveService campaignContributionRetrieveService
) : CustomControllerBase
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly CampaignMapper _campaignMapper = campaignMapper;
    private readonly MyAppServerConfiguration _app = myAppServerConfiguration.Value;
    private readonly ICampaignCreateService _campaignCreateService = campaignCreateService;
    private readonly ICampaignRetrieveService _campaignRetrieveService = campaignRetrieveService;
    private readonly ICampaignUpdateService _campaignUpdateService = campaignUpdateService;
    private readonly ICampaignContributionRetrieveService _campaignContributionRetrieveService =
        campaignContributionRetrieveService;

    [HttpGet(Name = nameof(GetCampaigns))]
    public async Task<ActionResult<IEnumerable<CampaignGetResponseDto>>> GetCampaigns()
    {
        var userId = _userManager.GetUserId(User);

        if (userId is null)
        {
            return BadRequestProblemDetails("Unable to identify user.");
        }

        var result = await _campaignRetrieveService.GetCampaigns();

        if (result.IsFailed)
        {
            return result.Errors.ToDetailedBadRequest();
        }

        return Ok(result.Value);
    }

    // [Authorize(Policy = AppPolicies.RequireCharity)]
    [HttpPost("create", Name = nameof(CreateCampaign))]
    public async Task<ActionResult<CampaignGetResponseDto>> CreateCampaign(
        [FromBody] CampaignCreateRequestDto createRequestDto
    )
    {
        var userId = _userManager.GetUserId(User);

        if (userId is null)
        {
            return BadRequestProblemDetails("Unable to identify user.");
        }

        if (!Guid.TryParse(userId, out var parsedUserId))
        {
            return BadRequestProblemDetails("Unable to identify user.");
        }

        var result = await _campaignCreateService.CreateCampaignAsync(
            createRequestDto,
            parsedUserId
        );

        if (result.IsFailed)
        {
            return result.Errors.ToDetailedBadRequest();
        }

        var campaign = result.Value;

        return CreatedAtRoute(nameof(GetCampaign), new { id = campaign.Id }, campaign);
    }

    [HttpGet("{id}", Name = nameof(GetCampaign))]
    public async Task<ActionResult<CampaignGetResponseDto>> GetCampaign([FromRoute] string id)
    {
        var userId = _userManager.GetUserId(User);

        if (userId is null)
        {
            return BadRequestProblemDetails("Unable to identify user.");
        }

        if (!Guid.TryParse(id, out var campaignId))
        {
            return BadRequestProblemDetails("Invalid ID format");
        }

        var result = await _campaignRetrieveService.GetCampaignByIdAsync(campaignId);

        if (result.IsFailed)
        {
            return result.Errors.ToDetailedBadRequest();
        }

        return result.Value;
    }

    [HttpPut("{id}", Name = nameof(UpdateCampaign))]
    public async Task<ActionResult<CampaignGetResponseDto>> UpdateCampaign(
        [FromRoute] string id,
        [FromBody] CampaignUpdateRequestDto updateRequestDto
    )
    {
        if (!Guid.TryParse(id, out var campaignId))
        {
            return BadRequestProblemDetails("Invalid ID format.");
        }

        if (campaignId != updateRequestDto.Id)
        {
            return BadRequestProblemDetails("Campaign ID does not match.");
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

        var updateResult = await _campaignUpdateService.UpdateCampaignAsync(
            updateRequestDto,
            parsedUserId
        );

        if (updateResult.IsFailed)
        {
            return updateResult.Errors.ToDetailedBadRequest();
        }

        return NoContent();
    }

    [HttpGet("{id}/contribution/report", Name = nameof(GetCampaignContributionsByCampaignId))]
    public async Task<
        ActionResult<IEnumerable<CampaignContributionGetResponseDto>>
    > GetCampaignContributionsByCampaignId([FromRoute] Guid id)
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null)
        {
            return BadRequestProblemDetails("Unable to identify user");
        }
        var result =
            await _campaignContributionRetrieveService.GetCampaignContributionsByCampaignIdAsync(
                id
            );

        if (result.IsFailed)
        {
            return result.Errors.ToDetailedBadRequest();
        }
        return result.Value.ToList();
    }

    [HttpGet("search", Name = nameof(SearchCampaign))]
    public async Task<ActionResult<CampaignGetResponseDto>> SearchCampaign(
        [FromQuery] string keyword
    )
    {
        var result = await _campaignRetrieveService.FilterCampaigns(keyword);

        if (result.IsFailed)
        {
            return result.Errors.ToDetailedBadRequest();
        }

        return Ok(result.Value);
    }

    [AllowAnonymous]
    [HttpGet("landingpage", Name = nameof(GetTopHighestRatingCampaigns))]
    public async Task<
        ActionResult<IEnumerable<CampaignGetResponseDto>>
    > GetTopHighestRatingCampaigns()
    {
        var result = await _campaignRetrieveService.GetTopHighestRatingCampaigns();

        if (result.IsFailed)
        {
            return result.Errors.ToDetailedBadRequest();
        }

        return Ok(result.Value);
    }
}
