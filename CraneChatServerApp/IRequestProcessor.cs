using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;

using CraneChat.SQSMessages;
using CraneChat.CoreLibrary;

namespace CraneChat.ServerApp
{
    public delegate void LoginRequestEventHandler(LoginRequest message);
    public delegate void LogoutRequestEventHandler(LogoutRequest message);
    public delegate void PingRequestEventHandler(PingRequest message);
    public delegate void FollowUserRequestEventHandler(FollowUserRequest message);
    public delegate void UnfollowUserRequestEventHandler(UnfollowUserRequest message);
    public delegate void SendBroadcastMessageRequestEventHandler(SendBroadcastMessageRequest message);

    interface IRequestProcessor : IDisposable
    {
        event LoginRequestEventHandler LoginRequestEvent;
        event LogoutRequestEventHandler LogoutRequestEvent;
        event PingRequestEventHandler PingRequestEvent;
        event FollowUserRequestEventHandler FollowUserRequestEvent;
        event UnfollowUserRequestEventHandler UnfollowUserRequestEvent;
        event SendBroadcastMessageRequestEventHandler SendBroadcastMessageRequestEvent;

        void ProcessIncomingMessage(object sender, SQSMessageEventArgs e);
    }
}
