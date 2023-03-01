using ClassLib.Interface;
using ClassLib.Logic;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data.SqlClient;
using System.Linq;


namespace ClassLib.Data
{
    public class CustomerDetailsDatabase : CompleteDatabase, IPersistDal<CustomerDto>, IFetchDal<CustomerDto>
    {
        public IEnumerable<CustomerDto> getAll() => Database.query<CustomerDto>("SELECT * FROM Customer ORDER BY FirstName");

        public void save(CustomerDto data)
        {
            Database.execute(
                "INSERT INTO Customer (FirstName, LastName, Email, PhoneNumber, DateOfBirth, Gender, Password, IsAdmin) VALUES (@firstName, @lastName, @email, @phoneNumber, @dateOfBirth, @gender, @password, @isAdmin)",
                new
                {
                    data.firstName,
                    data.lastName,
                    data.email,
                    data.phoneNumber,
                    data.password,
                    data.isAdmin
                }
            );  
        }

        public CustomerDto getById(int customerId)
        {
            var result = Database.query<CustomerDto>(
                "SELECT * FROM Customer WHERE CustomerId = @customerId",
                new
                {
                    customerId
                }
            ).ToImmutableList();

            return result.Count != 1 ? null : result.Single();
        }

        public CustomerDto verifyLogin(string email, string password)
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
                    CustomerDto customer = new CustomerDto();
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
        public void update(CustomerDto data)
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
                    data.isAdmin,
                    data.password
                }
            );
        }

        public IEnumerable<CustomerDto> search(string searchString)
        {
            throw new NotImplementedException();
        }

        public int saveGetId(CustomerDto t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CustomerDto> getAllByCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CustomerDto> getAllByCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
