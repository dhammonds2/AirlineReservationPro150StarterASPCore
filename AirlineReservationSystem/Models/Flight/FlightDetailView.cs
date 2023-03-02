using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Airline.Models
{
    public class FlightDetailView
    {
        public List<FlightView> Flights { get; set; }

        [BindProperty(SupportsGet = true)]
        public string searchString { get; set; }
    }
}
