using FlightApp.Database;
using FlightAppLibrary.Models.Response;
using System.Text.Json;
using Microsoft.Extensions.Http;
using System.Web;

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
        private IHttpClientFactory _clientFactory;
        public FlightApiRepository(IHttpClientFactory ClientFactory)
        {
            _clientFactory = ClientFactory;
        }
        public async Task<FlightResponse?> GetFlightByIata(string iata)
        {
            using (var client = _clientFactory.CreateClient("FlightApi"))
            {
                try
                {
                    var builder = new UriBuilder(client.BaseAddress!.ToString());
                    var query = HttpUtility.ParseQueryString(builder.Query);
                    query["flight_iata"] = iata;
                    builder.Query = query.ToString();
                    using HttpResponseMessage response = await client.GetAsync(builder.ToString());
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
            using (var client = _clientFactory.CreateClient("FlightApi"))
            {
                try
                {
                    var builder = new UriBuilder(client.BaseAddress!.ToString());
                    var query = HttpUtility.ParseQueryString(builder.Query);
                    query["arr_iata"] = arr_iata;
                    builder.Query = query.ToString();
                    using HttpResponseMessage response = await client.GetAsync(builder.ToString());
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
            using (var client = _clientFactory.CreateClient("FlightApi"))
            {
                try
                {
                    var builder = new UriBuilder(client.BaseAddress!.ToString());
                    var query = HttpUtility.ParseQueryString(builder.Query);
                    query["flight_status"] = "incident";
                    builder.Query = query.ToString();
                    using HttpResponseMessage response = await client.GetAsync(builder.ToString());
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
            using (var client = _clientFactory.CreateClient("FlightApi"))
            {
                try
                {
                    var builder = new UriBuilder(client.BaseAddress!.ToString());
                    var query = HttpUtility.ParseQueryString(builder.Query);
                    query["dep_iata"] = dep_iata;
                    query["arr_iata"] = arr_iata;   
                    builder.Query = query.ToString();
                    using HttpResponseMessage response = await client.GetAsync(builder.ToString());
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
            using (var client = _clientFactory.CreateClient("FlightApi"))
            {
                try
                {
                    var builder = new UriBuilder(client.BaseAddress!.ToString());
                    var query = HttpUtility.ParseQueryString(builder.Query);
                    query["dep_iata"] = dep_iata;
                    builder.Query = query.ToString();
                    using HttpResponseMessage response = await client.GetAsync(builder.ToString());
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
