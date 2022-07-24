using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using FormueConnect.Models;

namespace FormueConnect.Views
{

    public partial class ColumnWindow : Window
    {

        public Models.Table Table;
        public List<Column> Columns { get; set; }

    public ColumnWindow(Models.Table selectedTable)
        {
            InitializeComponent();
            Table = selectedTable;
            Columns = Connection.ConstructColumns(Table.Name);
            DataContext = Columns;
        }

        private void WriteClick(object sender, RoutedEventArgs e)
        {
            Connection.WriteColumns(Columns, Table.Name);
        }
    }
}
