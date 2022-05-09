using System;

namespace OpenWeatherAPIResponse
{
    public class WeatherConsole
    {
        /* Method for the messages */
        public static void ProcessWeatherData(string cityName, string stateCode, WeatherForecast weatherData)
        {
            /* formatting helper, just shows City Name and weather */
            Console.WriteLine("\n"+cityName + " weather:");

            /* prints temperature in fahrenheit and celsius */
            DetermineTemperature(weatherData.temp);

            /** input: wind degrees, wind direction
             *  returns wind direction as string
             *  output: WeatherData.winddirection **/
            weatherData.windDirection = CalculateWindDirection(weatherData.windDegrees);


            Console.WriteLine("The wind is currently blowing {0}.\n", weatherData.windDirection);

            /* based on the temperature and weather condition, sends a message that it is a nice day outside */
            DetermineNiceDay(weatherData.temp, weatherData.weather);

            /* based on the temperature, sends a message that the user should use a coat */
            DetermineCoat(weatherData.temp);

            /* based on the value of the weather field, sends a message that the user should bring an umbrella */
            DetermineUmbrella(weatherData.weather);

            /* based on the value of the weather field, sends a message that it is cloudy */
            DetermineClouds(weatherData.weather);
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
