using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.SQSMessages
{
    [Serializable]
    public abstract class CraneChatResponse : CraneChatMessage
    {
        protected CraneChatResponse()
            : base()
        {
        }

        public Guid RequestGuid { get; set; }
    }
}
