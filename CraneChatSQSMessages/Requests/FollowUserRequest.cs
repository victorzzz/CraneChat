using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.SQSMessages
{
    [Serializable]
    public class FollowUserRequest : CraneChatRequest
    {
        public FollowUserRequest()
            : this("", "", "")
        {
        }

        public FollowUserRequest(string userName = "", string password = "", string userToFollow = "")
            : base(userName, password)
        {
            UserToFollow = userToFollow;
        }

        public string UserToFollow { get; set; }
    }
}
