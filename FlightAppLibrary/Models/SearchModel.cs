using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightAppLibrary.Models
{
    public class SearchModel
    {
        public string? FlightIATA { get; set; }
        public string? DepartureIATA { get; set; }
        public string? ArrivalIATA { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
