using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.SQSMessages
{
    [Serializable]
    public class SendGroupMessageResponse : CraneChatSendMessageResponse
    {
        public SendGroupMessageResponse()
            : base()
        {
        }
    }
}
