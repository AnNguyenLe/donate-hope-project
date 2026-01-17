namespace DonateHope.WebAPI.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/health")]
[AllowAnonymous]
public class HealthCheckController : ControllerBase
{
    /// <summary>
    /// Health check endpoint to verify the API is running.
    /// </summary>
    /// <returns>Returns 200 OK with status information</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<HealthCheckResponse> HealthCheck()
    {
        var response = new HealthCheckResponse
        {
            Status = "Healthy",
            Timestamp = DateTime.UtcNow,
            Version = "1.0"
        };

        return Ok(response);
    }
}

/// <summary>
/// Response model for health check endpoint
/// </summary>
public class HealthCheckResponse
{
    /// <summary>
    /// Current health status of the API
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp of the health check
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// API version
    /// </summary>
    public string Version { get; set; } = string.Empty;
}
