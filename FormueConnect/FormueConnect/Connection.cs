using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;


namespace FormueConnect
{
    internal class Connection
    {

        private static SqlConnection? SqlConn = new();

        // handles potential variations of the string neeeded for authentication with SqlConnection
        // more functions could easily be added later
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


        public Task Authenticate(Dictionary<string,string> credentials, string authenticationProtocol)
        {
            string connectionString = AuthenticationStrings[authenticationProtocol](credentials);
            SqlConn.ConnectionString = connectionString;
            return SqlConn.OpenAsync();
        }


        public bool IsSqlConnNull()
        {
            return SqlConn == null;
        }

        public List<Table> ConstructTables()
        {
            string queryString = $@"SELECT TABLE_SCHEMA, TABLE_NAME
                                FROM INFORMATION_SCHEMA.TABLES
                                WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG='{SqlConn.Database}'";

            SqlCommand command = new(queryString, SqlConn);
            SqlDataReader reader = command.ExecuteReader();

            List<Table> tableList = new();
            while (reader.Read())
            {
                Table tempTable = new(reader.GetString(0) + "." + reader.GetString(1));
                tableList.Add(tempTable);
            }
            return tableList;
        }

        public List<Column> ConstructColumns(string Name)
        {
            string queryString = $@"SELECT TOP 1 *
                                    FROM {Name}";

            SqlCommand command = new(queryString, SqlConn);
            SqlDataReader reader = command.ExecuteReader();

            List<Column> columns = new();
            
            for(int i = 0; i < reader.FieldCount; i++) { 
                Column tempColumn = new(reader.GetName(i), reader.GetDataTypeName(i));
                columns.Add(tempColumn);
            }

            return columns;
        }

        public void WriteColumns(List<Column> Columns, string Table)
        {
            string INSERT_INTO = $"INSERT INTO {Table} (";
            string VALUES = " VALUES (";
            Column tempCol;
            for(int i=0; i < Columns.Count; i++)
            {
                tempCol = Columns[i];
                INSERT_INTO += tempCol.Name + ", ";
                VALUES += "'" + tempCol.UserInput + "', ";
            }
            char[] charsToTrim = {',',' '};
            INSERT_INTO = INSERT_INTO.Trim(charsToTrim);
            VALUES = VALUES.Trim(charsToTrim);
            string queryString = INSERT_INTO + ")" + VALUES + ");";

            SqlCommand command = new(queryString, SqlConn);
            command.ExecuteNonQuery();
            MessageBox.Show("I wrote to the database");
        }
    }
}
