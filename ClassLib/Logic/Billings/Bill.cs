using ClassLib.Data;
using ClassLib.Interface;
using System;

namespace ClassLib.Logic
{
    public class Bill
    {
        public PersistData<BillingObject> billing;
        public Bill(PersistData<BillingObject> billing)
        {
            this.billing = billing;
        }
        public BillingObject data { get; }

        public Bill(BillingObject billingDto)
        {
            data = billingDto;
        }
        public int billingId => data.billingId;
        public int FlightId => data.flightId;
        public int CustomerId => data.customerId;
        public int TicketId => data.ticketId;
        public string arrivalCountry => data.arrivalCountry;
        public string firstName => data.firstName;
        public double grandTotal => data.grandTotal;
        public DateTime paymentDate => data.paymentDate;
        public bool paymentStatus => data.paymentStatus;

        public void saveBilling(Bill billing) => this.billing.save(billing.data);
        public int saveBillingAndRetrieveId(Bill billing) => this.billing.saveGetId(billing.data);
        public void deleteBilling(int id) => this.billing.delete(id);
        public void updateBilling(Bill billing) => this.billing.update(billing.data);
    }
}

