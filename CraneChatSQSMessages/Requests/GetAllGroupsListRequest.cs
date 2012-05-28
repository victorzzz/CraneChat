using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.SQSMessages
{
    [Serializable]
    public class GetAllGroupsListRequest : CraneChatRequest
    {
        public GetAllGroupsListRequest()
            : this("", "")
        {
        }

        public GetAllGroupsListRequest(string userName = "", string password = "")
            : base(userName, password)
        {
        }
    }
}
