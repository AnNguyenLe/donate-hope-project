using DonateHope.Core.DTOs.CampaignDTOs;
using DonateHope.Core.Errors;
using DonateHope.Core.Mappers;
using DonateHope.Core.ServiceContracts.CampaignsServiceContracts;
using DonateHope.Domain.RepositoryContracts;
using FluentResults;

namespace DonateHope.Core.Services.CampaignsServices;

public class CampaignUpdateService(
    CampaignMapper campaignMapper,
    ICampaignsRepository campaignsRepository
) : ICampaignUpdateService
{
    private readonly CampaignMapper _campaignMapper = campaignMapper;
    private readonly ICampaignsRepository _campaignsRepository = campaignsRepository;

    public async Task<Result> UpdateCampaignAsync(
        CampaignUpdateRequestDto updateRequestDto,
        Guid userId
    )
    {
        var queryResult = await _campaignsRepository.GetCampaignById(updateRequestDto.Id);

        if (queryResult.IsFailed || queryResult.ValueOrDefault is null)
        {
            return new ProblemDetailsError("Campaign not available.");
        }

        var currentCampaign = queryResult.Value;

        if (userId != currentCampaign.UserId)
        {
            return new ProblemDetailsError("You are not the owner of this campaign.");
        }

        if (currentCampaign.IsPublished)
        {
            return new ProblemDetailsError(
                "This campaign is published. You cannot edit published campaign."
            );
        }

        var updatedCampaign = _campaignMapper.MapCampaignUpdateRequestDtoToCampaign(
            updateRequestDto
        );

        updatedCampaign.UpdatedAt = DateTime.UtcNow;
        updatedCampaign.UpdatedBy = userId;

        var updateResult = await _campaignsRepository.UpdateCampaign(updatedCampaign);
        if (updateResult.IsFailed)
        {
            return new ProblemDetailsError("Failed to update campaign.");
        }

        return Result.Ok();
    }
}
