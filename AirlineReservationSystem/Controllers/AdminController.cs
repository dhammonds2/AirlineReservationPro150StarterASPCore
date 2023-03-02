using Airline.Models;
using AirlineReservationSystem.Models;
using AirlineReservationSystem.Models.Customer;
using ClassLib.Data;
using ClassLib.Interface;
using ClassLib.Logic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
namespace AirlineReservationSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly FlightContainer fc;
        private readonly CustomerContainer cc;
        private readonly Flight flight;
        public AdminController()
        {
            fc = new FlightContainer(new FlightDatabase());
            cc = new CustomerContainer(new CustomerDatabase());
            flight = new Flight(new FlightDatabase());
        }

        // GET: Admin
        public IActionResult Flight()
        {
            FlightDetailView fdv = new FlightDetailView();
            fdv.Flights = new List<FlightView>();
            IEnumerable<Flight> flights = fc.getAllFlights();

            foreach (Flight flight in flights)
            {
                fdv.Flights.Add(new FlightView(flight));
            }

            return View(fdv);
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            Flight flight = fc.getFlightById(id);
            return View(new FlightView(flight));
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FlightView fvm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (DateTime.Compare(fvm.departureDate, fvm.arrivalDate) >= 0)
                    {
                        fvm.TimeIsNotValid = true;
                        return View(fvm);
                    }
                    FlightObject flightObject = new FlightObject
                    {
                        aircraftCode = fvm.aircraftCode,
                        aircraftType = fvm.aircraftType,
                        departureCountry = fvm.departureCountry,
                        arrivalCountry = fvm.arrivalCountry,
                        departureDate = fvm.departureDate,
                        arrivalDate = fvm.arrivalDate,
                        flightStatus = fvm.flightStatus,
                        price = fvm.price
                    };
                    flight.saveFlight(new Flight(flightObject));

                    return RedirectToAction("Flight", "Admin");
                }
                catch
                {
                    return View("Create", "Admin");
                }
            }
            return View();
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            Flight flight = fc.getFlightById(id);
            return View(new FlightView(flight));
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FlightView flightViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    FlightObject flightDto = new FlightObject
                    {
                        flightId = id,
                        aircraftCode = flightViewModel.aircraftCode,
                        aircraftType = flightViewModel.aircraftType,
                        departureCountry = flightViewModel.departureCountry,
                        arrivalCountry = flightViewModel.arrivalCountry,
                        departureDate = flightViewModel.departureDate,
                        arrivalDate = flightViewModel.arrivalDate,
                        flightStatus = flightViewModel.flightStatus,
                        price = flightViewModel.price
                    };

                    flight.updateFlight(new Flight(flightDto));

                    return RedirectToAction("Flight", "Admin");
                }
                catch
                {
                    return View("Edit", "Admin");
                }
            }
            return View();
        }


        // POST: Admin/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                flight.deleteFlight(id);

                return RedirectToAction("Flight", "Admin");
            }
            catch
            {
                return View("Flight", "Admin");
            }

        }

        public IActionResult Customer()
        {
            CustomerDetailView cdv = new CustomerDetailView();
            cdv.Customers = new List<CustomerView>();

            IEnumerable<Customers> customers = cc.getAllCustomers();

            foreach (Customers customer in customers)
            {
                cdv.Customers.Add(new CustomerView(customer));
            }

            return View(cdv);
        }
    }
}
