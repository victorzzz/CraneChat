using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.SQSMessages
{
    [Serializable]
    public class UnfollowUserRequest : CraneChatRequest
    {
        public UnfollowUserRequest()
            : this("", "", "")
        {
        }

        public UnfollowUserRequest(string userName = "", string password = "", string userToUnfollow = "")
            : base(userName, password)
        {
        }

        public string UserToUnfollow { get; set; }
    }
}
