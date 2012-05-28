using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.SQSMessages
{
    [Serializable]
    class LoginResponse : CraneChatResponse
    {
        public LoginResponse(string message = "", bool autorized = false) : base(message)
        {
            Autorized = autorized;
        }

        public bool Autorized { get; private set; }
    }
}
