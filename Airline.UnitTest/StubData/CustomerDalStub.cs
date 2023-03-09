using Airline.UnitTest.ExceptionHandling;
using Airline.UnitTest.StubData;
using ClassLib.Data;
using ClassLib.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Airline.UnitTest.StubData
{
    class CustomerDalStub : PersistData<CustomerObject>, FitchData<CustomerObject>
    {
        private List<CustomerObject> customerDtoList = new List<CustomerObject>()
            {
            new CustomerObject(){ customerId=1, firstName="Today's", lastName="news", email="Today@live.com", phoneNumber="0658475960", dateOfBirth=new DateTime(2001, 5, 3, 0, 00, 00), gender="male", password="AF825CF4AF92B2DC5699F66ED1A4853F5870E7EBF89A4C4A4FF138C39D6109A1", isAdmin=false},
            new CustomerObject(){ customerId=2, firstName="Dylan", lastName="test", email="dylan@live.com", phoneNumber="0694836745", dateOfBirth=new DateTime(1988, 5, 3, 0, 00, 00), gender="female", password="53B945B392DFC50FBDA2B803A0F9E7388420F094665BD31A1AE22DBC3D1F5482", isAdmin=false},
            new CustomerObject(){ customerId=3, firstName="Testing", lastName="Testi", email="testing@gmail.com", phoneNumber="0694856384", dateOfBirth=new DateTime(1995, 7, 18, 0, 00, 00), gender="other", password="2AEB9015B14196CCA87359FE32103AC60593D5E9F78793298F001E2225B17877", isAdmin=false}
            };
        public CustomerObject verifyLogin(string email, string password)
        {
            CustomerObject customer = customerDtoList.Single(x => x.email == email);
            if (customer.email == email && customer.password == password)
            {
                return customer;
            }
            throw new CustomerLoginException(string.Format("Unable to find a customer by matching password", email));
        }


        public void delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CustomerObject> getAll() => customerDtoList;
        public CustomerObject getById(int id)
        {
            throw new NotImplementedException();
        }

        public void save(CustomerObject t)
        {
            throw new NotImplementedException();
        }

        public int saveGetId(CustomerObject t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CustomerObject> search(string searchString)
        {
            throw new NotImplementedException();
        }

        public void update(CustomerObject t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CustomerObject> getAllByCustomer(int id)
        {
            throw new NotImplementedException();
        }
    }
}
