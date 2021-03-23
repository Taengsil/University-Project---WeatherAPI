/* 
 * File Author: Ciorba Bogdan
 * Last Modified: 23.03.2021 14:27
**/ 


using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace WeatherAPI
{
    class WeatherGeneratorFromAPI
    {
        /* generating cityname and statecode as class variables for later use */
        private static string CityName;
        private static string StateCode;

        /* generating WeatherData as new WeatherForecast object */
        private static WeatherForecast WeatherData = new WeatherForecast();

        /* Reads user keyboard input and formats it properly */
        public static void ReadInput(string[] args)
        {
            string StateName="";
            bool notReadFromConsole = true;
            if (args[0] != null && args[1] != null)
            {
                CityName = args[0];
                StateName = args[1];
                notReadFromConsole = false;
                Console.WriteLine("City name has been read from console as {0}, State name has been read from console as {1}", CityName, StateName);
            }
            else
            {
                CityName = Console.ReadLine();
                Console.WriteLine("Enter a city name and a state code:");
            }
            /** in the e-mail, the input is in one line. However, the user could type one line individually. The following will
            * check if the string is inputted in one go ("Chicago, IL") and split it into the correct variables, or if the user
            * inserts them separately, the program will pick up the second line correctly. **/
            
            /** if the Cityname contains both Cityname and state name, split it and generate it
            * otherwise, read the state name
            **/
            if (CityName.Contains(' ') && notReadFromConsole)
            {
                string[] word = CityName.Split(' ');
                CityName = word[0];
                StateName = word[1];
            }
            else if (notReadFromConsole == true)
            {
                StateName = Console.ReadLine();
            }
            /* transforms state abbreviation (AR) to state code (US-AR) */
            StateCode = GetStateCode(StateName);
        }

        /** Processing state abbreviation (AR) to state code (US-AR) 
         Openweather uses ISO 3166 codes, therefore we convert it to ISO 3166 **/
        private static string GetStateCode(string StateName)
        {
            
            StateName = StateName.ToUpper();

            string StateCode = "US-" + StateName;
            return StateCode;
        }

        /* Downloading the JSON string to var json, then deserializing it as a dynamic object in var data; */
        public static async Task GetWeather()
        {
            string baseUrl = "";
            GenerateUrl(ref baseUrl);
            var json = Fetch(baseUrl).Result;
            await PrintData(json);
            var data = JsonConvert.DeserializeObject<dynamic>(json);

            /* if there is data, generate it following the WeatherData object instructions */
            if (data != null)
            {
                ExtractWeatherData(data);
            }
            /* else, if there is no data, print "no data" **/
            else
            {
                Console.WriteLine("No data");
            }
        }

        /* Fetching API Key, Generating baseUrl */
        private static void GenerateUrl(ref string baseUrl)
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

        /* Fetches string from url and returns value as string*/
        static async Task<string> Fetch(string url)
        {
            /* fetching string from url */
            using var client = new HttpClient();
            var content = await client.GetStringAsync(url);

            /* returning value as string */
            return content;
        }

        /* Prints the output of the data object as a pretty-printed json */
        private static async Task PrintData (string json)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string path = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName;
            string fileName = Path.Combine(path, "apidata.json");
            using FileStream createstream = File.Create(fileName);
            await System.Text.Json.JsonSerializer.SerializeAsync(createstream, json, options);
        }

        /* Transforming from data object to WeatherData object */
        private static void ExtractWeatherData(dynamic data)
        {
            
            WeatherData.temp = data.main.temp;
            WeatherData.winddegrees = data.wind.deg;
            WeatherData.weather = data.weather[0].main;
        }

        /* Method for the messages */
        public static void WorkWeatherData()
        {
            /* formatting helper, just shows City Name and weather */
            Console.WriteLine("\n"+CityName + " weather:");

            /* prints temperature in fahrenheit and celsius */
            DetermineTemperature(WeatherData.temp);

            /** input: wind degrees, wind direction
             *  returns wind direction as string
             *  output: WeatherData.winddirection **/
            string winddirection ="";
            CalculateWindDirection(WeatherData.winddegrees, ref winddirection);


            Console.WriteLine("The wind is currently blowing {0}.\n", winddirection);

            /* based on the temperature and weather condition, sends a message that it is a nice day outside */
            DetermineNiceDay(WeatherData.temp, WeatherData.weather);

            /* based on the temperature, sends a message that the user should use a coat */
            DetermineCoat(WeatherData.temp);

            /* based on the value of the weather field, sends a message that the user should bring an umbrella */
            DetermineUmbrella(WeatherData.weather);

            /* based on the value of the weather field, sends a message that it is cloudy */
            DetermineClouds(WeatherData.weather);
        }

        /* Prints out a message with the temperature in Fahrenheit and Celsius */
        private static void DetermineTemperature(int temperature)
        {
            /* conversion to celsius */
            float tempcelsius = (temperature - 32) * 5 / 9;
            /* degrees fahrenheit + celsius print */
            Console.WriteLine("It's currently " + temperature + " degrees Fahrenheit out there.\nThat's " + tempcelsius + " degrees Celsius.\n");
        }

        /* Sets a threshold for a nice day and then sends a message if the conditions meet the threshold */
        private static void DetermineNiceDay(int temperature, string weather)
        {
            /* threshold for nice day message set as variable */
            string nicedaythreshold = "Clear Sky";

            if (temperature > 60 && weather == nicedaythreshold)
            {
                Console.WriteLine("It's a nice day outside.\n");
            }
        }

        /* Defining what bad weather is for umbrella message */
        private static void DetermineUmbrella (string weather)
        {
            string[] badweather = { "snow", "rain", "extreme" };
            if (weather.ToLower() == badweather[0] || weather.ToLower() == badweather[1] || weather.ToLower() == badweather[2])
            {
                Console.WriteLine("You should bring an umbrella.\n");
            }
        }

        /* Defining what clouds are for cloudy message */
        private static void DetermineClouds (string weather)
        {
            string cloudthreshold = "clouds";
            if (weather.ToLower() == cloudthreshold)
            {
                Console.WriteLine("It's cloudy outside.\n");
            }
        }

        /* Defines what coat threshold is for coat message */
        private static void DetermineCoat (int temperature)
        {
            int coatthreshold = 60;
            if (temperature < coatthreshold)
            {
                Console.WriteLine("You should bring a coat.\n");
            }
        }

        /* Checks which way the wind blows by comparing which part of the circle the orientation is */
        private static void CalculateWindDirection(int winddegrees, ref string winddirection)
        {
            if (winddegrees == 0)
            {
                /* |^ */
                winddirection = "North";
            }
            else if (winddegrees > 0 && winddegrees < 90)
            {
                /* /^ */
                winddirection = "North-East";
            }
            else if (winddegrees == 90)
            {
                /* -> */
                winddirection = "East";
            }
            else if (winddegrees > 90 && winddegrees < 180)
            {
                /* \v */
                winddirection = "South-East";
            }
            else if (winddegrees == 180)
            {
                /* |v */
                winddirection = "South";
            }
            else if (winddegrees > 180 && winddegrees < 270)
            {
                /* /v */
                winddirection = "South-West";
            }
            else if (winddegrees == 270)
            {
                /* <- */
                winddirection = "West";
            }
            else if (winddegrees > 270 && winddegrees < 360)
            {
                /* \^ */
                winddirection = "North-West";
            }
            else if (winddegrees == 360)
            {
                /* |^ */
                winddirection = "North";
            }
                /* in case of the wind value somehow being above 360 degrees */
            else winddirection = "Invalid wind direction data";
        }
    }
}
