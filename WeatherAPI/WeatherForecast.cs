namespace WeatherAPI
{
    public class WeatherForecast : iWeather
    {
        public double temp { get; set; }
        public int winddegrees { get; set; }
        public string weather { get; set; }

        public void GetWeather()
        {
            ;
        }
    }
}
