﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlightAppLibrary.Models.Response
{
    public class WeatherCondition
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("icon")]
        public string Icon { get; set; }
        [JsonPropertyName("code")]
        public int Code { get; set; }
    }
}
