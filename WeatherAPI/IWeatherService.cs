using System.Threading.Tasks;

namespace WeatherAPI
{
    public interface IWeatherService
    {
        Task<DataClass> GetWeatherData(string cityName, string stateCode);
    }

}