using ClassLib.Logic;
using System;
using System.ComponentModel.DataAnnotations;

namespace Airline.Models
{
    public class FlightView
    {
        public bool TimeIsNotValid { get; set; }
        public FlightView(){}

        [Key]
        public int flightId { get; set; }

        [Required(ErrorMessage = "An aircraft code is required to proceed!")]
        public string aircraftCode { get; set; }

        [Required(ErrorMessage = "An aircraft type is required to proceed!")]        
        public string aircraftType { get; set; }

        [Required(ErrorMessage = "A departure country is required to proceed!")]
        public string departureCountry { get; set; }

        
        public string arrivalCountry { get; set; }

        [Required(ErrorMessage = "A departure date is required to proceed!")]
        [CurrentDate(ErrorMessage = "departure date must be in the future!")]
        public DateTime departureDate { get; set; }

        [Required(ErrorMessage = "An arrival date is required to proceed!")]
        [MaxDate(ErrorMessage = "Arrival Time is less than a day!")]
        public DateTime arrivalDate { get; set; }

        [Required(ErrorMessage = "A flight status is required to proceed!")]
        public bool flightStatus { get; set; }

        [Required(ErrorMessage = "A price is required to proceed!")]
        public double price { get; set; }
              
        public FlightView(Flight flight)
        {
            this.flightId           = flight.flightId;
            this.aircraftCode       = flight.aircraftCode;
            this.aircraftType       = flight.aircraftType;
            this.departureCountry   = flight.departureCountry;
            this.arrivalCountry     = flight.arrivalCountry;
            this.departureDate      = flight.departureDate;
            this.arrivalDate        = flight.arrivalDate;
            this.flightStatus       = flight.flightStatus;
            this.price              = flight.price;
        }
    }
}
