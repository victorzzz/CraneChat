using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.SQSMessages
{
    [Serializable]
    public class SendBroadcastMessageResponse : CraneChatResponse
    {
        public SendBroadcastMessageResponse(string message = "") : base(message)
        {
        }
    }
}
