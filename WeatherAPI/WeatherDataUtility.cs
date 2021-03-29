using System.IO;
using System.Text.Json;

namespace WeatherAPI
{
    public class WeatherDataUtility
    {
        /* Prints the output of the data object as a pretty-printed json */
        public static WeatherForecast ReadFromJson(WeatherForecast weatherForecast)
        {
            string path = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName;
            string fileName = Path.Combine(path, "apidata.json");
            string jsonString = File.ReadAllText(fileName);

            DataClass data = JsonSerializer.Deserialize<DataClass>(jsonString);

            ExtractWeatherData(data, weatherForecast);
            return weatherForecast;
        }

        /* Transforming from data object to WeatherData object */
        public static void ExtractWeatherData(DataClass data, WeatherForecast WeatherData)
        {

            WeatherData.temp = data.main.temp;
            WeatherData.winddegrees = data.wind.deg;
            WeatherData.weather = data.weather[0].main;
        }
    }
}
