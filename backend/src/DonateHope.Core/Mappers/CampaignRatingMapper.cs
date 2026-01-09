using DonateHope.Core.DTOs.CampaignContributionDTOs;
using DonateHope.Core.DTOs.CampaignRatingDTOs;
using DonateHope.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace DonateHope.Core.Mappers;

[Mapper]
public partial class CampaignRatingMapper
{
    [MapperIgnoreTarget(nameof(CampaignRating.Id))]
    [MapperIgnoreTarget(nameof(CampaignRating.UserId))]
    [MapperIgnoreTarget(nameof(CampaignRating.User))]
    [MapperIgnoreTarget(nameof(CampaignRating.CreatedAt))]
    [MapperIgnoreTarget(nameof(CampaignRating.CreatedBy))]
    [MapperIgnoreTarget(nameof(CampaignRating.UpdatedAt))]
    [MapperIgnoreTarget(nameof(CampaignRating.UpdatedBy))]
    [MapperIgnoreTarget(nameof(CampaignRating.IsDeleted))]
    [MapperIgnoreTarget(nameof(CampaignRating.DeletedAt))]
    [MapperIgnoreTarget(nameof(CampaignRating.DeletedBy))]
    public partial CampaignRating MapCampaignRatingCreateRequestDtoToCampaignRating(CampaignRatingCreateRequestDto campaignRatingCreateRequestDto);
    
    [MapperIgnoreSource(nameof(CampaignRating.IsDeleted))]
    [MapperIgnoreSource(nameof(CampaignRating.DeletedAt))]
    [MapperIgnoreSource(nameof(CampaignRating.DeletedBy))]
    public partial CampaignRatingGetResponseDto MapCampaignRatingToCampaignRatingGetResponseDto(CampaignRating campaignRating);
    
    [MapperIgnoreTarget(nameof(CampaignRating.UserId))]
    [MapperIgnoreTarget(nameof(CampaignRating.User))]
    [MapperIgnoreTarget(nameof(CampaignRating.CreatedAt))]
    [MapperIgnoreTarget(nameof(CampaignRating.CreatedBy))]
    [MapperIgnoreTarget(nameof(CampaignRating.IsDeleted))]
    [MapperIgnoreTarget(nameof(CampaignRating.DeletedAt))]
    [MapperIgnoreTarget(nameof(CampaignRating.DeletedBy))]
    [MapperIgnoreTarget(nameof(CampaignRating.CampaignId))]
    public partial CampaignRating MapCampaignRatingUpdateRequestDtoToCampaignRating(CampaignRatingUpdateRequestDto campaignRatingUpdateRequestDto);
    
    [MapperIgnoreSource(nameof(CampaignRating.UpdatedAt))]
    [MapperIgnoreSource(nameof(CampaignRating.UpdatedBy))]
    public partial CampaignRatingDeleteResponseDto MapCampaignRatingToCampaignRatingDeleteResponseDto(CampaignRating campaignRating);
}