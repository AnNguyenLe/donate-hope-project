using DonateHope.Core.DTOs.Authentication;
using FluentValidation;

namespace DonateHope.Core.Validators.Authentication
{
    public class RegistrationAsCharityRequestValidator
        : AbstractValidator<RegistrationAsCharityRequest>
    {
        public RegistrationAsCharityRequestValidator()
        {
            RuleFor(model => model.OrgName)
                .NotEmpty()
                .WithMessage("Organization Name is required.")
                .MinimumLength(2)
                .WithMessage("Organization Name must be at least 2 characters long.");

            RuleFor(model => model.OrgAddress)
                .NotEmpty()
                .WithMessage("Organization Address is required.");

            RuleFor(model => model.OrgEmail)
                .NotEmpty()
                .WithMessage("Organization Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email address format.");

            RuleFor(model => model.OrgPhone)
                .NotEmpty()
                .WithMessage("Organization Phone is required.")
                .Matches(@"^\d{10,15}$")
                .WithMessage("Invalid phone number format. It should be between 10 and 15 digits.");

            RuleFor(model => model.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .MinimumLength(8)
                .WithMessage("Password must be at least 8 characters.")
                .Must(password => password.Any(char.IsLower))
                .WithMessage("Password must contain at least one lowercase letter.")
                .Must(password => password.Any(char.IsUpper))
                .WithMessage("Password must contain at least one uppercase letter.")
                .Must(password => password.Any(char.IsDigit))
                .WithMessage("Password must contain at least one digit.")
                .Must(password => password.Any(c => !char.IsLetterOrDigit(c)))
                .WithMessage("Password must contain at least one special character.");

            RuleFor(model => model.ConfirmPassword)
                .Equal(model => model.Password)
                .WithMessage("Password and Confirm Password do not match.")
                .NotEmpty()
                .WithMessage("Confirm Password is required.");

            RuleFor(model => model.RepFirstName)
                .NotEmpty()
                .WithMessage("Representative First Name is required.")
                .Matches("^[a-zA-Z ,.'-]+$")
                .WithMessage(
                    "Representative First Name can only contain English letters and the following special characters: comma, space, single quote, dot, and hyphen."
                )
                .MinimumLength(2)
                .WithMessage("Representative First Name must be at least 2 characters long.");

            RuleFor(model => model.RepLastName)
                .NotEmpty()
                .WithMessage("Representative Last Name is required.")
                .Matches("^[a-zA-Z ,.'-]+$")
                .WithMessage(
                    "Representative Last Name can only contain English letters and the following special characters: comma, space, single quote, dot, and hyphen."
                )
                .MinimumLength(2)
                .WithMessage("Representative Last Name must be at least 2 characters long.");

            RuleFor(model => model.RepEmail)
                .NotEmpty()
                .WithMessage("Representative Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email address format.");

            var today = DateOnly.FromDateTime(DateTime.UtcNow.Date);
            RuleFor(model => model.RepDateOfBirth)
                .NotEmpty()
                .WithMessage("Representative Date of Birth is required.")
                .Must(date => date < today)
                .WithMessage("Representative Date of Birth cannot be in the future.");
        }
    }
}
