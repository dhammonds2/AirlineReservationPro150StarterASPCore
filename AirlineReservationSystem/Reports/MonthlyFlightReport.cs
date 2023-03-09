using ClassLib.Data;
using ClassLib.Interface;
using System.Collections.Immutable;

namespace AirlineReservationSystem.Reports
{
    public class MonthlyFlightReport
    {
        public IEnumerable<FlightObject> getAll() => Database.query<FlightObject>("SELECT * FROM Flight ORDER BY departureDate");
        public IEnumerable<FlightObject> search(string searchString) => Database.query<FlightObject>("SELECT * FROM Flight WHERE arrivalCountry LIKE '%" + searchString + "%'");

        public FlightObject getById(int flightId)
        {
            var result = Database.query<FlightObject>(
                "SELECT * FROM Flight (AircraftCode, AircraftType, DepartureCountry, ArrivalCountry, DepartureDate, ArrivalDate, FlightStatus, Price)",
                new
                {
                    flightId
                }
            ).ToImmutableList();

            return result.Count != 1 ? null : result.Single();
        }
    }
}
