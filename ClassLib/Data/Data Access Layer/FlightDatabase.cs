using ClassLib.Interface;
using ClassLib.Logic;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;


namespace ClassLib.Data 
{
    public class FlightDatabase : PersistData<FlightObject>, FitchData<FlightObject>
    {

        public IEnumerable<FlightObject> getAll() => Database.query<FlightObject>("SELECT * FROM Flight ORDER BY departureDate");
        public IEnumerable<FlightObject> search(string searchString) => Database.query<FlightObject>("SELECT * FROM Flight WHERE arrivalCountry LIKE '%" + searchString + "%'");

        public FlightObject getById(int flightId)
        {
            var result = Database.query<FlightObject>(
                "SELECT * FROM Flight WHERE FlightId = @flightId",
                new
                {
                    flightId
                }
            ).ToImmutableList();

            return result.Count != 1 ? null : result.Single();
        }

        public void delete(int flightId)
        {
            Database.execute("DELETE FROM Flight WHERE FlightId = @flightId",
                new
                {
                    flightId
                }
            );
        }

        public void save(FlightObject data)
        {
            Database.execute(
                "INSERT INTO Flight (AircraftCode, AircraftType, DepartureCountry, ArrivalCountry, DepartureDate, ArrivalDate, FlightStatus, Price) VALUES (@aircraftCode, @AircraftType, @DepartureCountry, @ArrivalCountry, @DepartureDate, @ArrivalDate, @FlightStatus, @price)",
                new
                {
                    data.aircraftCode,
                    data.aircraftType,
                    data.departureCountry,
                    data.arrivalCountry,
                    data.departureDate,
                    data.arrivalDate,
                    data.flightStatus,
                    data.price
                }
            );
        }

        public void update(FlightObject data)
        {
            Database.execute(
                "UPDATE Flight SET AircraftCode = @aircraftCode, AircraftType = @aircraftType, DepartureCountry = @departureCountry, ArrivalCountry = @arrivalCountry, DepartureDate = @DepartureDate, ArrivalDate = @ArrivalDate, FlightStatus = @FlightStatus, Price = @price WHERE FlightId = @flightId",
                new
                {
                    data.flightId,
                    data.aircraftCode,
                    data.aircraftType,
                    data.departureCountry,
                    data.arrivalCountry,
                    data.departureDate,
                    data.arrivalDate,
                    data.flightStatus,
                    data.price
                }
            );
        }
        public FlightObject verifyLogin(string email, string password)
        {
            throw new NotImplementedException();
        }

        public int saveGetId(FlightObject t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FlightObject> getAllByCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FlightObject> getAllByCustomer(Customers customer)
        {
            throw new NotImplementedException();
        }
    }
}
