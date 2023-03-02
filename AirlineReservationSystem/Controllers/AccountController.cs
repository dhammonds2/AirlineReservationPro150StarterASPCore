using Airline;
using AirlineReservationSystem.Models;
using AirlineReservationSystem.Models.Customer;
using ClassLib.Data;
using ClassLib.Interface;
using ClassLib.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AirlineReservationSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly Customers customer;
        private readonly CustomerContainer cc;

        public AccountController()
        {
            customer = new Customers(new CustomerDatabase());
            cc = new CustomerContainer(new CustomerDatabase());
        }

        [HttpPost]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.logoutCustomer();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Login(CustomerLoginView customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Customers validCustomer = cc.verifyLogin(customer.email, customer.password);

                    logInCustomer(validCustomer);
                    return RedirectToAction("Dashboard", "Home");
                }
                catch
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: AccountController/Create
        public ActionResult Register()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(CustomerView customerViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CustomerObject customerDto = new CustomerObject
                    {
                        firstName = customerViewModel.firstName,
                        lastName = customerViewModel.lastName,
                        email = customerViewModel.email,
                        phoneNumber = customerViewModel.phoneNumber,
                        dateOfBirth = customerViewModel.dateOfBirth,
                        gender = customerViewModel.gender,
                        password = new PasswordHashing().passwordHash256(customerViewModel.password),
                        isAdmin = false
                    };

                    customer.saveCustomer(new Customers(customerDto));

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error in Encode: {ex.Message}");
                }

            }
            return View();
        }


        public ActionResult Update()
        {
            Customers validCustomer = HttpContext.Session.getCustomer();
            Customers customer = cc.getCustomerById(validCustomer.customerId);
            return View(new CustomerView(customer));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(CustomerView customerViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Customers validCustomer = HttpContext.Session.getCustomer();
                    CustomerObject customerDto = new CustomerObject
                    {
                        customerId = validCustomer.customerId,
                        firstName = customerViewModel.firstName,
                        lastName = customerViewModel.lastName,
                        email = customerViewModel.email,
                        phoneNumber = customerViewModel.phoneNumber,
                        dateOfBirth = customerViewModel.dateOfBirth,
                        gender = customerViewModel.gender,
                        password = new PasswordHashing().passwordHash256(customerViewModel.password)
                    };
                    customer.updateCustomer(new Customers(customerDto));

                    return RedirectToAction("Logout");
                }
                catch
                {
                    return View("Update");
                }
            }
            return RedirectToAction("Update");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                customer.deleteCustomer(id);

                return RedirectToAction("Customer", "Admin");
            }
            catch
            {
                return RedirectToAction("Customer", "Admin");
            }

        }

        private void logInCustomer(Customers validCustomer)
        {
            HttpContext.Session.updateCustomer(validCustomer);
        }

        private Customers updateCustomerInfo(CustomerObject customer)
        {
            Customers validCustomer = HttpContext.Session.getCustomer();
            HttpContext.Session.updateCustomer(validCustomer);
            return validCustomer;
        }
    }
}
