using FormueConnect.Models;
using System.Diagnostics;
using System.Windows;

namespace FormueConnect.Views
{
    public partial class LoginWindow : Window
    {

        public Credentials credentials { get; set; } = new(); // Password isn't handled here, so public is safe
        private ViewModels.LoginViewModel viewModel;

        public LoginWindow()
        {
            InitializeComponent();
            viewModel = new ViewModels.LoginViewModel();
            this.DataContext = viewModel;
        }


        private void LoginClick(object sender, RoutedEventArgs e)
        {
            viewModel.AskAuthenticate();
            TableWindow tableWindow = new();
            this.Close();
            tableWindow.Show();
        }
    }
}
