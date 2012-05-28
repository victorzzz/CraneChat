using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.SQSMessages
{
    public class MyContactListResponse : CraneChatResponse
    {
        public MyContactListResponse()
            : this(null)
        {
        }

        public MyContactListResponse(IEnumerable<string> contacts = null)
        {
            if (null != contacts)
            {
                Contacts = new List<string>(contacts);
            }
        }

        public List<string> Contacts { get; set; }
    }
}
