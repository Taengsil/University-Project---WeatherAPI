using System.Threading.Tasks;

namespace WeatherAPI
{
    public interface IWeatherService
    {
        Task<OpenWeatherAPIResponse> GetWeatherData(string cityName, string stateCode);
    }

}
