using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlightAppLibrary.Models.Response
{
    public class WeatherResponse
    {
        [JsonPropertyName("location")]
        public WeatherLocation Location { get; set; }
        [JsonPropertyName("current")]
        public CurrentWeather CurrentWeather { get; set; }
        [JsonPropertyName("wind_mph")]
        public double WindMph { get; set; }
        [JsonPropertyName("wind_kph")]
        public double WindKph { get; set; }
        [JsonPropertyName("wind_degree")]
        public int WindDegree { get; set; }
        [JsonPropertyName("wind_dir")]
        public string WindDirection { get; set; }
        [JsonPropertyName("pressure_mb")]
        public int PressureMb { get; set; }
        [JsonPropertyName("pressure_in")]
        public double PressureIn { get; set; }
        [JsonPropertyName("precip_mm")]
        public int PrecipMm { get; set; }
        [JsonPropertyName("precip_in")]
        public int PrecipIn { get; set; }
        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }
        [JsonPropertyName("cloud")]
        public int Cloud { get; set; }
        [JsonPropertyName("feelslike_c")]
        public double FeelsLikeC { get; set; }
        [JsonPropertyName("feelslike_f")]
        public double FeelsLikeF { get; set; }
        [JsonPropertyName("windchill_c")]
        public double WindChillC { get; set; }
        [JsonPropertyName("windchill_f")]
        public double WindChillF { get; set; }
        [JsonPropertyName("heatindex_c")]
        public double HeatIndexC { get; set; }
        [JsonPropertyName("heatindex_f")]
        public double HeatIndexF { get; set; }
        [JsonPropertyName("dewpoint_c")]
        public double DewPointC { get; set; }
        [JsonPropertyName("dewpoint_f")]
        public double DewPointF { get; set; }
        [JsonPropertyName("vis_km")]
        public int VisibilityKm { get; set; }
        [JsonPropertyName("vis_miles")]
        public int VisibilityMiles { get; set; }
        [JsonPropertyName("uv")]
        public int Uv { get; set; }
        [JsonPropertyName("gust_mph")]
        public double GustMph { get; set; }
        [JsonPropertyName("gust_kph")]
        public double GustKph { get; set; }
    }
}
