using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using MetricsAgent.DAL;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using MetricsAgent.Models;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;

        private readonly ICpuMetricsRepository _repository;

        public CpuMetricsController(ILogger<CpuMetricsController> logger, ICpuMetricsRepository repository)
        {
            _logger = logger;

            _logger.LogDebug(1, "NLog injected into CpuMetricsController");

            _repository = repository;
        }

        //Все-таки решил добавить метод Create для mock тестов
        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            _repository.Create(new CpuMetrics
            {
                Time = request.Time.ToUnixTimeMilliseconds(),
                Value = request.Value,
            });

            return Ok();
        }

        [HttpGet("getbytimeperiod")]
        public IActionResult GetByTimePeriod([FromBody] CpuMetricGetByTimePeriodRequest request)
        {
            _logger.LogInformation(1, $"This log from GetByTimePeriod - fromTime:{request.FromTime}, toTime:{request.ToTime}");

            var metrics = _repository.GetByTimePeriod(request.FromTime, request.ToTime);

            var response = new CpuMetricsResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new CpuMetricDto
                {
                    Time = DateTimeOffset.FromUnixTimeMilliseconds(metric.Time), 
                    Value = metric.Value, 
                    Id = metric.Id,
                });
            }

            return Ok(response);
        }
    }
}
