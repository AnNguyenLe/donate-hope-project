using DonateHope.Core.DTOs.CampaignCommentDTOs;
using DonateHope.Core.Errors;
using DonateHope.Core.Mappers;
using DonateHope.Core.ServiceContracts.CampaignCommentServiceContracts;
using DonateHope.Domain.RepositoryContracts;
using FluentResults;

namespace DonateHope.Core.Services.CampaignCommentsServices;

public class CampaignCommentRetrieveService(
    ICampaignCommentsRepository campaignCommentsRepository,
    CampaignCommentMapper campaignCommentMapper
) : ICampaignCommentRetrieveService
{
    private readonly ICampaignCommentsRepository _campaignCommentsRepository = campaignCommentsRepository;
    private readonly CampaignCommentMapper _campaignCommentMapper = campaignCommentMapper;

    public async Task<Result<CampaignCommentGetResponseDto>> GetCampaignCommentById(Guid campaignCommentId)
    {


        var campaignCommentResult = await _campaignCommentsRepository.GetCampaignCommentById(campaignCommentId);
        if (campaignCommentResult.IsFailed)
        {
            return new ProblemDetailsError(campaignCommentResult.Errors.First().Message);
        }

        return _campaignCommentMapper.MapCampaignCommentToCampaignCommentGetResponseDto(campaignCommentResult.Value);
    }
    public async Task<Result<(IEnumerable<CampaignCommentGetResponseDto> Comments, int TotalCount)>> GetCommentsByCampaignId(Guid campaignId, int page = 1, int pageSize = 6)
    {

        var (comments, totalCount) = await _campaignCommentsRepository.GetCommentsByCampaignId(campaignId, page, pageSize);


        if (comments == null || !comments.Any())
        {
            return new ProblemDetailsError($"No comments found for Campaign ID: {campaignId}");
        }

        var commentDtos = comments.Select(_campaignCommentMapper.MapCampaignCommentToCampaignCommentGetResponseDto);

        return Result.Ok((commentDtos, totalCount));
    }
}

