using System.Linq.Expressions;
using DonateHope.Domain.Entities;
using FluentResults;

namespace DonateHope.Domain.RepositoryContracts;

public interface ICampaignReportsRepository
{
    Task<Result<int>> AddCampaignReport(CampaignReport campaignReport);
    Task<Result<CampaignReport>> GetCampaignReportById(Guid campaignReportId); 
    IQueryable<CampaignReport> GetCampaignReports(Expression<Func<CampaignReport, bool>> predicate);
    Task<Result<int>> UpdateCampaignReport(CampaignReport updatedCampaignReport);
    Task<Result<int>> DeleteCampaignReport(Guid campaignReportId, Guid deletedBy, string reasonForDeletion);
    Task<Result<int>> DeleteCampaignReportPermanently(Guid campaignReportId);
}