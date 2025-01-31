using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlightAppLibrary.Models.Dtos
{
    public class SignInResponseDTO
    {
        [JsonPropertyName("isAuthSuccessful")]
        public bool IsAuthSuccessful { get; set; }
        [JsonPropertyName("errorMessage")]
        public string ErrorMessage { get; set; }
        [JsonPropertyName("token")]
        public string Token { get; set; }
        [JsonPropertyName("userDTO")]
        public UserDTO UserDTO { get; set; }

    }
}
