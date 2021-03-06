using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
<<<<<<< HEAD
=======
using Microsoft.Extensions.Logging;
>>>>>>> Lesson-3_branch

namespace MetricsManager.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
<<<<<<< HEAD
    public class RamMetricsController : ControllerBase//TODO:
    {
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
=======
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;

        public RamMetricsController(ILogger<RamMetricsController> logger)
        {
            _logger = logger;

            _logger.LogDebug(1, "NLog injected into RamMetricsController");
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation(1, $"This log from GetMetricsFromAgent - agentId:{agentId}, fromTime:{fromTime}, toTime:{toTime}");

>>>>>>> Lesson-3_branch
            return Ok();
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}/percintiles/{percentile}")]
        public IActionResult GetMetricsPercentilesFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
<<<<<<< HEAD
=======
            _logger.LogInformation(1, $"This log from GetMetricsPercentilesFromAgent - agentId:{agentId}, fromTime:{fromTime}, toTime:{toTime}");

>>>>>>> Lesson-3_branch
            return Ok();
        }

        [HttpHead("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
<<<<<<< HEAD
=======
            _logger.LogInformation(1, $"This log from GetMetricsFromAllCluster - fromTime:{fromTime}, toTime:{toTime}");

>>>>>>> Lesson-3_branch
            return Ok();
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsByPercentileFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime, [FromRoute] Percentile percentile)
        {
<<<<<<< HEAD
=======
            _logger.LogInformation(1, $"This log from GetMetricsByPercentileFromAllCluster - fromTime:{fromTime}, toTime:{toTime}, percentile:{percentile}");

>>>>>>> Lesson-3_branch
            return Ok();
        }
    }
}
