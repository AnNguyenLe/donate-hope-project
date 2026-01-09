using System.Linq.Expressions;
using DonateHope.Domain.Entities;
using FluentResults;

namespace DonateHope.Domain.RepositoryContracts;

public interface ICampaignRatingsRepository
{
    Task<Result<int>> AddCampaignRating(CampaignRating campaignRating);
    Task<Result<CampaignRating>> GetCampaignRatingById(Guid campaignRatingId); 
    IQueryable<CampaignRating> GetCampaignRatings(Expression<Func<CampaignRating, bool>> predicate);
    Task<Result<int>> UpdateCampaignRating(CampaignRating updatedCampaignRating);
    Task<Result<int>> DeleteCampaignRating(Guid campaignRatingId, Guid deletedBy);
    Task<Result<int>> DeleteCampaignRatingPermanently(Guid campaignRatingId);
}