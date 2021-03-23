using System.Collections.Generic;

namespace WeatherAPI
{
    /** "{
     * \"coord\":
     *  {
     * \"lon\":-87.65,
     * \"lat\":41.85
     *  },
     * \"weather\":
     * [
     *  {
     *      \"id\":804,
     *      \"main\":\"Clouds\",
     *      \"description\":\"overcast clouds\",
     *      \"icon\":\"04d\"
     *  }
     * ],
     * \"base\":
     * \"stations\",
     * \"main\":
     * {\"temp\":53.33,
     * \"feels_like\":46.36,
     * \"temp_min\":53.01,
     * \"temp_max\":54,
     * \"pressure\":1014,
     * \"humidity\":58},
     * \"visibility\":10000,
     * \"wind\":
     *  {
     *      \"speed\":8.05,
     *      \"deg\":130
     *  },
     * \"clouds\":
     *  {
     *      \"all\":90
     *  },
     * \"dt\":1616505007,
     * \"sys\":
     *  {
     *      \"type\":1,
     *      \"id\":4861,\
     *      "country\":\"US\",
     *      \"sunrise\":1616500095,
     *      \"sunset\":1616544347},
     *      \"timezone\":-18000,
     *      \"id\":4887398,
     *      \"name\":\"Chicago\",
     *      \"cod\":200
     *  }"
     * **/

    // Dataclass data = JsonConvert.DeserializeObject<Dataclass>(json); 
    public class Coord
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Main
    {
        public double temp { get; set; }
        public double feels_like { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
    }

    public class Wind
    {
        public double speed { get; set; }
        public int deg { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class Sys
    {
        public int type { get; set; }
        public int id { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    public class Dataclass
    {
        public Coord coord { get; set; }
        public List<Weather> weather { get; set; }
        public string _base { get; set; }
        public Main main { get; set; }
        public int visibility { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public Sys sys { get; set; }
        public int timezone { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }
    }


}
