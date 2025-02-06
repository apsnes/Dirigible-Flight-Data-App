namespace FlightApp.Helpers
{
    public static class QueryHash
    {
        public static string CreateKey(string? arrivals, string? departures, string? flight_iata, string? date, int page_number, int page_size)
        {
            string key = "";
            if (arrivals is not null) key += arrivals;
            if (departures is not null) key = departures;
            if (flight_iata is not null) key += flight_iata;
            if (date is not null) key += date;
            key += page_number.ToString();
            key += page_size.ToString();
            return key;
        }   
    }
}
