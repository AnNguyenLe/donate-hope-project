namespace DonateHope.Core.DTOs.Authentication;

public class RegistrationAsCharityRequest
{
    public string OrgAddress { get; set; } = string.Empty;
    public string OrgEmail { get; set; } = string.Empty;
    public string OrgName { get; set; } = string.Empty;
    public string OrgPhone { get; set; } = string.Empty;
    public DateOnly RepDateOfBirth { get; set; }
    public string RepEmail { get; set; } = string.Empty;
    public string RepFirstName { get; set; } = string.Empty;
    public string RepLastName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}
