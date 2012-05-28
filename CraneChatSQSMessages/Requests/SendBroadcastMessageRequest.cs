using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.SQSMessages
{
    [Serializable]
    public class SendBroadcastMessageRequest : CraneChatSendMessageRequest
    {
        public SendBroadcastMessageRequest()
            : this("", "", "", null)
        {
        }

        public SendBroadcastMessageRequest(string userName = "", string password = "",
            string messageBody = "", IEnumerable<MessageAttachment> attachments = null)
            : base(userName, password, messageBody, attachments)
        {
        }
    }
}
