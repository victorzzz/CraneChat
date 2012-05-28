using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.SQSMessages
{
    [Serializable]
    public class SearchMessagesRequest : CraneChatRequest
    {
        public SearchMessagesRequest()
            : this("", "", "")
        {
        }

        public SearchMessagesRequest(string userName = "", string password = "", string regExpr = "")
            : base(userName, password)
        {
            RegExpr = regExpr;
        }

        public string RegExpr { get; set; }
    }
}
