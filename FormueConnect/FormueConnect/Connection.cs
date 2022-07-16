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
    internal static class Connection
    {
        private static string? ConnectionString;
        private static Credentials credentials;

        // handles potential variations of the string neeeded for authentication with SqlConnection
        // more functions could easily be added later
        private static Dictionary<string, Func<Credentials, string>> AuthenticationStrings = new()
        {
            ["AADInteractive"] = credentials =>
            {
                string connectionString = $@"Server={credentials.Server}
                ;Authentication=Active Directory Interactive
                ;Database={credentials.Database}
                ;User Id={credentials.Username}";

                return connectionString;
            }
        };

        public static void RunWithOpenSqlConnection(Action<SqlConnection> connectionCallBack)
        {
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(ConnectionString);
                connectionCallBack(conn);
            }
            catch
            {
                MessageBox.Show("Failed to connect!");
            }
            finally
            {
                if (conn != null) conn.Dispose();
            }
        }

        public static void ExecuteSqlDataReader(string sqlString, Action<SqlDataReader> readerCallBack)
        {
            RunWithOpenSqlConnection(delegate (SqlConnection conn)
            {
                SqlCommand cmd = null;
                SqlDataReader reader = null;
                try
                {
                    cmd = new(sqlString, conn);
                    reader = cmd.ExecuteReader();
                    readerCallBack(reader);
                }
                catch
                {
                    MessageBox.Show("Failed to execute query!");
                }
                finally
                {
                    if (reader != null) reader.Dispose();
                    if (cmd != null) cmd.Dispose();
                }
            });
        }

        public static void ExecuteSqlNonQuery(string sqlString)
        {
            RunWithOpenSqlConnection(delegate (SqlConnection conn)
            {
                SqlCommand cmd = null;
                try
                {
                    cmd = new(sqlString, conn);
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("Error in executing NonQuery");
                }
                finally
                {
                    if (cmd != null) cmd.Dispose();
                }
            });
        }

        public static void Authenticate(Credentials tempCredentials, string authenticationProtocol)
        {
            string tempConnectionString = AuthenticationStrings[authenticationProtocol](tempCredentials);

            SqlConnection SqlConn = null;
            try
            {
                SqlConn = new(tempConnectionString);
                ConnectionString = tempConnectionString;
                credentials = tempCredentials;
            }
            catch
            {
                MessageBox.Show("Failed to login!");
            }
            finally
            {
                if (SqlConn != null) SqlConn.Dispose();
            }
        }

        public static List<Table> ConstructTables()
        {
            string sqlString = $@"SELECT TABLE_SCHEMA, TABLE_NAME
                                FROM INFORMATION_SCHEMA.TABLES
                                WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG='{credentials.Database}'";
            List<Table> tableList = new();
            ExecuteSqlDataReader(sqlString, delegate(SqlDataReader reader) {
                while (reader.Read())
                {
                    Table tempTable = new(reader.GetString(0) + "." + reader.GetString(1));
                    tableList.Add(tempTable);
                }
            });

            return tableList;
        }

        public static List<Column> ConstructColumns(string Name)
        {
            string sqlString = $@"SELECT TOP 1 *
                                    FROM {Name}";

            List<Column> columns = new();
            ExecuteSqlDataReader(sqlString, delegate (SqlDataReader reader) {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Column tempColumn = new(reader.GetName(i), reader.GetDataTypeName(i));
                    columns.Add(tempColumn);
                }
            });         

            return columns;
        }

        public static void WriteColumns(List<Column> Columns, string Table)
        {
            string INSERT_INTO = $"INSERT INTO {Table} (";
            string VALUES = " VALUES (";
            Column tempCol;
            for (int i = 0; i < Columns.Count; i++)
            {
                tempCol = Columns[i];
                INSERT_INTO += tempCol.Name + ", ";
                VALUES += "'" + tempCol.UserInput + "', ";
            }
            char[] charsToTrim = { ',', ' ' };
            INSERT_INTO = INSERT_INTO.Trim(charsToTrim);
            VALUES = VALUES.Trim(charsToTrim);

            string sqlString = INSERT_INTO + ")" + VALUES + ");";

            ExecuteSqlNonQuery(sqlString); // implement delegate to allow for cleaning after execution
  
            MessageBox.Show("I wrote to the database");
        }
    }
}
