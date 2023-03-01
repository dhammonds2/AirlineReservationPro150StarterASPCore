using ClassLib.Interface;
using ClassLib.Logic;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ClassLib.Data
{
    public class TicketDalMsSql : IPersistDal<TicketDto>, IFetchDal<TicketDto>
    {
        public int saveGetId(TicketDto data)
        {
            int id = Database.executeScalar(
                "INSERT INTO Ticket (TravelType, CustomerId, ClassType, NumberOfAdults, NumberOfChildren) VALUES (@travelType, @customerId, @classType, @numberOfAdults, @numberOfChildren); SELECT SCOPE_IDENTITY()",
                new
                {
                    data.customerId,
                    data.travelType,
                    data.classType,
                    data.numberOfAdults,
                    data.numberOfChildren
                }
            );
            return id;
        }
        public IEnumerable<TicketDto> getAll() => Database.query<TicketDto>("SELECT * FROM Ticket");

        public void delete(int ticketId)
        {
            Database.execute(
                "DELETE FROM Ticket WHERE TicketId = @ticketId",
                new
                {
                    ticketId
                }
             );
        }


        public void update(TicketDto data)
        {
            Database.execute(
                "UPDATE Ticket SET FlightId = @flightId, TravelType = @travelType, ClassType = @classType, NumberOfAdults = @numberOfAdults, NumberOfChildren = @numberOfChildren WHERE TicketId = @ticketId",
                new
                {
                    data.ticketId,
                    data.customerId,
                    data.flightId,
                    data.travelType,
                    data.classType,
                    data.numberOfAdults,
                    data.numberOfChildren
                }
            );
        }


        public TicketDto getById(int ticketId)
        {
            var result = Database.query<TicketDto>(
                "SELECT * FROM Ticket WHERE TicketId = @ticketId",
                new
                {
                    ticketId
                }
            ).ToImmutableList();

            return result.Count != 1 ? null : result.Single();
        }

        public IEnumerable<TicketDto> search(string searchString)
        {
            throw new NotImplementedException();
        }

        public TicketDto verifyLogin(string email, string password)
        {
            throw new NotImplementedException();
        }

        public void save(TicketDto t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TicketDto> getAllByCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TicketDto> getAllByCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
