namespace DonateHope.Core.Common.Authorization;

public static class AppUserRoles
{
    public const string ADMIN = "Admin";
    public const string DONATOR = "Donator";
    public const string CHARITY = "Charity";

    public static bool HasRole(this IList<string> userRoles, string expectingRole)
    {
        return userRoles.Any(role => role == expectingRole);
    }
}
