using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.SQSMessages
{
    public class GroupMessageNotification : CraneChatReceiveMessageNotification
    {
        public GroupMessageNotification(string messageBody = "",
            IEnumerable<MessageAttachment> attachments = null,
            string toGroup = "")
            : base(messageBody, attachments)
        {
            ToGroup = toGroup;
        }

        public string ToGroup { set; get; }
    }
}
