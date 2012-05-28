using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.SQSMessages
{
    [Serializable]
    public class AllGroupsListResponse : CraneChatGroupResponse
    {
        public AllGroupsListResponse()
            : this(null)
        {
        }

        public AllGroupsListResponse(IEnumerable<string> groups = null) : base(groups)
        {
        }
    }
}
