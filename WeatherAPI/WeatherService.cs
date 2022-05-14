using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherAPI
{
    public class WeatherService : IWeatherService
    {
        private static HttpClient httpClient = new HttpClient();

        async Task<OpenWeatherAPIResponse> IWeatherService.GetWeatherData(string cityName, string stateCode)
        {

            string baseUrl = GenerateUrl(cityName, stateCode);
            if (baseUrl != null)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, baseUrl);
                using var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                if (response.IsSuccessStatusCode)
                {
                    WeatherProcessor.fetchingIsSuccess = true;
                    try
                    {
                        return await response.Content.ReadFromJsonAsync<OpenWeatherAPIResponse>();
                    }
                    catch (NotSupportedException) // When content type is not valid
                    {
                        string reason = "The content type is not supported.";
                        OpenWeatherAPIResponse mockedAPIResponse = new OpenWeatherAPIResponse();
                        mockedAPIResponse = BuildLocalAPIResponse(mockedAPIResponse, reason);

                        WeatherProcessor.fetchingIsSuccess = true;
                        return mockedAPIResponse;
                    }
                    catch (JsonException) // Invalid JSON
                    {
                        string reason = "Invalid JSON.";
                        OpenWeatherAPIResponse mockedAPIResponse = new OpenWeatherAPIResponse();
                        mockedAPIResponse = BuildLocalAPIResponse(mockedAPIResponse, reason);

                        WeatherProcessor.fetchingIsSuccess = true;
                        return mockedAPIResponse;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("{0} Exception caught.", e);
                        string reason = "Caught unhandled exception.";
                        OpenWeatherAPIResponse mockedAPIResponse = new OpenWeatherAPIResponse();
                        mockedAPIResponse = BuildLocalAPIResponse(mockedAPIResponse, reason);

                        WeatherProcessor.fetchingIsSuccess = true;
                        return mockedAPIResponse;
                    }
                }
            }
            else if (baseUrl == null)
            {
                string reason = "isEnabled set to false or baseUrl is null. Check appsettings.json if this was not on purpose.";
                OpenWeatherAPIResponse mockedAPIResponse = new OpenWeatherAPIResponse();
                mockedAPIResponse = BuildLocalAPIResponse(mockedAPIResponse, reason);

                WeatherProcessor.fetchingIsSuccess = true;
                return mockedAPIResponse;
            }
            return null;

        }

        /* Fetching API Key, generating and returning baseUrl when given CityName and StateCode*/
        private string GenerateUrl(string CityName, string StateCode)
        {
            /* fetching data from appsettings.json */
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();

            var section = config.GetSection(nameof(OpenWeatherAPIRequest));
            var openWeatherAPIRequest = section.Get<OpenWeatherAPIRequest>();

            /* generating baseUrl */
            string baseUrl;
            if (openWeatherAPIRequest.isEnabled == true)
            {
                if (openWeatherAPIRequest.weatherAPIUrl != "http://api.openweathermap.org/data/2.5/weather?q=")
                {
                    Console.WriteLine("Invalid weatherAPI url. Using default URL.\n");
                    openWeatherAPIRequest.weatherAPIUrl = "http://api.openweathermap.org/data/2.5/weather?q=";
                }
                if (openWeatherAPIRequest.options != "&units=imperial&appid=")
                {
                    Console.WriteLine("Invalid options. Using default options.\n");
                    openWeatherAPIRequest.options = "&units=imperial&appid=";
                }
                baseUrl = openWeatherAPIRequest.weatherAPIUrl + CityName + "," + StateCode + openWeatherAPIRequest.options + openWeatherAPIRequest.apiKey;
            }
            else
            {
                baseUrl = null;
            }
            return baseUrl;
        }

        public static async Task PrintToJson(OpenWeatherAPIResponse Json)
        {

            string path = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName;
            string fileName = Path.Combine(path, "apidata.json");
            //using (StreamWriter file = File.CreateText(fileName))
            //{
            //    Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
            //    serializer.Serialize(file, json);
            //}
            using FileStream createstream = File.Create(fileName);
            await JsonSerializer.SerializeAsync(createstream, Json);
        }

        private OpenWeatherAPIResponse BuildLocalAPIResponse(OpenWeatherAPIResponse _mockedAPIResponse, string reason)
        {
            Console.WriteLine("\n" + reason + " Using local values...\n");
            _mockedAPIResponse.coord = new OpenWeatherAPIResponse.Coord();
            _mockedAPIResponse.coord.lon = 50.00;
            _mockedAPIResponse.coord.lat = 50.00;

            _mockedAPIResponse.weather = new System.Collections.Generic.List<OpenWeatherAPIResponse.Weather>();
            _mockedAPIResponse.weather.Add(new OpenWeatherAPIResponse.Weather()
            {
                id = 50,
                main = "local.weather.main",
                description = "local.weather.description",
                icon = "local.weather.icon",
            });

            _mockedAPIResponse.main = new OpenWeatherAPIResponse.Main();
            _mockedAPIResponse.main.temp = 50.00;
            _mockedAPIResponse.main.feels_like = 50.00;
            _mockedAPIResponse.main.temp_min = 50.00;
            _mockedAPIResponse.main.temp_max = 50.00;
            _mockedAPIResponse.main.pressure = 50;
            _mockedAPIResponse.main.humidity = 50;

            _mockedAPIResponse.wind = new OpenWeatherAPIResponse.Wind();
            _mockedAPIResponse.wind.speed = 50.00;
            _mockedAPIResponse.wind.deg = 50;

            _mockedAPIResponse.clouds = new OpenWeatherAPIResponse.Clouds();
            _mockedAPIResponse.clouds.all = 50;

            _mockedAPIResponse.sys = new OpenWeatherAPIResponse.Sys();
            _mockedAPIResponse.sys.type = 1;
            _mockedAPIResponse.sys.id = 50;
            _mockedAPIResponse.sys.country = "local.country";
            _mockedAPIResponse.sys.sunrise = 50;
            _mockedAPIResponse.sys.sunset = 50;

            _mockedAPIResponse._base = "local._base";
            _mockedAPIResponse.visibility = 50;
            _mockedAPIResponse.dt = 50;
            _mockedAPIResponse.timezone = 50;
            _mockedAPIResponse.id = 50;
            _mockedAPIResponse.name = "local.name";
            _mockedAPIResponse.cod = 50;

            //foreach (System.ComponentModel.PropertyDescriptor descriptor in System.ComponentModel.TypeDescriptor.GetProperties(mockedAPIResponse))
            //{
            //    string name = descriptor.Name;
            //    object value = descriptor.GetValue(mockedAPIResponse);
            //    Console.WriteLine("{0}={1}", name, value);
            //}

            return _mockedAPIResponse;
        }
    }
}
