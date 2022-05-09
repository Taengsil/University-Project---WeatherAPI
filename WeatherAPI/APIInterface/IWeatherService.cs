using System.Threading.Tasks;

namespace OpenWeatherAPIResponse
{
    public interface IWeatherService
    {
        Task<OpenWeatherAPIResponse> GetWeatherData(string cityName, string stateCode);
    }

}