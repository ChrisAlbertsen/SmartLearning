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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace FormueConnect.Views
{

    public partial class TableWindow : Window
    {

        ViewModels.TableViewModel viewModel;

        public TableWindow()
        {
            InitializeComponent();
            viewModel = new();
            viewModel.ConstructTables();
            this.DataContext = viewModel;
        }

        private void TableClick(object sender, SelectionChangedEventArgs args)
        {
            viewModel.TableClickHandler(args.AddedItems[0]);
            //Models.Table selectedTable = (Models.Table)args.AddedItems[0];
            //ColumnWindow columnWindow = new(selectedTable);
            //columnWindow.Show();
        }
    }
}
