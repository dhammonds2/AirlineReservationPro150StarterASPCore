using ClassLib.Data;
using ClassLib.Interface;
using System.Collections.Generic;
using System.Linq;

namespace ClassLib.Logic
{
    public class FlightContainer
    {
        private readonly FitchData<FlightObject> flightContainer;

        public FlightContainer(FitchData<FlightObject> flightContainer)
        {
            this.flightContainer = flightContainer;
        }
        public IEnumerable<Flight> getAllFlights() => flightContainer.getAll().Select(flightDto => new Flight(flightDto));
        public IEnumerable<Flight> searchFlight(string searchString) => flightContainer.search(searchString).Select(flightDto => new Flight(flightDto));    
        public Flight getFlightById(int id) => new Flight(flightContainer.getById(id));
    }
}
