using System;

namespace ClassLib.Interface
{
    public class BillingObject
    {
        public int billingId { get; set; }
        public int flightId { get; set; }
        public int customerId { get; set; }
        public int ticketId { get; set; }
        public string arrivalCountry { get; set; }
        public string firstName { get; set; }
        public double grandTotal { get; set; }
        public DateTime paymentDate { get; set; }
        public bool paymentStatus { get; set; }
    }
}
