using System;
using System.IO;
using System.Text.Json;

namespace OpenWeatherAPIResponse
{
    public class WeatherDataUtility
    {
        /* Reads the output of a json object (printed earlier) */
        public static WeatherForecast ReadFromJson(WeatherForecast weatherForecast)
        {
            string path = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName;
            string fileName = Path.Combine(path, "apidata.json");
            string jsonString = File.ReadAllText(fileName);

            OpenWeatherAPIResponse data = JsonSerializer.Deserialize<OpenWeatherAPIResponse>(jsonString);

            ExtractWeatherData(data, weatherForecast);
            return weatherForecast;
        }

        /* Transforming from data object to WeatherData object */
        public static void ExtractWeatherData(OpenWeatherAPIResponse _data, WeatherForecast weatherData)
        {

            weatherData.temp = _data.main.temp;
            weatherData.windDegrees = _data.wind.deg;
            weatherData.weather = _data.weather[0].main;
        }
    }
}
