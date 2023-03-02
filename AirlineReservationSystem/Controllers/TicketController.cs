using AirlineReservationSystem.Models;
using ClassLib.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClassLib.Data;
using System.Collections.Generic;
using ClassLib.Interface;
using System;
using System.Net.Sockets;
using Airline;
using Airline.Models;

namespace AirlineReservationSystem.Controllers
{
    public class TicketController : Controller
    {
        private readonly TicketContainer tc;
        private readonly Ticket ticket;
        private readonly FlightContainer fc;
        private readonly Flight flight;
        private readonly BillingContainer bc;
        private readonly Bill billing;
        public TicketController()
        {
            tc = new TicketContainer(new TicketDatabase());
            ticket = new Ticket(new TicketDatabase());
            fc = new FlightContainer(new FlightDatabase());
            flight = new Flight(new FlightDatabase());
            bc = new BillingContainer(new BillingDatabase());
            billing = new Bill(new BillingDatabase());
        }

        public IActionResult Search()
        {
            Customers loggedInCustomer = HttpContext.Session.getCustomer();

            FlightDetailView fdv = new FlightDetailView();
            fdv.Flights = new List<FlightView>();
            IEnumerable<Flight> flights = fc.getAllFlights();

            foreach (Flight flight in flights)
            {
                fdv.Flights.Add(new FlightView(flight));
            }
            return string.IsNullOrEmpty(loggedInCustomer.firstName) ? (IActionResult)RedirectToAction("Index", "Home") : View(fdv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(string searchString)
        {
            Customers loggedInCustomer = HttpContext.Session.getCustomer();

            FlightDetailView fdv = new FlightDetailView();
            fdv.Flights = new List<FlightView>();
            IEnumerable<Flight> flights = fc.searchFlight(searchString);

            foreach (Flight flight in flights)
            {
                fdv.Flights.Add(new FlightView(flight));
            }
            return string.IsNullOrEmpty(loggedInCustomer.firstName) ? (ActionResult)RedirectToAction("Index", "Home") : View(fdv);
        }

        public IActionResult Create()
        {
            Customers loggedInCustomer = HttpContext.Session.getCustomer();
            return string.IsNullOrEmpty(loggedInCustomer.firstName) ? (IActionResult)RedirectToAction("Register", "Account") : View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TicketView tvm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Customers loggedInCustomer = HttpContext.Session.getCustomer();
                    TicketObject ticketObject = new TicketObject
                    {
                        customerId = loggedInCustomer.customerId,
                        travelType = tvm.travelType,
                        classType = tvm.classType,
                        numberOfAdults = tvm.numberOfAdults,
                        numberOfChildren = tvm.numberOfChildren
                    };
                    int ticketId = ticket.saveTicketAndRetrieveId(new Ticket(ticketObject));

                    HttpContext.Session.SetString("ticketId", ticketId.ToString());
                    return RedirectToAction("Search");
                }
                catch
                {
                    return View("Index", "Home");
                }
            }
            return View();
        }


        public ActionResult Book(int id)
        {
            try
            {
                updateTicket(id);
                Customers loggedInCustomer = HttpContext.Session.getCustomer();
                Flight flight = fc.getFlightById(id);
                int ticketId = Convert.ToInt32(HttpContext.Session.GetString("ticketId"));
                Ticket ticket = tc.getTicketById(ticketId);
                CreateBilling(flight, ticket, loggedInCustomer);
                const int firstClassRate = 3;
                const double childrensRate = 0.75;
                double adultPrice;
                double childPrice;
                double totalPrice;
                if (ticket.classType == "First Class")
                {
                    adultPrice = flight.price * firstClassRate;
                    childPrice = flight.price * childrensRate * firstClassRate;
                    totalPrice = ticket.numberOfAdults * (flight.price * firstClassRate) + ticket.numberOfChildren * (flight.price * childrensRate * firstClassRate);
                }
                else
                {
                    adultPrice = flight.price;
                    childPrice = flight.price * childrensRate;
                    totalPrice = ticket.numberOfAdults * flight.price + ticket.numberOfChildren * (flight.price * childrensRate);
                }

                ViewBag.Customer = loggedInCustomer;
                ViewBag.Flight = flight;
                ViewBag.Ticket = ticket;
                ViewBag.AdultFarePrice = adultPrice;
                ViewBag.ChildFarePrice = childPrice;
                ViewBag.TotalPrice = totalPrice;
                return string.IsNullOrEmpty(loggedInCustomer.firstName) ? (ActionResult)RedirectToAction("Index", "Home") : View();
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
        private void CreateBilling(Flight flight, Ticket ticket, Customers customer)
        {
            const int firstClassRate = 3;
            const double childrensRate = 0.75;
            double totalPrice;
            if (ticket.classType == "First Class")
            {
                totalPrice = ticket.numberOfAdults * (flight.price * firstClassRate) + ticket.numberOfChildren * (flight.price * childrensRate * firstClassRate);
            }
            else
            {
                totalPrice = ticket.numberOfAdults * flight.price + ticket.numberOfChildren * (flight.price * childrensRate);
            }
            BillingObject billingDto = new BillingObject
            {
                flightId = flight.flightId,
                customerId = customer.customerId,
                ticketId = ticket.ticketId,
                grandTotal = totalPrice,
                paymentDate = DateTime.Now,
                paymentStatus = true
            };
            billing.saveBilling(new Bill(billingDto));
        }

        private void updateTicket(int id)
        {
            int ticketId = Convert.ToInt32(HttpContext.Session.GetString("ticketId"));
            var ticketdata = tc.getTicketById(ticketId);
            TicketObject ticketDto = new TicketObject
            {
                ticketId = ticketdata.ticketId,
                flightId = id,
                customerId = ticketdata.customerId,
                travelType = ticketdata.travelType,
                classType = ticketdata.classType,
                numberOfAdults = ticketdata.numberOfAdults,
                numberOfChildren = ticketdata.numberOfChildren
            };
            ticket.updateTicket(new Ticket(ticketDto));
        }


        // POST: TicketController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                billing.deleteBilling(id);
                return RedirectToAction("Dashboard", "Home");
            }
            catch
            {
                return RedirectToAction("Dashboard", "Home");
            }
        }
    }
}
