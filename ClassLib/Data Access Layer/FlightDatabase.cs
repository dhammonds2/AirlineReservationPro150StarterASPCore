using ClassLib.Interface;
using ClassLib.Logic;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;


namespace ClassLib.Data 
{
    public class FlightDalMsSql : IPersistDal<FlightDto>, IFetchDal<FlightDto>
    {

        public IEnumerable<FlightDto> getAll() => Database.query<FlightDto>("SELECT * FROM Flight ORDER BY departureDate");
        public IEnumerable<FlightDto> search(string searchString) => Database.query<FlightDto>("SELECT * FROM Flight WHERE arrivalCountry LIKE '%" + searchString + "%'");

        public FlightDto getById(int flightId)
        {
            var result = Database.query<FlightDto>(
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

        public void save(FlightDto data)
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

        public void update(FlightDto data)
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
        public FlightDto verifyLogin(string email, string password)
        {
            throw new NotImplementedException();
        }

        public int saveGetId(FlightDto t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FlightDto> getAllByCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FlightDto> getAllByCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
