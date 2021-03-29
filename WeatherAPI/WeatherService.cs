using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherAPI
{
    public class WeatherService
    {
        private static HttpClient httpClient = new HttpClient();

        async Task<DataClass> GetWeatherData(string cityName, string stateCode, bool isSuccess)
        {

            string baseUrl = "";
            GenerateUrl(cityName, stateCode, ref baseUrl);
            var request = new HttpRequestMessage(HttpMethod.Get, baseUrl);

            using var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            if (response.IsSuccessStatusCode)
            {
                // perhaps check some headers before deserialising
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

        /* Fetching API Key, Generating baseUrl */
        private static void GenerateUrl(string CityName, string StateCode, ref string baseUrl)
        {
            /* fetching data from appsettings.json */
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();

            var section = config.GetSection(nameof(WeatherClientConfig));
            var weatherClientConfig = section.Get<WeatherClientConfig>();

            /* generating baseUrl */

            baseUrl = weatherClientConfig.WeatherAPIUrl + CityName + "," + StateCode + weatherClientConfig.Options + weatherClientConfig.apiKey;
        }

        public static async Task PrintToJson(IWeatherService Json)
        {
            /** The commented code uses System.Text.Json to create
             *  a serialized .JSON file, however it does not 
             *  pretty-print and replaces the '\n' character with
             *  '\u0020'                                          **/
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string path = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName;
            string fileName = Path.Combine(path, "apidata.json");
            //using (StreamWriter file = File.CreateText(fileName))
            //{
            //    Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
            //    serializer.Serialize(file, json);
            //}
            using FileStream createstream = File.Create(fileName);
            await JsonSerializer.SerializeAsync(createstream, Json, options);
        }


    }
}
