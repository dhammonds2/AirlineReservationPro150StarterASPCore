using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Text;

namespace ClassLib.Data
{
    public class Database
    {
        

        private static readonly SqlConnection connection =
             new SqlConnection("data source=(localdb)\\MSSQLLocalDB;initial catalog=Airline;Trusted_Connection=True");

         static Database()
         {
             connection.Open();
         }

         public static IEnumerable<T> query<T>(string sql, object parameters = null)
         {
            
             return connection.Query<T>(sql, parameters);
         }

         public static int execute(string sql, object parameters = null)
         {
             return connection.Execute(sql, parameters);
         }

        public static int executeScalar(string sql, object parameters = null)
        {
            return connection.ExecuteScalar<Int32>(sql, parameters);
        }
    }
}
