using DonateHope.Core.DTOs.CampaignDTOs;
using DonateHope.Core.Errors;
using DonateHope.Core.Mappers;
using DonateHope.Core.ServiceContracts.CampaignsServiceContracts;
using DonateHope.Domain.Entities;
using DonateHope.Domain.EntityExtensions;
using DonateHope.Domain.RepositoryContracts;
using FluentResults;

namespace DonateHope.Core.Services.CampaignsServices;

public class CampaignCreateService(
    ICampaignsRepository campaignsRepository,
    CampaignMapper campaignMapper
) : ICampaignCreateService
{
    private readonly ICampaignsRepository _campaignsRepository = campaignsRepository;
    private readonly CampaignMapper _campaignMapper = campaignMapper;

    public async Task<Result<CampaignGetResponseDto>> CreateCampaignAsync(
        CampaignCreateRequestDto campaignCreateRequestDto,
        Guid userId
    )
    {
        var campaign = _campaignMapper
            .MapCampaignCreateRequestDtoToCampaign(campaignCreateRequestDto)
            .OnCampaignCreating(userId);

        var queryResult = await _campaignsRepository.AddCampaign(campaign);
        if (queryResult.IsFailed)
        {
            return new ProblemDetailsError(
                "Unexpected error(s) during the campaign creating process. Please contact support team."
            );
        }

        var totalAffectedRows = queryResult.ValueOrDefault;
        if (totalAffectedRows == 0)
        {
            return new ProblemDetailsError("Failed to create campaign");
        }
        return _campaignMapper.MapCampaignToCampaignGetResponseDto(campaign);
    }
}
