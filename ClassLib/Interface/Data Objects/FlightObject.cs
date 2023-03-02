using System;

namespace ClassLib.Interface
{
    public class FlightObject
    {
        public int flightId { get; set; }
        public string aircraftCode { get; set; }
        public string aircraftType { get; set; }
        public string departureCountry { get; set; }
        public string arrivalCountry { get; set; }
        public DateTime departureDate { get; set; }
        public DateTime arrivalDate { get; set; }
        public bool flightStatus { get; set; }
        public double price { get; set; }
    }
}
