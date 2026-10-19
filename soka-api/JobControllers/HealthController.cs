using Microsoft.AspNetCore.Mvc;

namespace soka_api.JobControllers;

public class HealthController() : SoController
{

    [HttpGet]
    [Route("api/health")]
    public IActionResult GetHealth()
    {
        return Ok();
    }
}
