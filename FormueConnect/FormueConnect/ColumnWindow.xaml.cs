﻿using System;
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

namespace FormueConnect
{

    public partial class ColumnWindow : Window
    {

        public Table Table;
        static private Connection SqlConn = new();
        public List<Column> Columns { get; set; }

    public ColumnWindow(Table selectedTable)
        {
            InitializeComponent();
            Table = selectedTable;
            Columns = SqlConn.ConstructColumns(Table.Name);
            DataContext = Columns;
        }

        private void WriteClick(object sender, RoutedEventArgs e)
        {
            SqlConn.WriteColumns(Columns, Table.Name);
        }
    }
}
