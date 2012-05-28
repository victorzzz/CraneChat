using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.SQSMessages
{
    [Serializable]
    public class SendMessageToGroupRequest : CraneChatSendMessageRequest
    {
        public SendMessageToGroupRequest()
            : this("", "", "", null, "")
        {
        }

        public SendMessageToGroupRequest(
            string userName = "", 
            string password = "",
            string messageBody = "",
            IEnumerable<MessageAttachment> attachments = null,
            string groupName = null) : base (userName, password, messageBody, attachments)
        {
            GroupName = groupName;
        }

        public string GroupName { get; set; }
    }
}
