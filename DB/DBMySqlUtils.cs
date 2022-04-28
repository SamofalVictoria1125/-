using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Курсовая
{
    class DBMySqlUtils
    {

        static MySqlConnection connection = null;

        public static MySqlConnection
                GetDBConnection()
        {
            if (connection != null)
                return connection;

            string host = "127.0.0.1";
            int port = 3306;
            string database = "samofal";
            string username = "root";
            string pass = "love10105";

            String connString = "Server=" + host + ";Database=" + database
                + ";port=" + port + ";User Id=" + username + ";password="+pass;

            connection = new MySqlConnection(connString);

            return connection;
        }
    }
}
