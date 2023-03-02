using System;
using System.Collections.Generic;

using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace ClassLib.Data
{
    public class FullDatabase
    {
        private readonly string connectionString;
        public SqlConnection conn;
        public SqlCommand cmd;
        public SqlDataReader reader;

        public FullDatabase()
        {
            connectionString = "data source=(localdb)\\MSSQLLocalDB;initial catalog=Airline;Trusted_Connection=True";
        }

        public void databaseConnection(string query)
        {
            conn = new SqlConnection(connectionString);
            conn.Open();
            cmd = new SqlCommand(query, conn);
        }

        public void connClose()
        {
            conn = new SqlConnection(connectionString);
            conn.Close();
        }
    }
}
