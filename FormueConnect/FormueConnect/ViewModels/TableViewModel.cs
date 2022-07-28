using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Microsoft.Data.SqlClient;

namespace FormueConnect.ViewModels
{
    public class TableViewModel : ViewModelBase
    {
        public ObservableCollection<Models.Table> tables { get; set; } = new();
        
        public void ConstructTables()
        {
            string sqlString = $@"SELECT TABLE_SCHEMA, TABLE_NAME
                                FROM INFORMATION_SCHEMA.TABLES
                                WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG='{Connection.credentials.Database}'";

            
            Connection.ExecuteSqlDataReader(sqlString, delegate (SqlDataReader reader) {
                while (reader.Read())
                {
                    tables.Add(new Models.Table(reader.GetString(0) + "." + reader.GetString(1)));
                }
            });

        }

        public void TableClickHandler(Object selectionObj)
        {
            ViewModels.ColumnViewModel columnViewModel = new((Models.Table)selectionObj);
        }
    }
}
