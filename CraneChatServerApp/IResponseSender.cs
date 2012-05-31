using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CraneChat.SQSMessages;

namespace CraneChat.ServerApp
{
    interface IResponseSender : IDisposable
    {
        void SendErrorResponse(CraneChatRequest request, string error);
        void SendOKResponse(CraneChatRequest request);
        void SendLoginResponse(CraneChatRequest request);
        void SendBroadcastMessageNotification(string toUser, string body, List<MessageAttachment> attachments, string fromUser);
        void SendBroadcastMessageResponse(CraneChatRequest request);
        void SendSearchMessagesResponse(CraneChatRequest request, IEnumerable<SearchMessagesResponseItem> items);
    }
}
