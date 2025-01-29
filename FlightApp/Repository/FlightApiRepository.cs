using FlightApp.Database;
using System.Text.Json;

namespace FlightApp.Repository
{
    public interface IFlightApiRepository
    {
        Task<FlightResponse?> GetFlightByIata(string iata);
    }

    public class FlightApiRepository : IFlightApiRepository
    {
        public async Task<FlightResponse?> GetFlightByIata(string iata)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("access_key", "7f8e9351d9959f9b49acd565d06a3571");
                client.DefaultRequestHeaders.Add("flight_iata", iata);
                try
                {
                    using HttpResponseMessage response = await client.GetAsync("http://api.aviationstack.com/v1/flights");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<FlightResponse>(responseBody);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }
            }
            return null;
        }
    }
}
