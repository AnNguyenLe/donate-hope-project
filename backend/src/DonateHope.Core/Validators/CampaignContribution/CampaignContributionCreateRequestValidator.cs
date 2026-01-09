using DonateHope.Core.DTOs.CampaignContributionDTOs;
using DonateHope.Domain.Enums;
using FluentValidation;

namespace DonateHope.Core.Validators.CampaignContribution;

public class CampaignContributionCreateRequestValidator : AbstractValidator<CampaignContributionCreateRequestDto>
{
    public CampaignContributionCreateRequestValidator()
    {
        RuleFor(model => model.Amount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Amount must be greater than or equal to 0.");
        
        RuleFor(model => model.UnitOfMeasurement)
            .Must(BeAValidUnitOfMeasurement)
            .WithMessage("UnitOfMeasurement is not valid.");
        
        RuleFor(model => model.ContributionMethod)
            .Must(BeAValidContributionMethod)
            .WithMessage("ContributionMethod is not valid.");
    }

    private bool BeAValidUnitOfMeasurement(string? unitOfMeasurement)
    {
        return Enum.TryParse(typeof(MeasurementUnits), unitOfMeasurement, true, out var result) &&
               Enum.IsDefined(typeof(MeasurementUnits), result);
    }

    public bool BeAValidContributionMethod(string? contributionMethod)
    {
        return Enum.TryParse(typeof(ContributionMethods), contributionMethod, true, out var result) &&
               Enum.IsDefined(typeof(ContributionMethods), result);
    }
}