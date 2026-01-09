using DonateHope.Core.DTOs.CampaignReportDTOs;
using FluentValidation;

namespace DonateHope.Core.Validators.CampaignReport;

public class CampaignReportDeleteRequestValidator : AbstractValidator<CampaignReportDeleteRequestDto>
{
    public CampaignReportDeleteRequestValidator()
    {
        RuleFor(model => model.ReasonForDeletion)
            .NotEmpty()
            .WithMessage("Reason for deletion is required.");
    }
}