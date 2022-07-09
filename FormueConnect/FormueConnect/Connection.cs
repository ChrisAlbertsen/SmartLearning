using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;


namespace FormueConnect
{
    internal class Connection
    {

        private static SqlConnection? SqlConn = null;

        // handles potential variations of the string neeeded for authentication with SqlConnection
        // A dictionary with string keys and function values, which takes a dictionary as a parameter and returns a string
        private Dictionary<string, Func<Dictionary<string,string>, string>> AuthenticationStrings = new()
        {
            ["AADInteractive"] = credentials =>
            {
                string connectionString = $@"Server={credentials["server"]}
                ;Authentication=Active Directory Interactive
                ;Database={credentials["database"]}
                ;User Id={credentials["username"]}";

                return connectionString;
            }
        };

        public async void Authenticate(Dictionary<string,string> credentials, string authenticationProtocol)
        {
            string connectionString = AuthenticationStrings[authenticationProtocol](credentials);

            SqlConnection sqlConn = new(connectionString);

            try
            {
                await sqlConn.OpenAsync();
            }
            catch (SqlException e)
            {
                Trace.WriteLine("Did not login");
            }
            Trace.WriteLine("Connected successfully!");

            SqlConn = sqlConn;
        }

        public bool IsSqlConnNull()
        {
            return SqlConn == null;
        }

    }
}
