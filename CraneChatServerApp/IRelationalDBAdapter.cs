using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.ServerApp
{
    interface IRelationalDBAdapter : IDisposable
    {
        void FollowUser(string requesterName, string userToFollow);
        void UnFollowUser(string requesterName, string userToUnFollow);
    }
}
