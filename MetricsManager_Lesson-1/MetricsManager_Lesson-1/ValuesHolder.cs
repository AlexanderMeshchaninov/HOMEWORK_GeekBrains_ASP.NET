using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager_Lesson_1
{
    public class ValuesHolder
    {
        public List<WeatherForecast> weatherForecastsData;

        public ValuesHolder()
        {
            weatherForecastsData = new List<WeatherForecast>();
        }

        public void Add(WeatherForecast weather)
        {
            weatherForecastsData.Add(weather);
        }
    }
}
