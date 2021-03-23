using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPI
{
    class Program
    {
        static async Task Main()
        {
            /* reads user keyboard input and formats it properly */
            WeatherGeneratorFromAPI.ReadInput();

            /* connects to API and generates WeatherData */
            await WeatherGeneratorFromAPI.GetWeather();

            /* generates output messages */
            WeatherGeneratorFromAPI.WorkWeatherData();
        }
    }
}
