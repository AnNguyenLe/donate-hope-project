using DonateHope.Core.DTOs.CampaignContributionDTOs;
using FluentValidation;

namespace DonateHope.Core.Validators.CampaignContribution;

public class CampaignContributionDeleteRequestValidator : AbstractValidator<CampaignContributionDeleteRequestDto>
{
    public CampaignContributionDeleteRequestValidator()
    {
        RuleFor(model => model.ReasonForDeletion)
            .NotEmpty()
            .WithMessage("Reason for deletion is required.");
    }
}