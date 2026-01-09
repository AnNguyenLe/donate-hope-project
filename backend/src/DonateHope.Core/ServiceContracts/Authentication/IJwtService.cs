using DonateHope.Domain.IdentityEntities;

namespace DonateHope.Core.ServiceContracts.Authentication;

public interface IJwtService
{
    AccessTokenData GenerateAccessToken(AppUser user, List<string> roles);
    AccessTokenData GenerateCharityAccessToken(AppUser user, List<string> roles);
}
