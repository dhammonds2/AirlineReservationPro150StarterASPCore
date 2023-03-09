using ClassLib.Data;
using ClassLib.Interface;

namespace AirlineReservationSystem.Reports
{
    public class CurrentReservationReports
    {
        public int saveGetId(TicketObject data)
        {
            int id = Database.executeScalar(
                "SELECT * FROM Ticket (TravelType, CustomerId, ClassType) VALUES (@travelType, @customerId, @classType)",
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

    }
}
