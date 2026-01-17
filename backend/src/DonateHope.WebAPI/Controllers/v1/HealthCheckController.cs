namespace DonateHope.WebAPI.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/health")]
[AllowAnonymous]
public class HealthCheckController : ControllerBase
{
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

public class HealthCheckResponse
{
    public string Status { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public string Version { get; set; } = string.Empty;
}
