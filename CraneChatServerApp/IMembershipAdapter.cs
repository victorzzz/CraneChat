using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.ServerApp
{
    interface IMembershipAdapter : IDisposable
    {
        bool ValidateUserCredamtial(string userName, string password);
    }
}
