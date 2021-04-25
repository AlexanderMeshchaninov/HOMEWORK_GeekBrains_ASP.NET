using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        [HttpGet("available/{freeMemoryRamSizeInMb}")]
        public IActionResult GetMetricsFromAgent([FromRoute] decimal freeMemoryRamSizeInMb)
        {
            return Ok();
        }
    }
}
