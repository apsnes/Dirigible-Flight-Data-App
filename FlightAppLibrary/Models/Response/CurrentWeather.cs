using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlightAppLibrary.Models.Response
{
    public class CurrentWeather
    {
        [JsonPropertyName("temp_c")]
        public double TempC { get; set; }
        [JsonPropertyName("temp_f")]
        public double TempF { get; set; }
        [JsonPropertyName("is_day")]
        public int IsDay { get; set; }
        [JsonPropertyName("condition")]
        public WeatherCondition Condition { get; set; }
    }
}
