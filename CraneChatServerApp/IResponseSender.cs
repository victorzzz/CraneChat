using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CraneChat.SQSMessages;

namespace CraneChat.ServerApp
{
    interface IResponseSender : IDisposable
    {
        void SendErrorResponse(string userName, string error);
        void SendLoginResponse(string userName);
        void SendBroadcastMessageNotification(string userName, string body, List<MessageAttachment> attachments);
    }
}
