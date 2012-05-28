using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.SQSMessages
{
    public class CraneChatGroupResponse : CraneChatResponse
    {
        public CraneChatGroupResponse()
            : this(null)
        {
        }

        public CraneChatGroupResponse(IEnumerable<string> groups = null)
        {
            if (null != groups)
            {
                Groups = new List<string>(groups);
            }
        }

        public List<string> Groups { get; private set; }
    }
}
