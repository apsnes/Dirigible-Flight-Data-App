using FlightApp.Database;
using FlightApp.Models.Response;
using System.Text.Json;

namespace FlightApp.Repository
{
    public interface IFlightApiRepository
    {
        Task<FlightResponseWrapper?> GetFlightByIata(string iata);
        Task<List<FlightResponse>> GetFlightByArrivalAirportActive(string arr_iata);
        Task<List<FlightResponse>> GetIncidentFlights();
        Task<List<FlightResponse>> GetFlightsByRoute(string dep_iata, string arr_iata);
        Task<List<FlightResponse>> GetFlightsByDepartureAirportActive(string dep_iata);
    }

    public class FlightApiRepository : IFlightApiRepository
    {
        public async Task<FlightResponseWrapper?> GetFlightByIata(string iata)
        {
            using (var client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Add("flight_iata", iata);
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

        public async Task<List<FlightResponse>> GetFlightByArrivalAirportActive(string arr_iata)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    using HttpResponseMessage response = await client.GetAsync($"https://api.aviationstack.com/v1/flights?access_key=7f8e9351d9959f9b49acd565d06a3571&arr_iata={arr_iata}&flight_status=active");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<FlightResponseWrapper>(responseBody).Data.ToList();
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }
            }
            return null;
        }

        public async Task<List<FlightResponse>> GetIncidentFlights()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    using HttpResponseMessage response = await client.GetAsync($"https://api.aviationstack.com/v1/flights?access_key=7f8e9351d9959f9b49acd565d06a3571&flight_status=incident");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<FlightResponseWrapper>(responseBody).Data.ToList();
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }
            }
            return null;
        }

        public async Task<List<FlightResponse>> GetFlightsByRoute(string dep_iata, string arr_iata)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    using HttpResponseMessage response = await client.GetAsync($"https://api.aviationstack.com/v1/flights?access_key=7f8e9351d9959f9b49acd565d06a3571&dep_iata={dep_iata}&arr_iata={arr_iata}");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<FlightResponseWrapper>(responseBody).Data.ToList();
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }
            }
            return null;
        }
        public async Task<List<FlightResponse>> GetFlightsByDepartureAirportActive(string dep_iata)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    using HttpResponseMessage response = await client.GetAsync($"https://api.aviationstack.com/v1/flights?access_key=7f8e9351d9959f9b49acd565d06a3571&dep_iata={dep_iata}&flight_status=active");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<FlightResponseWrapper>(responseBody).Data.ToList();
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
