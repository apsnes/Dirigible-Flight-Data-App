using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FlightAppFrontend.Shared.Extensions
{
    public static class HttpExtensions
    {
        public static async Task<(T? Value, string? ErrorMessage)> GetValueAsync<T>(this HttpClient client, string path)
        {
            var response = await client.GetAsync(path);

            string responseBody = await response.Content.ReadAsStringAsync();

            if(response.IsSuccessStatusCode)
            {
                return(JsonSerializer.Deserialize<T>(responseBody)!, null);
            }
            else
            {
                return(default, responseBody);
            }
        }
    }
}
