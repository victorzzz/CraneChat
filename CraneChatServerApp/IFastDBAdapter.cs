using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CraneChat.SQSMessages;

namespace CraneChat.ServerApp
{
    interface IFastDBAdapter : IDisposable
    {
        void ChangeUserState(string userName, CraneChatUserState state);
        void AddBroadcastMessage(string userName, string body, IEnumerable<MessageAttachment> attachments);
    }
}
