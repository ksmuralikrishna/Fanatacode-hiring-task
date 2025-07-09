using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoginDashboardApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        [HttpGet("chartdata")]
        public IActionResult GetChartData()
        {
            var data = new[]
            {
                new { Status = "Open", Count = 10 },
                new { Status = "In Progress", Count = 5 },
                new { Status = "Closed", Count = 7 }
            };

            return Ok(data);
        }
    }
}
