using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.SQSMessages
{
    [Serializable]
    public class LogoutRequest : CraneChatRequest
    {
        public LogoutRequest()
            : this("", "")
        {
        }

        public LogoutRequest(string userName = "", string password = "") 
            : base(userName, password)
        {
        }
    }
}
