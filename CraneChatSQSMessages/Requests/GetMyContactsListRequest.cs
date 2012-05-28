using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.SQSMessages
{
    [Serializable]
    public class GetMyContactsListRequest : CraneChatRequest
    {
        public GetMyContactsListRequest()
            : this("", "")
        {
        }

        public GetMyContactsListRequest(string userName = "", string password = "")
            : base(userName, password)
        {
        }
    }
}
