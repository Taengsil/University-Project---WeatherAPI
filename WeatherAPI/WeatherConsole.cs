using System;

namespace WeatherAPI
{
    public class WeatherConsole
    {
        /* Method for the messages */
        public static void ProcessWeatherData(string cityName, string stateCode, WeatherForecast weatherData)
        {
            Console.WriteLine(cityName + " weather:");
            DetermineTemperature(weatherData.temp);

            weatherData.windDirection = CalculateWindDirection(weatherData.windDegrees);
            Console.WriteLine("The wind is currently blowing {0}.\n", weatherData.windDirection);

            DetermineNiceDay(weatherData.temp, weatherData.weather);
            DetermineCoat(weatherData.temp, weatherData.weather);
            DetermineUmbrella(weatherData.weather);
            DetermineClouds(weatherData.weather);
        }

        /* Prints out a message with the temperature in Fahrenheit and Celsius */
        private static void DetermineTemperature(double temperature)
        {
            /* conversion to celsius */
            double tempcelsius = (temperature - 32) * 5 / 9;
            tempcelsius = (double)System.Math.Round(tempcelsius, 2);
            /* degrees fahrenheit + celsius print */
            Console.WriteLine("It's currently " + temperature + " degrees Fahrenheit.\nThat's " + (tempcelsius) + " degrees Celsius.");
        }

        /* Sets a threshold for a nice day and then sends a message if the conditions meet the threshold */
        private static void DetermineNiceDay(double temperature, string weather)
        {
            /* threshold for nice day message set as variable */
            string nicedaythreshold = "Clear Sky";

            if (temperature > 60 && weather == nicedaythreshold)
            {
                Console.WriteLine("It's a nice day outside.");
            }
            else if (temperature == 50 && weather == "local.weather.main")
            {
                Console.WriteLine("It's a very debuggy weather.");
            }
        }

        /* Defining what bad weather is for umbrella message */
        private static void DetermineUmbrella (string weather)
        {
            string[] badweather = { "snow", "rain", "extreme" };
            if (weather.ToLower() == badweather[0] || weather.ToLower() == badweather[1] || weather.ToLower() == badweather[2])
            {
                Console.WriteLine("You should bring an umbrella.");
            }
            else if (weather.ToLower() == "local.weather.main")
            {
                Console.WriteLine("You should bring a debugging umbrella.");
            }
        }

        /* Defining what clouds are for cloudy message */
        private static void DetermineClouds (string weather)
        {
            string cloudthreshold = "clouds";
            if (weather.ToLower() == cloudthreshold)
            {
                Console.WriteLine("It's cloudy outside.");
            }
            else if (weather.ToLower() == "local.weather.main")
            {
                Console.WriteLine("There are debugging clouds outside.");
            }
        }

        /* Defines what coat threshold is for coat message */
        private static void DetermineCoat (double temperature, string weather)
        {
            int coatthreshold = 60;
            if (temperature < coatthreshold && weather != "local.weather.main")
            {
                Console.WriteLine("You should bring a coat.");
            }
            else if (weather == "local.weather.main")
            {
                Console.WriteLine("You should bring a debugging coat");
            }
        }

        /* Checks which way the wind blows by comparing which part of the circle the orientation is */
        private static string CalculateWindDirection(int windDegrees)
        {
            if (windDegrees == 0)
            {
                // |^ 
                return "North";
            }
            else if (windDegrees > 0 && windDegrees < 90)
            {
                // /^ 
                return "North-East";
            }
            else if (windDegrees == 90)
            {
                // -> 
                return "East";
            }
            else if (windDegrees > 90 && windDegrees < 180)
            {
                // \v 
                return "South-East";
            }
            else if (windDegrees == 180)
            {
                // |v 
                return "South";
            }
            else if (windDegrees > 180 && windDegrees < 270)
            {
                // /v 
                return "South-West";
            }
            else if (windDegrees == 270)
            {
                // <-
                return "West";
            }
            else if (windDegrees > 270 && windDegrees < 360)
            {
                // \^ 
                return "North-West";
            }
            else if (windDegrees == 360)
            {
                // |^ 
                return "North";
            }
                /* in case of the wind value somehow being above 360 degrees */
            return "Error: invalid wind direction data";
        }
    }
}
