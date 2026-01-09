using System.Linq.Expressions;
using DonateHope.Core.DTOs.CampaignDTOs;
using DonateHope.Core.Errors;
using DonateHope.Core.Mappers;
using DonateHope.Core.ServiceContracts.CampaignsServiceContracts;
using DonateHope.Domain.Entities;
using DonateHope.Domain.RepositoryContracts;
using FluentResults;

namespace DonateHope.Core.Services.CampaignsServices;

public class CampaignRetrieveService(
    ICampaignsRepository campaignsRepository,
    CampaignMapper campaignMapper
) : ICampaignRetrieveService
{
    private readonly ICampaignsRepository _campaignsRepository = campaignsRepository;
    private readonly CampaignMapper _campaignMapper = campaignMapper;

    public async Task<Result<CampaignGetResponseDto>> GetCampaignByIdAsync(Guid campaignId)
    {
        var campaignResult = await _campaignsRepository.GetCampaignById(campaignId);
        if (campaignResult.IsFailed)
        {
            return new ProblemDetailsError(campaignResult.Errors.First().Message);
        }

        return _campaignMapper.MapCampaignToCampaignGetResponseDto(campaignResult.Value);
    }

    public async Task<Result<IEnumerable<CampaignGetResponseDto>>> GetCampaigns()
    {
        var campaignsListQuery = await _campaignsRepository.GetCampaigns();

        if (campaignsListQuery.IsFailed)
        {
            return new ProblemDetailsError(campaignsListQuery.Errors.First().Message);
        }

        var campaignDtos = campaignsListQuery.Value.Select(
            _campaignMapper.MapCampaignToCampaignGetResponseDto
        );

        return Result.Ok(campaignDtos);
    }

    public async Task<Result<IEnumerable<CampaignGetResponseDto>>> FilterCampaigns(string keyword)
    {
        Expression<Func<Campaign, bool>> matchedExpression = campaign =>
            campaign.IsDeleted == false
            && (campaign.Title ?? string.Empty).ToLower().Contains(keyword.ToLower());

        var queryResult = await _campaignsRepository.GetCampaigns(matchedExpression);

        if (queryResult.IsFailed)
        {
            return new ProblemDetailsError(queryResult.Errors.First().Message);
        }

        var campaignDtos = queryResult.Value.Select(
            _campaignMapper.MapCampaignToCampaignGetResponseDto
        );

        return Result.Ok(campaignDtos);
    }
    
    public async Task<Result<IEnumerable<CampaignGetResponseDto>>> GetTopHighestRatingCampaigns()
    {
        var campaignsListQuery = await _campaignsRepository.GetCampaigns();

        if (campaignsListQuery.IsFailed)
        {
            return new ProblemDetailsError(campaignsListQuery.Errors.First().Message);
        }

        var top3Campaigns = campaignsListQuery.Value
            .Where((campaign) => !campaign.IsDeleted)
            .OrderByDescending(campaign => campaign.AverageRatingPoint)
            .Take(3);

        var campaignDtos = top3Campaigns.Select(
            _campaignMapper.MapCampaignToCampaignGetResponseDto
        );

        return Result.Ok(campaignDtos);
    }
}
