using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.SQSMessages
{
    [Serializable]
    public class LoginRequest : CraneChatRequest
    {
        public LoginRequest() : this("", "")
        {
        }

        public LoginRequest(string userName = "", string password = "") : base(userName, password)
        {
        }
    }
}
