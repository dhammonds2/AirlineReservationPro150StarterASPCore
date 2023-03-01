using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ClassLib
{
    internal class CompleteDatabase
    {
        private readonly string connectionString;
        public SqlConnection conn;
        public SqlCommand cmd;
        public SqlDataReader reader;

        public CompleteDatabase()
        {
            connectionString = "";
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
