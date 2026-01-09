using DonateHope.Core.DTOs.CampaignCommentDTOs;
using DonateHope.Core.Errors;
using DonateHope.Core.Mappers;
using DonateHope.Core.ServiceContracts.CampaignCommentServiceContracts;
using DonateHope.Domain.Entities;
using DonateHope.Domain.EntityExtensions;
using DonateHope.Domain.RepositoryContracts;
using FluentResults;

namespace DonateHope.Core.Services.CampaignCommentsServices;

public class CampaignCommentCreateService(
    ICampaignCommentsRepository campaignCommentsRepository,
    CampaignCommentMapper campaignCommentMapper
) : ICampaignCommentCreateService
{
    private readonly ICampaignCommentsRepository _campaignCommentsRepository = campaignCommentsRepository;
    private readonly CampaignCommentMapper _campaignCommentMapper = campaignCommentMapper;

    public async Task<Result<CampaignComment>> CreateCampaignCommentAsync(
        CampaignCommentCreateRequestDto campaignCommentCreateRequestDto,
        Guid userId
    )
    {
        var campaignComment = _campaignCommentMapper
            .MapCampaignCommentCreateRequestDtoToCampaignComment(campaignCommentCreateRequestDto)
            .OnNewCampaignCommentCreating(userId);

        var affectedRows = await _campaignCommentsRepository.AddCampaignComment(campaignComment);
        if (affectedRows == 0)
        {
            return new ProblemDetailsError("Failed to create campaign comment");
        }
        return campaignComment;
    }
}
