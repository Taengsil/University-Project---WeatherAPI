namespace WeatherAPI
{
    interface iWeather
    {
        static int temp;
        static int winddegrees;
        static string winddirection;
        static string weather;
        void GetWeather(string CityName, string StateCode);
    }
}
