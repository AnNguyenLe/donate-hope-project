using Asp.Versioning;
using DonateHope.Core.Common.ExtensionMethods;
using DonateHope.Core.DTOs.Authentication;
using DonateHope.Core.ServiceContracts.Authentication;
using DonateHope.WebAPI.Filters.ActionFilterAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DonateHope.WebAPI.Controllers.v1.Account
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/account/forgot-password")]
    public class ForgotPasswordController(IAuthenticationService authenticationService)
        : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService = authenticationService;

        [HttpPost(Name = "ForgotPassword")]
        [AllowAnonymous]
        [ModelBindingFailureFormatFilter]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
            var result = await _authenticationService.ForgotPasswordAsync(request);

            if (result.IsFailed)
            {
                return result.Errors.ToDetailedBadRequest();
            }

            return Ok();
        }
    }
}
