using FlightApp.Database;
using FlightAppLibrary.Models.Response;
using System.Text.Json;

namespace FlightApp.Repository
{
    public interface IFlightApiRepository
    {
        Task<FlightResponse?> GetFlightByIata(string iata);
        Task<List<FlightResponse>> GetFlightByArrivalAirportActive(string arr_iata);
        Task<List<FlightResponse>> GetIncidentFlights();
        Task<List<FlightResponse>> GetFlightsByRoute(string dep_iata, string arr_iata);
        Task<List<FlightResponse>> GetFlightsByDepartureAirportActive(string dep_iata);
    }

    public class FlightApiRepository : IFlightApiRepository
    {
        string key = "fb7c292b632cfe66d517705b143c0dc9";

        public async Task<FlightResponse?> GetFlightByIata(string iata)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    using HttpResponseMessage response = await client.GetAsync($"https://api.aviationstack.com/v1/flights?access_key={key}&flight_iata={iata}");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<FlightResponseWrapper>(responseBody)!.Data.FirstOrDefault();
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
                    using HttpResponseMessage response = await client.GetAsync($"https://api.aviationstack.com/v1/flights?access_key={key}&arr_iata={arr_iata}&flight_status=active");
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
                    using HttpResponseMessage response = await client.GetAsync($"https://api.aviationstack.com/v1/flights?access_key={key}&flight_status=incident");
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
                    using HttpResponseMessage response = await client.GetAsync($"https://api.aviationstack.com/v1/flights?access_key={key}&dep_iata={dep_iata}&arr_iata={arr_iata}");
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
                    using HttpResponseMessage response = await client.GetAsync($"https://api.aviationstack.com/v1/flights?access_key={key}&dep_iata={dep_iata}&flight_status=active");
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
