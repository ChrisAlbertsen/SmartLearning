using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Data.SqlClient;

namespace FormueConnect
{
    public partial class LoginWindow : Window
    {


        public LoginWindow()
        {
            InitializeComponent();
        }


        private Connection Conn = new();


        private async void LoginClick(object sender, RoutedEventArgs e)
        {

            // User data from UI fields
            Dictionary<string, string> credentials = new()
            {
                ["username"] = Username.Text,
                ["server"] = Server.Text,
                ["database"] = Database.Text
            };

            await Conn.Authenticate(credentials, "AADInteractive");

            if (Conn.IsSqlConnNull()) {
                LoginError();
                return;
            }

            TableWindow tableWindow = new();
            this.Close();
            tableWindow.Show();
        }

        private void LoginError()
        {
            Trace.WriteLine("A login error has occured!!");
            // Show the user something related to the failed event.
        }
    }
}
