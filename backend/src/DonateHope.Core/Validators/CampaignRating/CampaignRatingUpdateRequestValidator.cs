using DonateHope.Core.DTOs.CampaignRatingDTOs;
using DonateHope.Domain.Enums;
using FluentValidation;

namespace DonateHope.Core.Validators.CampaignRating;

public class CampaignRatingUpdateRequestValidator : AbstractValidator<CampaignRatingUpdateRequestDto>
{
    public CampaignRatingUpdateRequestValidator()
    {
        RuleFor(model => model.RatingPoint)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(5)
            .WithMessage("Rating point must be greater than or equal to 0 and less than or equal to 5.");
    }
}