using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.SQSMessages
{
    public class GetMyGroupsListRequest : CraneChatRequest
    {
        public GetMyGroupsListRequest()
            : this("", "")
        {
        }

        public GetMyGroupsListRequest(string userName = "", string password = "")
            : base(userName, password)
        {
        }
    }
}
