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
        public string Id { get; set; }
        public Thumbnail Thumbnail { get; set; }
        [JsonPropertyName("thumbnail_large")]
        public Thumbnail ThumbnailLarge { get; set; }
        public string Link { get; set; }
        public string Photographer { get; set; }

    }
}
