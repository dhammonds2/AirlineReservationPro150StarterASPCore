using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ClassLib
{
    internal class DatabaseBig
    {
        private readonly string connectionString;
        public SqlConnection conn;
        public SqlCommand cmd;
        public SqlDataReader reader;

        public DatabaseBig()
        {
            connectionString = "Data Source=mssql.fhict.local;Initial Catalog=dbi463189_airline;Persist Security Info=True;User ID=dbi463189_airline;Password=m4VEw2tX;";
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
