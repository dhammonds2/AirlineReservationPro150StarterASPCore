using ClassLib.Data;
using ClassLib.Interface;
using ClassLib.Logic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airline
{
    public static class SessionExtentions
    {
        public static void updateCustomer(this ISession session, Customers customer)
        {
            session.SetInt32("id", customer.customerId);
            session.SetString("firstName", customer.firstName);
            session.SetString("admin", customer.isAdmin.ToString().ToLower());
            setObject(session, "validCustomer", customer);
        }

        public static void setObject(this ISession session, string key, object value)
        {
            if (containsObject(session, key))
            {
                deleteObject(session, key);
            }
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static bool containsObject(this ISession session, string key)
        {
            return session.Get(key) != null;
        }
        public static void deleteObject(this ISession session, string key)
        {
            session.Remove(key);
        }
        public static void logoutCustomer(this ISession session)
        {
            if (containsObject(session, "validCustomer"))
            {
                session.SetInt32("id", 0);
                session.SetString("firstName", string.Empty);
                session.SetString("admin", "false");
                deleteObject(session, "validCustomer");
            }
            createCustomer(session);
        }
        public static void createCustomer(this ISession session)
        {
            if (!containsObject(session, "validCustomer"))
            {
                CustomerObject tempCustomer = new CustomerObject
                {
                    firstName = "",
                    isAdmin = false,
                };
                setObject(session, "validCustomer", tempCustomer);
            }
        }

        public static Customers getCustomer(this ISession session)
        {
            if (!string.IsNullOrEmpty(session.GetString("firstName")))
            {
                int id = (int)session.GetInt32("id");
                CustomerContainer customerContainer = new CustomerContainer(new CustomerDatabase());
                Customers validCustomer = customerContainer.getCustomerById(id);
                return validCustomer;
            }
            else
            {
                CustomerObject customerDto = new CustomerObject { firstName = "" };
                Customers inValidCustomer = new Customers(customerDto);

                return inValidCustomer;
            }
        }

        public static void resetCustomer(this ISession session)
        {
            if (containsObject(session, "validCustomer"))
            {
                session.SetInt32("id", 0);
                session.SetString("name", string.Empty);
                session.SetString("admin", "false");
                deleteObject(session, "validCustomer");
            }
            createCustomer(session);
        }
    }
}
