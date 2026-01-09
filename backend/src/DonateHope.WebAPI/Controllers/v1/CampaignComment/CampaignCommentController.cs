using Asp.Versioning;
using DonateHope.Core.Common.ExtensionMethods;
using DonateHope.Core.ConfigurationOptions.AppServer;
using DonateHope.Core.DTOs.CampaignCommentDTOs;
using DonateHope.Core.Mappers;
using DonateHope.Core.ServiceContracts.CampaignCommentServiceContracts;
using DonateHope.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;


namespace DonateHope.WebAPI.Controllers.v1.CampaignComment;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}")]
[ApiController]
public class CampaignCommentController(
    ICampaignCommentCreateService campaignCommentCreateService,
    ICampaignCommentRetrieveService campaignCommentRetrieveService,
    ICampaignCommentUpdateService campaignCommentUpdateService,
    ICampaignCommentDeleteService campaignCommentDeleteService,
    UserManager<AppUser> userManager,
    IOptions<MyAppServerConfiguration> myAppServerConfiguration,
    CampaignCommentMapper campaignCommentMapper
) : CustomControllerBase
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly CampaignCommentMapper _campaignCommentMapper = campaignCommentMapper;
    private readonly MyAppServerConfiguration _app = myAppServerConfiguration.Value;
    private readonly ICampaignCommentCreateService _campaignCommentCreateService = campaignCommentCreateService;
    private readonly ICampaignCommentRetrieveService _campaignCommentRetrieveService = campaignCommentRetrieveService;
    private readonly ICampaignCommentUpdateService _campaignCommentUpdateService = campaignCommentUpdateService;
    private readonly ICampaignCommentDeleteService _campaignCommentDeleteService = campaignCommentDeleteService;

    [HttpPost("campaign-comment/create", Name = nameof(CreateCampaignComment))]
    public async Task<ActionResult<CampaignCommentGetResponseDto>> CreateCampaignComment(
        [FromBody] CampaignCommentCreateRequestDto createRequest
    )
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
        var result = await _campaignCommentCreateService.CreateCampaignCommentAsync(createRequest, parsedUserId);

        if (result.IsFailed)
        {
            return result.Errors.ToDetailedBadRequest();
        }

        var campaignComment = result.Value;

        return CreatedAtRoute(
            nameof(CreateCampaignComment),
            new { id = campaignComment.Id },
            campaignComment
        );
    }
    [HttpGet("campaign-comment/{id}", Name = nameof(GetCampaignComment))]
    public async Task<ActionResult<CampaignCommentGetResponseDto>> GetCampaignComment([FromRoute] Guid id)
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null)
        {
            return BadRequestProblemDetails("Unable to identify user");
        }

        var result = await _campaignCommentRetrieveService.GetCampaignCommentById(id);

        if (result.IsFailed)
        {
            return result.Errors.ToDetailedBadRequest();
        }

        return result.Value;
    }
    [HttpPut("campaign-comment/{id}", Name = nameof(UpdateCampaignComment))]
    public async Task<ActionResult<CampaignCommentUpdateRequestDto>> UpdateCampaignComment([FromRoute] string id, [FromBody] CampaignCommentUpdateRequestDto updateCampaignCommentDto)
    {
        if (!Guid.TryParse(id, out var campaignCommentId))
        {
            return BadRequestProblemDetails("Invalid ID format.");
        }

        if (campaignCommentId != updateCampaignCommentDto.Id)
        {
            return BadRequestProblemDetails("Campaign Comment ID does not match.");
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

        var updatedResult = await _campaignCommentUpdateService.UpdateCampaignCommentAsync(
            updateCampaignCommentDto,
            parsedUserId
        );

        if (updatedResult.IsFailed)
        {
            return updatedResult.Errors.ToDetailedBadRequest();
        }

        return NoContent();
    }
    [HttpDelete("campaign-comment/{id}", Name = nameof(DeleteCampaignComment))]
    public async Task<ActionResult> DeleteCampaignComment(
        [FromRoute] string id)
    {
        if (!Guid.TryParse(id, out var campaignCommentId))
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

        var result = await _campaignCommentDeleteService.DeleteCampaignCommentAsync(
            campaignCommentId, deletedBy
            );

        if (result.IsFailed)
        {
            return result.Errors.ToDetailedBadRequest();
        }
        return Ok(new { Message = "Your comment has been deleted." });
    }

    [HttpGet("campaign/{id}/comment", Name = nameof(GetCommentsByCampaignId))]
    public async Task<ActionResult> GetCommentsByCampaignId([FromRoute] Guid id, [FromQuery] int page = 1, [FromQuery] int pageSize = 6)
    {
        pageSize = pageSize > 100 ? 100 : pageSize;
        var result = await _campaignCommentRetrieveService.GetCommentsByCampaignId(id, page, pageSize);

        if (result.IsFailed)
        {
            return result.Errors.ToDetailedBadRequest();
        }
        var (comments, totalCount) = result.Value;

        var response = new
        {
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize,
            Comments = comments
        };
        return Ok(response);
    }
}
