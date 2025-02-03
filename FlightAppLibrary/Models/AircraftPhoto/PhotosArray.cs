using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlightAppLibrary.Models.AircraftPhoto
{
    public class PhotosArray
    {
        [JsonPropertyName("photos")]
        public Photo[]? Photos {  get; set; }
    }
}
