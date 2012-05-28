using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.SQSMessages
{
    [Serializable]
    public class SendPrivateMessageResponse : CraneChatSendMessageResponse
    {
        public SendPrivateMessageResponse()
            : base()
        {
        }
    }
}
