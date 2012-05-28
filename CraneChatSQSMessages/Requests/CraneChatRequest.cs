using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.SQSMessages
{
    [Serializable]
    public abstract class CraneChatRequest : CraneChatMessage
    {
        protected CraneChatRequest() : this("", "")
        {
        }

        protected CraneChatRequest(string userName = "", string password = "") : base()
        {
            UserName = userName;
            Password = password;

            RequestGuid = Guid.NewGuid();
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid RequestGuid { get; set; }
    }
}
