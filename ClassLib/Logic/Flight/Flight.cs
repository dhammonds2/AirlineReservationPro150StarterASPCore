using ClassLib.Data;
using ClassLib.Interface;
using System;

namespace ClassLib.Logic
{
    public class Flight 
    {
        public PersistData<FlightObject> flight;
        public Flight(PersistData<FlightObject> flight)
        {
            this.flight = flight;
        }
        public FlightObject data { get; }

        public Flight(FlightObject flightDto)
        {
            data = flightDto;
        }
        public int flightId => data.flightId;
        public string aircraftCode => data.aircraftCode;
        public string aircraftType => data.aircraftType;
        public string departureCountry => data.departureCountry;
        public string arrivalCountry => data.arrivalCountry;
        public DateTime departureDate => data.departureDate;
        public DateTime arrivalDate => data.arrivalDate;
        public bool flightStatus => data.flightStatus;
        public double price => data.price;

        public void saveFlight(Flight flight) => this.flight.save(flight.data);      
        public void updateFlight(Flight flight) => this.flight.update(flight.data);
        public void deleteFlight(int id) => flight.delete(id);
        
    }
}

