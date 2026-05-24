using Microsoft.AspNetCore.Mvc;

namespace MedicalRecordService.Controllers;

[ApiController]
[Route("api/health")]
public class HealthController : ControllerBase
{
    [AcceptVerbs("GET", "HEAD")]
    public IActionResult Get()
    {
        return Ok(new { status = "Healthy" });
    }
}
