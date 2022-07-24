using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using FormueConnect.Models;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Data.SqlClient;


namespace FormueConnect.Views
{

    public partial class TableWindow : Window
    {

        public List<Models.Table> TableList;

        public TableWindow()
        {
            InitializeComponent();
            ViewModels.TableViewModel tableViewModel = new();
            tableViewModel.ConstructTables();
            this.DataContext = tableViewModel;
        }

        private void OpenTable(object sender, SelectionChangedEventArgs args)
        {
            Table? selectedTable = (Table?)args.AddedItems[0];
            ColumnWindow columnWindow = new(selectedTable);
            columnWindow.Show();
        }
    }
}
