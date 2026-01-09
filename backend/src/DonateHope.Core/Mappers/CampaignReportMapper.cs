using DonateHope.Core.DTOs.CampaignReportDTOs;
using DonateHope.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace DonateHope.Core.Mappers;

[Mapper]
public partial class CampaignReportMapper
{
    [MapperIgnoreTarget(nameof(CampaignReport.Id))]
    [MapperIgnoreTarget(nameof(CampaignReport.CreatedAt))]
    [MapperIgnoreTarget(nameof(CampaignReport.CreatedBy))]
    [MapperIgnoreTarget(nameof(CampaignReport.UpdatedAt))]
    [MapperIgnoreTarget(nameof(CampaignReport.UpdatedBy))]
    [MapperIgnoreTarget(nameof(CampaignReport.IsDeleted))]
    [MapperIgnoreTarget(nameof(CampaignReport.DeletedAt))]
    [MapperIgnoreTarget(nameof(CampaignReport.DeletedBy))]
    public partial CampaignReport MapCampaignReportCreateRequestDtoToCampaignReport(CampaignReportCreateRequestDto campaignReportCreateRequestDto);
    
    [MapperIgnoreSource(nameof(campaignReport.IsDeleted))]
    [MapperIgnoreSource(nameof(campaignReport.DeletedAt))]
    [MapperIgnoreSource(nameof(campaignReport.DeletedBy))]
    public partial CampaignReportGetResponseDto MapCampaignReportToCampaignReportGetResponseDto(CampaignReport campaignReport);
    
    [MapperIgnoreTarget(nameof(CampaignReport.CreatedAt))]
    [MapperIgnoreTarget(nameof(CampaignReport.CreatedBy))]
    [MapperIgnoreTarget(nameof(CampaignReport.IsDeleted))]
    [MapperIgnoreTarget(nameof(CampaignReport.DeletedAt))]
    [MapperIgnoreTarget(nameof(CampaignReport.DeletedBy))]
    [MapperIgnoreTarget(nameof(CampaignReport.CampaignId))]
    public partial CampaignReport MapCampaignReportUpdateRequestDtoToCampaignReport(CampaignReportUpdateRequestDto campaignReportUpdateRequestDto);
}