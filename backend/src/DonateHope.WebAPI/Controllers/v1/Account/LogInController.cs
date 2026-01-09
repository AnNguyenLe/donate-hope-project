using Asp.Versioning;
using DonateHope.Core.Common.ExtensionMethods;
using DonateHope.Core.DTOs.Authentication;
using DonateHope.Core.ServiceContracts.Authentication;
using DonateHope.WebAPI.Filters.ActionFilterAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DonateHope.WebAPI.Controllers.v1.Account;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/account/login")]
public class LogInController(IAuthenticationService authenticationService) : ControllerBase
{
    private readonly IAuthenticationService _authenticationService = authenticationService;

    [HttpPost]
    [AllowAnonymous]
    [ModelBindingFailureFormatFilter]
    public async Task<ActionResult<AuthenticationResponse>> LogIn(LogInRequest loginRequest)
    {
        var loginResult = await _authenticationService.LogInAsync(loginRequest);

        if (loginResult.IsFailed)
        {
            return loginResult.Errors.ToDetailedBadRequest();
        }

        return loginResult.ValueOrDefault;
    }
}
