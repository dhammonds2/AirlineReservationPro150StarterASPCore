using ClassLib.Data;
using ClassLib.Interface;
using System.Collections.Immutable;

namespace AirlineReservationSystem.Reports
{
    public class MonthlyPassengerReport
    {
        public IEnumerable<CustomerObject> getAll() => Database.query<CustomerObject>("SELECT * FROM Customer ORDER BY FirstName");
        public CustomerObject getById(int customerId)
        {
            var result = Database.query<CustomerObject>(
                "SELECT * FROM Customer (FirstName, LastName, Email, PhoneNumber, DateOfBirth, Gender, Password, IsAdmin)",
                new
                {
                    customerId
                }
            ).ToImmutableList();

            return result.Count != 1 ? null : result.Single();
        }
    }
}
