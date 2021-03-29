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

        async Task<DataClass> IWeatherService.GetWeatherData(string cityName, string stateCode, bool isSuccess)
        {
            
            string baseUrl = GenerateUrl(cityName, stateCode);
            var request = new HttpRequestMessage(HttpMethod.Get, baseUrl);

            using var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            /**/
            Type GetStaticType<T>(T x) { return typeof(T); }

            if (response.IsSuccessStatusCode)
            {
                // perhaps check some headers before deserialising
                /**/
                Console.WriteLine("{1}\n\n\n{0}", response, GetStaticType(response));
                isSuccess = true;

                try
                {
                    return await response.Content.ReadFromJsonAsync<DataClass>();
                }
                catch (NotSupportedException) // When content type is not valid
                {
                    Console.WriteLine("The content type is not supported.");
                }
                catch (JsonException) // Invalid JSON
                {
                    Console.WriteLine("Invalid JSON.");
                }
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

            var section = config.GetSection(nameof(WeatherClientConfig));
            var weatherClientConfig = section.Get<WeatherClientConfig>();

            /* generating baseUrl */
            string baseUrl = weatherClientConfig.WeatherAPIUrl + CityName + "," + StateCode + weatherClientConfig.Options + weatherClientConfig.apiKey;
            return baseUrl;
        }

        public static async Task PrintToJson(DataClass Json)
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


    }
}
