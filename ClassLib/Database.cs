using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace ClassLib
{
    internal class Database
    {
        private static readonly SqlConnection connection =
            new SqlConnection("");

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
