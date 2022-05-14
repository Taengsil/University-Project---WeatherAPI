namespace WeatherAPI
{
    class OpenWeatherAPIRequest
    {
        /// <summary>
        /// Dictates whether the program uses local fake values (false) or calls the web API (true)
        /// </summary>
        public bool isEnabled { get; set; }
        
        /// <summary>
        /// Base URL to call
        /// </summary>
        public string weatherAPIUrl { get; set; }
        
        /// <summary>
        /// Additional options
        /// </summary>
        public string options { get; set; }

        /// <summary>
        /// API key; Account-based, each user should get theirs from https://openweathermap.org/api
        /// </summary>
        public string apiKey { get; set; }
    }
}
