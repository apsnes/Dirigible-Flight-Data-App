using FlightApp.Database;
using FlightApp.Models.Response;
using System.Text.Json;

namespace FlightApp.Repository
{
    public interface IFlightApiRepository
    {
        Task<FlightResponseWrapper?> GetFlightByIata(string iata);
    }

    public class FlightApiRepository : IFlightApiRepository
    {
        public async Task<FlightResponseWrapper?> GetFlightByIata(string iata)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("flight_iata", iata);
                try
                {
                    using HttpResponseMessage response = await client.GetAsync($"https://api.aviationstack.com/v1/flights?access_key=7f8e9351d9959f9b49acd565d06a3571&flight_iata={iata}");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<FlightResponseWrapper>(responseBody);
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
