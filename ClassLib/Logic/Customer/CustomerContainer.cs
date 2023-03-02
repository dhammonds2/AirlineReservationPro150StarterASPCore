using ClassLib.Data;
using ClassLib.Interface;
using System.Collections.Generic;
using System.Linq;

namespace ClassLib.Logic
{
    public class CustomerContainer
    {
        private readonly FitchData<CustomerObject> customerContainer;

        public CustomerContainer(FitchData<CustomerObject> customerContainer)
        {
            this.customerContainer = customerContainer;
        }
        public Customers getCustomerById(int id) => new Customers(customerContainer.getById(id));
        public Customers verifyLogin(string email, string password) => new Customers(customerContainer.verifyLogin(email, new PasswordHashing().passwordHash256(password)));
        public IEnumerable<Customers> getAllCustomers() => customerContainer.getAll().Select(customerDto => new Customers(customerDto));

    }
}
