using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.SQSMessages
{
    [Serializable]
    public class SendPrivateMessageRequest : CraneChatSendMessageRequest
    {
        public SendPrivateMessageRequest()
            : this("", "", "", null, "")
        {
        }

        public SendPrivateMessageRequest(string userName = "", string password = "",
            string messageBody = "",
            IEnumerable<MessageAttachment> attachments = null, 
            string recipient = "")
            : base(userName, password, messageBody, attachments)
        {
            Recipient = recipient;
        }

        public string Recipient { get; set; }
    }
}
