using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.SQSMessages
{
    [Serializable]
    public class CraneChatSendMessageRequest : CraneChatRequest
    {
        public CraneChatSendMessageRequest()
            : this("", "", "", null)
        {
        }

        public CraneChatSendMessageRequest(
            string userName = "", 
            string password = "",
            string messageBody = "",
            IEnumerable<MessageAttachment> attachments = null) : base (userName, password)
        {
            MessageBody = messageBody;
            if (null != attachments)
            {
                Attachments = new List<MessageAttachment>(attachments);
            }
        }

        public string MessageBody { get; set; }
        public List<MessageAttachment> Attachments { get; set; }

    }
}
