using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager_Lesson_1
{
    public class ValuesHolder
    {
        public List<WeatherForecast> weatherData;

        public ValuesHolder()
        {
            weatherData = new List<WeatherForecast>();
        }

        public void Add(WeatherForecast weather)
        {
            weatherData.Add(weather);
        }
    }
}
