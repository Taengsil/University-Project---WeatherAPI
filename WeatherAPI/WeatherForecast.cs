using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPI
{
    public class WeatherForecast : iWeather
    {
        public int temp { get; set; }
        public int winddegrees { get; set; }
        public string weather { get; set; }

        public void GetWeather(string CityName, string StateCode)
        {
            ;
        }
    }
}
