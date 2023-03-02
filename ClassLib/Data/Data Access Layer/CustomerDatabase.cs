using ClassLib.Interface;
using ClassLib.Logic;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data.SqlClient;
using System.Linq;


namespace ClassLib.Data
{
    public class CustomerDatabase : FullDatabase, PersistData<CustomerObject>, FitchData<CustomerObject>
    {
        public IEnumerable<CustomerObject> getAll() => Database.query<CustomerObject>("SELECT * FROM Customer ORDER BY FirstName");

        public void save(CustomerObject data)
        {
            Database.execute(
                "INSERT INTO Customer (FirstName, LastName, Email, PhoneNumber, DateOfBirth, Gender, Password, IsAdmin) VALUES (@firstName, @lastName, @email, @phoneNumber, @dateOfBirth, @gender, @password, @isAdmin)",
                new
                {
                    data.firstName,
                    data.lastName,
                    data.email,
                    data.phoneNumber,
                    data.dateOfBirth,
                    data.gender,
                    data.password,
                    data.isAdmin
                }
            );  
        }

        public CustomerObject getById(int customerId)
        {
            var result = Database.query<CustomerObject>(
                "SELECT * FROM Customer WHERE CustomerId = @customerId",
                new
                {
                    customerId
                }
            ).ToImmutableList();

            return result.Count != 1 ? null : result.Single();
        }

        public CustomerObject verifyLogin(string email, string password)
        {
            string query1 = "SELECT CustomerId FROM Customer WHERE Email='" + email + "' AND Password='" + password + "'";
            databaseConnection(query1);
            using (SqlCommand strQuery = new SqlCommand(query1, conn))
            {
                strQuery.Parameters.AddWithValue("@Email", email);
                strQuery.Parameters.AddWithValue("@Password", password);
                SqlDataReader dr = strQuery.ExecuteReader();
                if (dr.HasRows)
                {
                    string query2 = "SELECT * FROM Customer WHERE Email='" + email + "'";
                    databaseConnection(query2);
                    cmd.Parameters.AddWithValue("@Email", email);
                    CustomerObject customer = new CustomerObject();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        customer.customerId = (int)reader["CustomerId"];
                        customer.firstName = (string)reader["FirstName"];
                        customer.isAdmin = (bool)reader["IsAdmin"];
                        return customer;
                    }
                }
            }
            throw new Exception();
        }


        public void delete(int customerId)
        {
            Database.execute("DELETE FROM Billing WHERE CustomerId = @customerId",
                new
                {
                    customerId
                }
            );
            Database.execute("DELETE FROM Ticket WHERE CustomerId = @customerId",
                new
                {
                    customerId
                }
            );
            Database.execute("DELETE FROM Customer WHERE CustomerId = @customerId",
                new
                {
                    customerId
                }
            );
        }
        public void update(CustomerObject data)
        {
            Database.execute(
                "UPDATE Customer SET FirstName = @firstName, LastName = @lastName, Email = @email, PhoneNumber = @phoneNumber, DateOfBirth = @dateOfBirth, Gender = @gender, Password = @password WHERE CustomerId = @customerId",
                new
                {
                    data.customerId,
                    data.firstName,
                    data.lastName,
                    data.email,
                    data.phoneNumber,
                    data.dateOfBirth,
                    data.gender,
                    data.password
                }
            );
        }

        public IEnumerable<CustomerObject> search(string searchString)
        {
            throw new NotImplementedException();
        }

        public int saveGetId(CustomerObject t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CustomerObject> getAllByCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CustomerObject> getAllByCustomer(Customers customer)
        {
            throw new NotImplementedException();
        }
    }
}
