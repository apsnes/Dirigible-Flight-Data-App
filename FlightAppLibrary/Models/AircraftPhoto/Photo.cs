using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlightAppLibrary.Models.AircraftPhoto
{
    public class Photo
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("thumbnail")]
        public Thumbnail Thumbnail { get; set; }
        [JsonPropertyName("thumbnail_large")]
        public Thumbnail ThumbnailLarge { get; set; }
        [JsonPropertyName("link")]
        public string Link { get; set; }
        [JsonPropertyName("photographer")]
        public string Photographer { get; set; }

    }
}
