using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlightAppLibrary.Models.AircraftPhoto
{
    public class Thumbnail
    {
        [JsonPropertyName("src")]
        public string Src {  get; set; }
        [JsonPropertyName("size")]
        public PhotoSize Size { get; set; }
    }
}
