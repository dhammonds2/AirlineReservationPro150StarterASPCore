using ClassLib.Data;
using ClassLib.Interface;
using System;

namespace ClassLib.Logic
{
    public class Customers
    {
        public PersistData<CustomerObject> customer;
        public Customers(PersistData<CustomerObject> customer)
        {
            this.customer = customer;
        }
        public CustomerObject data { get; }

        public Customers(CustomerObject customerDto)
        {
            data = customerDto;
        }
        public int customerId => data.customerId;
        public string firstName => data.firstName;
        public string lastName => data.lastName;
        public string email => data.email;
        public string phoneNumber => data.phoneNumber;
        public DateTime dateOfBirth => data.dateOfBirth;
        public string gender => data.gender;
        public string password => data.password;
        public bool isAdmin => data.isAdmin;

        public void saveCustomer(Customers customer) => this.customer.save(customer.data);        
        public void updateCustomer(Customers customer) => this.customer.update(customer.data);       
        public void deleteCustomer(int id) => customer.delete(id);
    }   
}
