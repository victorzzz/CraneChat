using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web.Security;
using CraneChat.CoreLibrary;

namespace CraneChat.ServerApp
{
    class ASPMembershipAdapter : BaseDisposable, IMembershipAdapter
    {
        public ASPMembershipAdapter()
        {
        }

        public bool ValidateUserCredamtial(string userName, string password)
        {
            bool result = Membership.ValidateUser(userName, password);
            return result;
        }
    }
}
