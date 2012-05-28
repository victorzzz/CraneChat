using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.SQSMessages
{
    [Serializable]
    public class ErrorCraneChatMessageResponse : CraneChatResponse
    {
        public ErrorCraneChatMessageResponse()
            : this("")
        {
        }

        public ErrorCraneChatMessageResponse(string errorMessage = "")
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; set; }
    }
}
