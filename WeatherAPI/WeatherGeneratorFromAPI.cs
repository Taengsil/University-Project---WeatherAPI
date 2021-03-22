using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeatherAPI
{
    class WeatherGeneratorFromAPI
    {
        static void Main()
        {
            /** generating cityname and statecode as empty strings for later use **/
            string CityName, StateCode;
            CityName = StateCode = string.Empty;

            /** reads user keyboard input and formats it properly **/
            ReadInput(ref CityName, ref StateCode);

            WeatherForecast WeatherData = new WeatherForecast();

            /** connects to API and generates WeatherData **/
            GetWeather(CityName, StateCode, ref WeatherData);

            /** generates output messages **/
            WorkWeatherData(CityName, WeatherData);
        }

        public static void ReadInput(ref string CityName, ref string StateCode)
        {
            Console.WriteLine("Enter a city name and a state code:");
            /** In the e-mail, the input is in one line. However, the user could type one line individually. The following will
            * check if the string is inputted in one go ("Chicago, IL") and split it into the correct variables, or if the user
            *
            * inserts them separately, the program will pick up the second line correctly. **/
            CityName = Console.ReadLine();
            string StateName;

            /** if the Cityname contains both Cityname and state name, split it and generate it
            * otherwise, read the state name
            **/
            if (CityName.Contains(' '))
            {
                string[] word = CityName.Split(' ');
                CityName = word[0];
                StateName = word[1];
            }
            else
            {
                StateName = Console.ReadLine();
            }
            /** transforms state abbreviation (AR) to state code (US-AR) **/
            StateCode = GetStateCode(StateName);
        }

        public static string GetStateCode(string StateName)
        {
            /** processing state abbreviation (AR) to state code (US-AR) **/
            StateName = StateName.ToUpper();

            /** openweather uses ISO 3166 codes, therefore we convert it to ISO 3166**/
            string StateCode = "US-" + StateName;
            return StateCode;
        }

        public static void GetWeather(string CityName, string StateCode, ref WeatherForecast WeatherData)
        {
            string baseUrl = "";
            GenerateUrl(CityName, StateCode, ref baseUrl);

            /** Downloading the JSON string to var json, then deserializing it as a dynamic object in var data; **/
            var json = Fetch(baseUrl).Result;
            var data = JsonConvert.DeserializeObject<dynamic>(json);

            /** if there is data, generate it following the WeatherData object instructions **/
            if (data != null)
            {
                GenerateWeatherData(ref WeatherData, ref data);
            }
            /** else, if there is no data, print "no data" **/
            else
            {
                Console.WriteLine("No data");
            }
        }

        public static void GenerateUrl(string CityName, string StateCode, ref string baseUrl)
        {   
            /** fetching api key **/
            string apiKey = "";
            apiKey = System.IO.File.ReadAllText(@"F:\Visual Studio Community Projects\WeatherAPI\api.txt");
            
            /** generating baseUrl **/
            string startUrl = "http://api.openweathermap.org/data/2.5/weather?q=";
            
            /** options must always have &appid= at the end**/
            string options = "&units=imperial&appid=";
            baseUrl = startUrl + CityName + "," + StateCode + options + apiKey;
        }

        static async Task<string> Fetch(string url)
        {
            /** fetching string from url**/
            using var client = new HttpClient();
            var content = await client.GetStringAsync(url);

            /** returning value as string**/
            return content;
        }

        public static void GenerateWeatherData(ref WeatherForecast WeatherData, ref dynamic data)
        {
            /** transforming from data object to WeatherData object**/
            WeatherData.temp = data.main.temp;
            WeatherData.winddegrees = data.wind.deg;
            WeatherData.weather = data.weather[0].main;
        }

        public static void WorkWeatherData(string CityName, WeatherForecast WeatherData)
        {
            /** formatting helper, just shows City Name and weather **/
            Console.WriteLine("\n"+CityName + " weather:");

            /** prints temperature in fahrenheit and celsius **/
            DetermineTemperature(WeatherData.temp);

            /** input: wind degrees, wind direction
             *  returns wind direction as string
             *  output: WeatherData.winddirection **/
            string winddirection ="";
            CalculateWindDirection(WeatherData.winddegrees, ref winddirection);


            Console.WriteLine("The wind is currently blowing {0}.\n", winddirection);

            /** based on the temperature and weather condition, sends a message that it is a nice day outside **/
            DetermineNiceDay(WeatherData.temp, WeatherData.weather);

            /** based on the temperature, sends a message that the user should use a coat **/
            DetermineCoat(WeatherData.temp);

            /** based on the value of the weather field, sends a message that the user should bring an umbrella **/
            DetermineUmbrella(WeatherData.weather);

            /** based on the value of the weather field, sends a message that it is cloudy **/
            DetermineClouds(WeatherData.weather);
        }

        public static void DetermineTemperature(int temperature)
        {
            /** conversion to celsius **/
            float tempcelsius = (temperature - 32) * 5 / 9;
            /** degrees fahrenheit + celsius print **/
            Console.WriteLine("It's currently " + temperature + " degrees Fahrenheit out there.\nThat's " + tempcelsius + " degrees Celsius.\n");
        }

        public static void DetermineNiceDay(int temperature, string weather)
        {
            /** threshold for nice day message set as variable**/
            string nicedaythreshold = "Clear Sky";

            if (temperature > 60 && weather == nicedaythreshold)
            {
                Console.WriteLine("It's a nice day outside.\n");
            }
        }

        public static void DetermineUmbrella (string weather)
        {
            /** defining what bad weather is for umbrella message **/
            string[] badweather = { "snow", "rain", "extreme" };
            if (weather.ToLower() == badweather[0] || weather.ToLower() == badweather[1] || weather.ToLower() == badweather[2])
            {
                Console.WriteLine("You should bring an umbrella.\n");
            }
        }
        
        public static void DetermineClouds (string weather)
        {
            /** defining what clouds are for cloudy message **/
            string cloudthreshold = "clouds";
            if (weather.ToLower() == cloudthreshold)
            {
                Console.WriteLine("It's cloudy outside.\n");
            }
        }

        public static void DetermineCoat (int temperature)
        {
            /** defines what coat threshold is for coat message **/
            int coatthreshold = 60;
            if (temperature < coatthreshold)
            {
                Console.WriteLine("You should bring a coat.\n");
            }
        }

        public static void CalculateWindDirection(int winddegrees, ref string winddirection)
        {
            /** checks which way the wind blows by comparing which part of the circle the orientation is**/
            if (winddegrees == 0)
            {
                /** |^ **/
                winddirection = "North";
            }
            else if (winddegrees > 0 && winddegrees < 90)
            {
                /** /^ **/
                winddirection = "North-East";
            }
            else if (winddegrees == 90)
            {
                /** -> **/
                winddirection = "East";
            }
            else if (winddegrees > 90 && winddegrees < 180)
            {
                /** \v **/
                winddirection = "South-East";
            }
            else if (winddegrees == 180)
            {
                /** |v **/
                winddirection = "South";
            }
            else if (winddegrees > 180 && winddegrees < 270)
            {
                /** /v **/
                winddirection = "South-West";
            }
            else if (winddegrees == 270)
            {
                /** <- **/
                winddirection = "West";
            }
            else if (winddegrees > 270 && winddegrees < 360)
            {
                /** \^ **/
                winddirection = "North-West";
            }
            else if (winddegrees == 360)
            {
                /** |^ **/
                winddirection = "North";
            }
            else winddirection = "Invalid wind direction data";
        }
    }
}
