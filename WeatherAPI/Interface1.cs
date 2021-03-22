using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeatherAPI
{
    interface iWeather
    {
        static int temp;
        static int winddegrees;
        static string winddirection;
        static string weather;
        //void GetWeather();
    }
}
