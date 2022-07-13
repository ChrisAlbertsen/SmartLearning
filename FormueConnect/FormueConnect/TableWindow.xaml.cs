using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Data.SqlClient;

namespace FormueConnect
{

    public partial class TableWindow : Window
    {

        static private Connection SqlConn = new();
        public List<Table> TableList;

        public TableWindow()
        {
            InitializeComponent();
            BindTables();
        }

        private void BindTables()
        {
            TableList = SqlConn.ConstructTables();
            lbTableNames.DataContext = TableList;
        }

        private void OpenTable(object sender, SelectionChangedEventArgs args)
        {
            Table selectedTable = (Table)args.AddedItems[0];
            ColumnWindow columnWindow = new(selectedTable);
            columnWindow.Show();
        }
    }
}
