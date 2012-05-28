using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.SQSMessages
{
    [Serializable]
    public class LogoutResponse : CraneChatResponse
    {
        public LogoutResponse(string message = "") : base(message)
        {
        }

    }
}
