using DonateHope.Core.DTOs.CampaignRatingDTOs;
using DonateHope.Core.Errors;
using DonateHope.Core.Mappers;
using DonateHope.Core.ServiceContracts.CampaignRatingsServiceContracts;
using DonateHope.Domain.RepositoryContracts;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace DonateHope.Core.Services.CampaignRatingServices;

public class CampaignRatingDeleteService(
    ILogger<CampaignRatingDeleteService> logger,
    ICampaignRatingsRepository campaignRatingsRepository,
    CampaignRatingMapper campaignRatingMapper
    ) : ICampaignRatingDeleteService
{
    private readonly ILogger<CampaignRatingDeleteService> _logger = logger;
    private readonly ICampaignRatingsRepository _campaignRatingsRepository = campaignRatingsRepository;
    private readonly CampaignRatingMapper _campaignRatingMapper = campaignRatingMapper;

    public async Task<Result<CampaignRatingDeleteResponseDto>> DeleteCampaignRatingAsync(
        Guid campaignRatingId,
        Guid deletedBy
        )
    {
        var queryResult = await _campaignRatingsRepository.GetCampaignRatingById(campaignRatingId);
        if (queryResult.IsFailed || queryResult.ValueOrDefault is null)
        {
            _logger.LogWarning(
                "Failed to retrieve campaign rating {CampaignRatingId}. ErrorMessage: {ErrorMessage}",
                campaignRatingId,
                queryResult.Errors.First().Message
                );
            return new ProblemDetailsError(queryResult.Errors.First().Message);
        }
        
        var deletedCampaignRating = queryResult.Value;
        
        if (deletedCampaignRating.IsDeleted)
        {
            _logger.LogWarning("The campaign rating {CampaignRatingId} is already marked as deleted.", campaignRatingId);
            return new ProblemDetailsError("This campaign rating does not exist.");
        }
        
        if (deletedBy != deletedCampaignRating.UserId)
        {
            _logger.LogWarning(
                "User id {UserId} is unauthorized to update campaign contribution {CampaignRatingId}",
                deletedBy,
                deletedCampaignRating.Id
            );
            return new ProblemDetailsError("You are unauthorized to update this campaign rating.");
        }
        
        deletedCampaignRating.DeletedAt = DateTime.UtcNow;
        deletedCampaignRating.DeletedBy = deletedBy;
        
        var deletedResult = await _campaignRatingsRepository.DeleteCampaignRating(
            campaignRatingId,
            deletedBy
        );

        if (deletedResult.IsFailed)
        {
            _logger.LogWarning(
                "Failed to delete campaign rating {CampaignRatingId}. Error: {ErrorMessage}",
                campaignRatingId,
                deletedResult.Errors.First().Message
                );
            return new ProblemDetailsError("Failed to delete campaign rating.");
        }
        
        _logger.LogInformation("Successfully deleted campaign rating {CampaignRatingId}", campaignRatingId);
        return _campaignRatingMapper.MapCampaignRatingToCampaignRatingDeleteResponseDto(deletedCampaignRating);
    }
}