using AirlineReservationSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

using Microsoft.AspNetCore.Http;
using ClassLib.Logic;
using ClassLib.Interface;
using System.Collections.Generic;
using ClassLib.Data;
using AirlineReservationSystem.Models.Billing;
using Airline;
using Airline.Models;

namespace AirlineReservationSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly TicketContainer tc;
        private readonly BillingContainer bc;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            tc = new TicketContainer(new TicketDatabase());
            bc = new BillingContainer(new BillingDatabase());
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {

            return View();
        }
        public IActionResult Flight()
        {

            return View();
        }

        public IActionResult Dashboard()
        {
            Customers loggedInCustomer = HttpContext.Session.getCustomer();
            ViewBag.User = loggedInCustomer;

            BillingDetailView bdv = new BillingDetailView();
            bdv.billings = new List<BillingViewModel>();
            IEnumerable<Bill> billings = bc.getAllBillingsById(loggedInCustomer.customerId);

            foreach (Bill billing in billings)
            {
                bdv.billings.Add(new BillingViewModel(billing));
            }

            return string.IsNullOrEmpty(loggedInCustomer.firstName) ? (IActionResult)RedirectToAction("Index", "Home") : View(bdv);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
