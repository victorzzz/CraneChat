using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.SQSMessages
{
    [Serializable]
    public class AddContactRequest : CraneChatRequest
    {
        public AddContactRequest()
            : this("", "", "")
        {
        }

        public AddContactRequest(string userName = "", string password = "", string contactToAdd = "")
            : base(userName, password)
        {
            ContactToAdd = contactToAdd;
        }

        public string ContactToAdd { get; set; }
    }
}
