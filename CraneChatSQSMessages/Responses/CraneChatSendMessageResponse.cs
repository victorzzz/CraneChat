using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.SQSMessages
{
    [Serializable]
    public abstract class CraneChatSendMessageResponse : CraneChatResponse
    {
        public CraneChatSendMessageResponse()
            : base() 
        {
        }
     }
}
