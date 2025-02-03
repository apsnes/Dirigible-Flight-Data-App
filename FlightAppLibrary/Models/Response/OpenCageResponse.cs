using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlightAppLibrary.Models.Response
{
    public class OpenCageResponse
    {
        [JsonPropertyName("results")]
        public List<OpenCageResult> Results { get; set; }
    }
}
