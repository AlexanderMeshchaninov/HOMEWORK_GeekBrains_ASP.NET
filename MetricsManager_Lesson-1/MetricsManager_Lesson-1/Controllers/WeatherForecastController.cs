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
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly ValuesHolder holder;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ValuesHolder holder)
        {
            _logger = logger;

            this.holder = holder;
        }

        [HttpGet("create")]
        public IActionResult Create([FromQuery] DateTime date)
        {
            var rnd = new Random();
            //Решил привязаться к не очень елегантному варианту - году (такого года не существует), поэтому сделал так
            //Как можно было бы адекватно выйти из данной ситуации?
            if (date.Year != 0001)
            {
                holder.weatherData.Add(new WeatherForecast
                {
                    Date = new DateTime(date.Year, date.Month, date.Day),
                    TemperatureC = rnd.Next(-20, 55),
                    Summary = Summaries[rnd.Next(Summaries.Length)]
                });
            }
            else
            {
                holder.weatherData.Add(new WeatherForecast
                {
                    Date = DateTime.Now,
                    TemperatureC = rnd.Next(-20, 55),
                    Summary = Summaries[rnd.Next(Summaries.Length)]
                });
            }
            string message = "Record were saved";
            return Ok(message);
        }

        [HttpGet("read")]
        public IActionResult Read([FromQuery] DateTime begin, [FromQuery] DateTime end)
        {
            if (begin.Year != 0001 || end.Year != 0001)
            {
                var result = new List<WeatherForecast>();

                for (int i = 0; i < holder.weatherData.Count; i++)
                {
                    DateTime from = new DateTime(begin.Year, begin.Month, begin.Day);
                    DateTime to = new DateTime(end.Year, end.Month, end.Day);
                    DateTime currentItem = holder.weatherData[i].Date;

                    if (currentItem >= from && currentItem <= to)
                    {
                        result.Add(holder.weatherData[i]);
                    }
                }
                return Ok(result);
                result.Clear();
            }
            else
            {
                return Ok(holder.weatherData);
            }
            return Ok();
        }

        [HttpPut("update")]
        public IActionResult Update([FromQuery] DateTime findDate, [FromQuery] int valueToUpdate, [FromQuery] int newValue)
        {
            for (int i = 0; i < holder.weatherData.Count; i++)
            {
                if (holder.weatherData[i].Date == findDate)
                {
                    if (holder.weatherData[i].TemperatureC == valueToUpdate)
                    {
                        holder.weatherData[i].TemperatureC = newValue;
                    }
                }
            }
            string message = "Record were updated";
            return Ok(message);
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] DateTime begin, [FromQuery] DateTime end)
        {
            for (int i = 0; i < holder.weatherData.Count; i++)
            {
                DateTime from = new DateTime(begin.Year, begin.Month, begin.Day);
                DateTime to = new DateTime(end.Year, end.Month, end.Day);
                DateTime currentItem = holder.weatherData[i].Date;

                if (currentItem >= from && currentItem <= to || currentItem >= from)
                {
                    holder.weatherData.RemoveAt(i);
                }
            }
            string message = "Records were deleted";
            return Ok(message);
        }
    }
}
