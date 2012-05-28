using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.SQSMessages
{
    public class PrivateMessageNotification : CraneChatReceiveMessageNotification
    {
        public PrivateMessageNotification(string messageBody = "",
            IEnumerable<MessageAttachment> attachments = null,
            string fromUser = "")
            : base(messageBody, attachments)
        {
            FromUser = fromUser;
        }

        public string FromUser { set; get; }
    }
}
