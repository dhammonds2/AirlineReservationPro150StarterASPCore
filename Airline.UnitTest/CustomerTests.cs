using NUnit.Framework;
using System;
using ClassLib.Logic;
using Airline.UnitTest.StubData;
using Airline.UnitTest.ExceptionHandling;

namespace Airline.UnitTest
{
    class CustomerTests
    {
        [Test]
        public void verifyLogin_MockCustomers_AreEqual()
        {
            // Arrange
            CustomerDalStub customerDalStub = new();
            CustomerContainer customerContainer = new(customerDalStub);
            Customers customer = new(customerDalStub);

            string validEmail1 = "today@live.com";
            string validPassword1 = "Today52!";

            string validEmail2 = "dylan@live.com";
            string invalidPassword1 = "randomTest2";

            string invalidEmail1 = "testing@gmail.com";
            string validPassword2 = "TResaf12!";

            string validEmail3 = "mkmkms@game.net";
            string validPassword3 = "Doesit1!";

            // Act & Assert
            Assert.DoesNotThrow(() => customerContainer.verifyLogin(validEmail1, validPassword1));
            Assert.Catch<CustomerLoginException>(() => customerContainer.verifyLogin(validEmail2, invalidPassword1));
            Assert.Catch<InvalidOperationException>(() => customerContainer.verifyLogin(invalidEmail1, validPassword2));
            Assert.DoesNotThrow(() => customerContainer.verifyLogin(validEmail3, validPassword3));

        }
    }
}