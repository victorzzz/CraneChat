using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.SQSMessages
{
    [Serializable]
    public class PingRequest : CraneChatRequest
    {
        public PingRequest()
            : this("", "")
        {
        }

        public PingRequest(string userName = "", string password = "")
            : base(userName, password)
        {
        }
    }
}