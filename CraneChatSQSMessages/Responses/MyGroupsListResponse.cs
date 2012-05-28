using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.SQSMessages
{
    public class MyGroupsListResponse : CraneChatGroupResponse
    {
        public MyGroupsListResponse()
            : this(null)
        {
        }

        public MyGroupsListResponse(IEnumerable<string> groups = null) : base(groups)
        {
        }
    }
}
