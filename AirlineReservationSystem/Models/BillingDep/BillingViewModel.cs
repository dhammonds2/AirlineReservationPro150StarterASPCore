using ClassLib.Interface;
using ClassLib.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
namespace AirlineReservationSystem.Models.Billing
{
    public class BillingViewModel
    {
        
        //public BillingViewModel(ClassLib.Logic.Bill billing) { }

        public int billingId { get; set; }
        public string arrivalCountry { get; set; }
        public string firstName { get; set; }
        public Ticket ticket { get; set; }
        public double farePaid { get; set; }
        public DateTime paymentDate { get; set; }
        public bool paymentStatus { get; set; }

        public BillingViewModel(Bill billing)
        {
            this.billingId = billing.billingId;
            this.arrivalCountry = billing.arrivalCountry;
            this.firstName = billing.firstName;
            this.farePaid = billing.grandTotal;
            this.paymentDate = billing.paymentDate;
            this.paymentStatus = billing.paymentStatus;
        }
    }
}
