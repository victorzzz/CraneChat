using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.SQSMessages
{
    public abstract class CraneChatReceiveMessageNotification : CraneChatResponse
    {
        public CraneChatReceiveMessageNotification()
            : this("", null)
        {
        }

        public CraneChatReceiveMessageNotification(
            string messageBody = "",
            IEnumerable<MessageAttachment> attachments = null) : base ()
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
