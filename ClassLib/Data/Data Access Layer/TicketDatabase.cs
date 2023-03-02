using ClassLib.Interface;
using ClassLib.Logic;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ClassLib.Data
{
    public class TicketDatabase : PersistData<TicketObject>, FitchData<TicketObject>
    {
        public int saveGetId(TicketObject data)
        {
            int id = Database.executeScalar(
                "INSERT INTO Ticket (TravelType, CustomerId, ClassType) VALUES (@travelType, @customerId, @classType); SELECT SCOPE_IDENTITY()",
                new
                {
                    data.customerId,
                    data.travelType,
                    data.classType,
                   
                }
            );
            return id;
        }
        public IEnumerable<TicketObject> getAll() => Database.query<TicketObject>("SELECT * FROM Ticket");

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


        public void update(TicketObject data)
        {
            Database.execute(
                "UPDATE Ticket SET FlightId = @flightId, TravelType = @travelType, ClassType = @classType WHERE TicketId = @ticketId",
                new
                {
                    data.ticketId,
                    data.customerId,
                    data.flightId,
                    data.travelType,
                    data.classType
                }
            );
        }


        public TicketObject getById(int ticketId)
        {
            var result = Database.query<TicketObject>(
                "SELECT * FROM Ticket WHERE TicketId = @ticketId",
                new
                {
                    ticketId
                }
            ).ToImmutableList();

            return result.Count != 1 ? null : result.Single();
        }

        public IEnumerable<TicketObject> search(string searchString)
        {
            throw new NotImplementedException();
        }

        public TicketObject verifyLogin(string email, string password)
        {
            throw new NotImplementedException();
        }

        public void save(TicketObject t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TicketObject> getAllByCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TicketObject> getAllByCustomer(Customers customer)
        {
            throw new NotImplementedException();
        }
    }
}
