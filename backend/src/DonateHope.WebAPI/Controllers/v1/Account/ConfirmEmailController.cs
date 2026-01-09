using DonateHope.Core.Common.ExtensionMethods;
using DonateHope.Core.DTOs.Authentication;
using DonateHope.Core.ServiceContracts.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DonateHope.WebAPI.Controllers.v1.Account
{
    [ApiController]
    public class ConfirmEmailController(IAuthenticationService authenticationService)
        : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService = authenticationService;

        [HttpGet]
        [Route("api/v1/account/confirm-email")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthenticationResponse>> OnGetAsync(
            [FromQuery] EmailConfirmationRequest request
        )
        {
            var result = await _authenticationService.ConfirmEmailUserRegistrationAsync(request);

            if (result.IsFailed)
            {
                return result.Errors.ToDetailedBadRequest();
            }

            return result.ValueOrDefault;
        }
    }
}
