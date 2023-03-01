using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLib.Interface;
using ClassLib.Logic;

namespace ClassLib.Data_Access_Layer
{
    internal class BillingDalMsSql : DatabaseBig, IPersistDal<BillingDto>, IFetchDal<BillingDto>
    {
        public void delete(int billingId)
        {
            Database.execute("DELETE FROM Billing WHERE BillingId = @billingId",
                new
                {
                    billingId
                }
            );
        }

        public IEnumerable<BillingDto> getAllByCustomer(int customerId)
        {
            var billing = Database.query<BillingDto>(
                "SELECT Billing.BillingId, Customer.FirstName, Flight.ArrivalCountry, Billing.GrandTotal, Billing.PaymentDate, Billing.PaymentStatus FROM((Billing INNER JOIN Customer ON Billing.CustomerId = Customer.CustomerId) INNER JOIN Flight ON Billing.FlightId = Flight.FlightId) WHERE Billing.CustomerId = @customerId",
                new
                {
                    customerId
                });
            return billing;
        }


        public BillingDto getById(int id)
        {
            throw new NotImplementedException();
        }

        public void save(BillingDto data)
        {
            Database.execute(
                "INSERT INTO Billing (FlightId, CustomerId, TicketId, GrandTotal, PaymentDate, PaymentStatus) VALUES (@flightId, @customerId, @ticketId, @grandTotal, @paymentDate, @paymentStatus)",
                new
                {
                    data.flightId,
                    data.customerId,
                    data.ticketId,
                    data.grandTotal,
                    data.paymentDate,
                    data.paymentStatus
                }
            );
        }

        public IEnumerable<BillingDto> getAll() => Database.query<BillingDto>("SELECT * FROM Billing");
        public int saveGetId(BillingDto data)
        {
            int id = Database.executeScalar(
                "INSERT INTO Billing (FlightId, CustomerId, TicketId, GrandTotal, PaymentDate, PaymentStatus) VALUES (@flightId, @customerId, @ticketId, @grandTotal, @paymentDate, @paymentStatus); SELECT SCOPE_IDENTITY()",
                new
                {
                    data.flightId,
                    data.customerId,
                    data.ticketId,
                    data.grandTotal,
                    data.paymentDate,
                    data.paymentStatus
                }
            );
            return id;
        }

        public IEnumerable<BillingDto> search(string searchString)
        {
            throw new NotImplementedException();
        }

        public void update(BillingDto t)
        {
            throw new NotImplementedException();
        }

        public BillingDto verifyLogin(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
