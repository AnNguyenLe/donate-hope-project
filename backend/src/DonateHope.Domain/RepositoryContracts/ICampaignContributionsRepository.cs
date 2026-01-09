using System.Collections;
using System.Linq.Expressions;
using DonateHope.Domain.Entities;
using FluentResults;

namespace DonateHope.Domain.RepositoryContracts;

public interface ICampaignContributionsRepository
{
    Task<Result<int>> AddCampaignContribution(CampaignContribution campaignContribution);
    Task<Result<CampaignContribution>> GetCampaignContributionById(Guid campaignContributionId); 
    IQueryable<CampaignContribution> GetCampaignContributions(Expression<Func<CampaignContribution, bool>> predicate);
    Task<Result<int>> UpdateCampaignContribution(CampaignContribution updateCampaignContribution);
    Task<Result<int>> DeleteCampaignContribution(Guid campaignContributionId, Guid deletedBy, string reasonForDeletion);
    Task<Result<int>> DeleteCampaignContributionPermanently(Guid campaignContributionId);
    Task<Result<IEnumerable<CampaignContribution>>> GetCampaignContributionsByCampaignId(Guid campaignId);
}