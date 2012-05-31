using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.SQSMessages
{
    [Serializable]
    public class CraneChatResponse : CraneChatMessage
    {
        public CraneChatResponse()
            : base()
        {
        }

        public Guid RequestGuid { get; set; }
    }
}
