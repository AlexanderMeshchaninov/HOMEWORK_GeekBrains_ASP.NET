<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
=======
﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using MetricsAgent.DAL;
using MetricsAgent.Models;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using Microsoft.Extensions.Logging;
>>>>>>> Lesson-3_branch

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
<<<<<<< HEAD
        [HttpGet("available/{freeMemoryRamSizeInMb}")]
        public IActionResult GetMetricsFromAgent([FromRoute] decimal freeMemoryRamSizeInMb)
        {
            return Ok();
        }
=======
        private readonly ILogger<RamMetricsController> _logger;

        private readonly IRamMetricsRepository _repository;

        public RamMetricsController(ILogger<RamMetricsController> logger, IRamMetricsRepository repository)
        {
            _logger = logger;

            _logger.LogDebug(1, "NLog injected into RamMetricsController");

            _repository = repository;
        }

        //Все-таки решил добавить метод Create для mock тестов
        [HttpPost("create")]
        public IActionResult Create([FromBody] RamMetricCreateRequest request)
        {
            _repository.Create(new RamMetrics()
            {
                Time = request.Time.ToUnixTimeMilliseconds(),
                Value = request.Value,
            });
            return Ok();
        }

        [HttpGet("getbytimeperiod")]
        public IActionResult GetByTimePeriod([FromBody] RamMetricGetByTimePeriodRequest request)
        {
            _logger.LogInformation(1, $"This log from GetByTimePeriod - fromTime:{request.FromTime}, toTime:{request.ToTime}");

            var metrics = _repository.GetByTimePeriod(request.FromTime, request.ToTime);

            var response = new RamMetricsResponse()
            {
                Metrics = new List<RamMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new RamMetricDto()
                {
                    Time = DateTimeOffset.FromUnixTimeMilliseconds(metric.Time), 
                    Value = metric.Value, 
                    Id = metric.Id,
                });
            }

            return Ok(response);
        }
>>>>>>> Lesson-3_branch
    }
}
