using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager_Lesson_1.Controllers
{
    [Route("api/weatherForecastMetrics/")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        private readonly ValuesHolder holder;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ValuesHolder holder)
        {
            _logger = logger;

            this.holder = holder;
        }

        [HttpGet("create")]
        public IActionResult Create([FromQuery] DateTimeOffset date, [FromQuery] int temperatureC)
        {
            holder.weatherForecastsData.Add(new WeatherForecast
            {
                Date = new DateTimeOffset(date.Date),
                TemperatureC = temperatureC,
            });

            string message = "Record were saved";
            return Ok(message);
        }

        [HttpGet("read")]
        public IActionResult Read([FromQuery] DateTimeOffset begin, [FromQuery] DateTimeOffset end)
        {
            var result = new List<WeatherForecast>();

            DateTimeOffset from = new DateTimeOffset(begin.Date);
            DateTimeOffset to = new DateTimeOffset(end.Date);
            
            for (int i = 0; i < holder.weatherForecastsData.Count; i++)
            {
                DateTimeOffset currentTime = holder.weatherForecastsData[i].Date;

                if (currentTime >= from && currentTime <= to)
                {
                    result.Add(holder.weatherForecastsData[i]);
                }
            }
            return Ok(result);
            result.Clear();
        }

        [HttpPut("update")]
        public IActionResult Update([FromQuery] DateTimeOffset findDate, [FromQuery] int newValue)
        {
            for (int i = 0; i < holder.weatherForecastsData.Count; i++)
            {
                if (holder.weatherForecastsData[i].Date == findDate)
                {
                    holder.weatherForecastsData[i].TemperatureC = newValue;
                }
            }
            string message = "Record were updated";
            return Ok(message);
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] DateTimeOffset begin, [FromQuery] DateTimeOffset end)
        {
            DateTimeOffset from = new DateTimeOffset(begin.Date);
            DateTimeOffset to = new DateTimeOffset(end.Date);
            
            for (int i = 0; i < holder.weatherForecastsData.Count; i++)
            {
                DateTimeOffset currentItem = holder.weatherForecastsData[i].Date;

                if (currentItem >= from && currentItem <= to)
                {
                    holder.weatherForecastsData[i] = null;
                }
            }

            //Пересобираем массив уже без null
            holder.weatherForecastsData = holder.weatherForecastsData.Where(x => x != null).ToList();
            string message = "Records were deleted";
            return Ok(message);
        }
    }
}
