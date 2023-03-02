using ClassLib.Interface;
using ClassLib.Logic;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ClassLib.Data
{
    public class BillingDatabase : FullDatabase, PersistData<BillingObject>, FitchData<BillingObject>
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
        
        public IEnumerable<BillingObject> getAllByCustomer(int customerId)
        {
            var billing = Database.query<BillingObject>(
                "SELECT Billing.BillingId, Customer.FirstName, Flight.ArrivalCountry, Billing.GrandTotal, Billing.PaymentDate, Billing.PaymentStatus FROM((Billing INNER JOIN Customer ON Billing.CustomerId = Customer.CustomerId) INNER JOIN Flight ON Billing.FlightId = Flight.FlightId) WHERE Billing.CustomerId = @customerId",
                new
                {
                    customerId
                });
            return billing;
        }
        
           
        public BillingObject getById(int id)
        {
            throw new NotImplementedException();
        }

        public void save(BillingObject data)
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

        public IEnumerable<BillingObject> getAll() => Database.query<BillingObject>("SELECT * FROM Billing");
        public int saveGetId(BillingObject data)
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

        public IEnumerable<BillingObject> search(string searchString)
        {
            throw new NotImplementedException();
        }

        public void update(BillingObject t)
        {
            throw new NotImplementedException();
        }

        public BillingObject verifyLogin(string email, string password)
        {
            throw new NotImplementedException();
        }

        
    }
}
