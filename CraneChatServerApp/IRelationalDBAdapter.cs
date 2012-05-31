using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.ServerApp
{
    interface IRelationalDBAdapter : IDisposable
    {
        bool FollowUser(string requesterName, string userToFollow);
        bool UnFollowUser(string requesterName, string userToUnFollow);

        IEnumerable<string> GetFollowers(string userName);
    }
}
