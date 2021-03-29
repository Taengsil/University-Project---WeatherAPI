using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPI
{
    public class Program
    {
        async Task Main(string[] args)
        {
            string cityName=null;
            string stateCode=null;

            /* reads user keyboard input and formats it properly */
            ReadInput(args, ref cityName, ref stateCode);

            /* connects to API and generates WeatherData */
            IWeatherService WeatherForecastData = null;
            bool fetchingIsSuccess = false;
            await WeatherForecastData.GetWeatherData(cityName, stateCode, fetchingIsSuccess);

            /* generates output messages */
            if (fetchingIsSuccess)
            WeatherGeneratorFromAPI.WorkWeatherData(cityName, stateCode);
        }



        /* Reads user keyboard input and formats it properly */
        public static void ReadInput(string[] args, ref string CityName, ref string StateCode)
        {
            string StateName = "";
            bool notReadFromConsole = true;
            if (args[0] != null && args[1] != null)
            {
                CityName = args[0];
                StateName = args[1];
                notReadFromConsole = false;
                Console.WriteLine("City name has been read from console as {0}; State name has been read from console as {1}", CityName, StateName);
            }
            else if (args[0] == null && args[1] == null)
            {
                WeatherGeneratorFromAPI.CityName = Console.ReadLine();
                Console.WriteLine("Enter a city name and a state code:");
            }

            /** in the e-mail, the input is in one line. However, the user could type one line individually. The following will
            * check if the string is inputted in one go ("Chicago, IL") and split it into the correct variables, or if the user
            * inserts them separately, the program will pick up the second line correctly. **/

            /** if the Cityname contains both Cityname and state name, split it and generate it
            * otherwise, read the state name
            **/

            if (WeatherGeneratorFromAPI.CityName.Contains(' ') && notReadFromConsole == true)
            {
                string[] word = WeatherGeneratorFromAPI.CityName.Split(' ');
                WeatherGeneratorFromAPI.CityName = word[0];
                StateName = word[1];
            }
            else if (notReadFromConsole == true)
            {
                StateName = Console.ReadLine();
                /** if user only inputted CityName, but then adds a space. 
                * example input: 
                * Chicago
                * IL IL
                * **/
                if (StateName.Contains(' '))
                {
                    string[] word = WeatherGeneratorFromAPI.CityName.Split(' ');
                    StateName = word[0];
                }
            }
            /* transforms state abbreviation (AR) to state code (US-AR) */
            WeatherGeneratorFromAPI.StateCode = GetStateCode(StateName);
        }

        /** Processing state abbreviation (AR) to state code (US-AR) 
         Openweather uses ISO 3166 codes, therefore we convert it to ISO 3166 **/
        private static string GetStateCode(string StateName)
        {

            StateName = StateName.ToUpper();

            string StateCode = "US-" + StateName;
            return StateCode;
        }
    }
}
