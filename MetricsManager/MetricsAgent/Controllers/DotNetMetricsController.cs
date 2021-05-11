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
using Microsoft.Extensions.Logging;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using MetricsAgent.Models;
using MetricsAgent.DAL;
>>>>>>> Lesson-3_branch

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
<<<<<<< HEAD
        [HttpGet("errors-count/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsErrorsCountFromAgent(TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }
=======
        private readonly ILogger<DotNetMetricsController> _logger;

        private readonly IDotNetMetricsRepository _repository;

        public DotNetMetricsController(ILogger<DotNetMetricsController> logger, IDotNetMetricsRepository repository)
        {
            _logger = logger;

            _logger.LogDebug(1, "NLog injected into DotNetMetricsController");

            _repository = repository;
        }

        //Все-таки решил добавить метод Create для mock тестов
        [HttpPost("create")]
        public IActionResult Create([FromBody] DotNetMetricCreateRequest request)
        {
            _repository.Create(new DotNetMetrics()
            {
                Time = request.Time.ToUnixTimeMilliseconds(),
                Value = request.Value,
            });
            return Ok();
        }

        [HttpGet("getbytimeperiod")]
        public IActionResult GetByTimePeriod([FromBody] DotNetMetricGetByTimePeriodRequest request)
        {
            _logger.LogInformation(1, $"This log from GetByTimePeriod - fromTime:{request.FromTime}, toTime:{request.ToTime}");

            var metrics = _repository.GetByTimePeriod(request.FromTime, request.ToTime);

            var response = new DotNetMetricResponse()
            {
                Metrics = new List<DotNetMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new DotNetMetricDto
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
