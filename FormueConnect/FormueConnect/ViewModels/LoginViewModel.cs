using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormueConnect.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public Models.Credentials credentials { get; set; } = new(); // Password isn't handled here, so public is safe


        public void AskAuthenticate()
        {
            Connection.Authenticate(credentials, "AADInteractive");
        }

    }
}
