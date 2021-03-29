using System;

namespace WeatherAPI
{
    public class WeatherConsoleOperations
    {
        /* Method for the messages */
        public static void WorkWeatherData(string CityName, string StateCode, WeatherForecast WeatherData)
        {
            /* formatting helper, just shows City Name and weather */
            Console.WriteLine("\n"+CityName + " weather:");

            /* prints temperature in fahrenheit and celsius */
            DetermineTemperature(WeatherData.temp);

            /** input: wind degrees, wind direction
             *  returns wind direction as string
             *  output: WeatherData.winddirection **/
            WeatherData.winddirection = CalculateWindDirection(WeatherData.winddegrees);


            Console.WriteLine("The wind is currently blowing {0}.\n", WeatherData.winddirection);

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
        private static void DetermineTemperature(double temperature)
        {
            /* conversion to celsius */
            double tempcelsius = (temperature - 32) * 5 / 9;
            tempcelsius = (double)System.Math.Round(tempcelsius, 2);
            /* degrees fahrenheit + celsius print */
            Console.WriteLine("It's currently " + temperature + " degrees Fahrenheit.\nThat's " + (tempcelsius) + " degrees Celsius.\n");
        }

        /* Sets a threshold for a nice day and then sends a message if the conditions meet the threshold */
        private static void DetermineNiceDay(double temperature, string weather)
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
        private static void DetermineCoat (double temperature)
        {
            int coatthreshold = 60;
            if (temperature < coatthreshold)
            {
                Console.WriteLine("You should bring a coat.\n");
            }
        }

        /* Checks which way the wind blows by comparing which part of the circle the orientation is */
        private static string CalculateWindDirection(int winddegrees)
        {
            if (winddegrees == 0)
            {
                /* |^ */
                return "North";
            }
            else if (winddegrees > 0 && winddegrees < 90)
            {
                /* /^ */
                return "North-East";
            }
            else if (winddegrees == 90)
            {
                /* -> */
                return "East";
            }
            else if (winddegrees > 90 && winddegrees < 180)
            {
                /* \v */
                return "South-East";
            }
            else if (winddegrees == 180)
            {
                /* |v */
                return "South";
            }
            else if (winddegrees > 180 && winddegrees < 270)
            {
                /* /v */
                return "South-West";
            }
            else if (winddegrees == 270)
            {
                /* <- */
                return "West";
            }
            else if (winddegrees > 270 && winddegrees < 360)
            {
                /* \^ */
                return "North-West";
            }
            else if (winddegrees == 360)
            {
                /* |^ */
                return "North";
            }
                /* in case of the wind value somehow being above 360 degrees */
            return "Error: invalid wind direction data";
        }
    }
}
