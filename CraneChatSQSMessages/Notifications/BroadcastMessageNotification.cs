using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.SQSMessages
{
    public class BroadcastMessageNotification : CraneChatReceiveMessageNotification
    {
        public BroadcastMessageNotification(string messageBody = "",
            IEnumerable<MessageAttachment> attachments = null,
            string fromUser = "")
            : base(messageBody, attachments)
        {
            FromUser = fromUser;
        }

        public string FromUser { set; get; }
    }
}
