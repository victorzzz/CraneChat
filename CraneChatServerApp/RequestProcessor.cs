using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;

using CraneChat.SQSMessages;
using CraneChat.CoreLibrary;

namespace CraneChat.ServerApp
{
    class RequestProcessor : IRequestProcessor
    {
        public event LoginRequestEventHandler LoginRequestEvent;
        public event LogoutRequestEventHandler LogoutRequestEvent;
        public event PingRequestEventHandler PingRequestEvent;
        public event FollowUserRequestEventHandler FollowUserRequestEvent;
        public event UnfollowUserRequestEventHandler UnfollowUserRequestEvent;
        public event SendBroadcastMessageRequestEventHandler SendBroadcastMessageRequestEvent;

        public RequestProcessor()
        {

        }

        #region IRequestProcessor implementation

        public void ProcessIncomingMessage(object sender, SQSMessageEventArgs e)
        {
            Message sqsMessage = e.SQSMessage;
            string sqsMessageBody = sqsMessage.Body;
            CraneChatMessage message = CraneChatMessage.FromXML(sqsMessageBody);

            // Thanks C# 4 for this "Visitor" implementation with "dynamic"
            OnMessage((dynamic)message);
        }

        #endregion

        void OnMessage(AddContactRequest message)
        {

        }

        void OnMessage(AddToGroupRequest message)
        {

        }

        void OnMessage(FollowUserRequest message)
        {
            if (null != FollowUserRequestEvent)
            {
                FollowUserRequestEvent(message);
            }
        }

        void OnMessage(GetAllGroupsListRequest message)
        {

        }

        void OnMessage(GetMyContactsListRequest message)
        {

        }

        void OnMessage(GetMyGroupsListRequest message)
        {

        }

        void OnMessage(LoginRequest message)
        {
            if(null != LoginRequestEvent)
            {
                LoginRequestEvent(message);
            }
        }

        void OnMessage(LogoutRequest message)
        {
            if(null != LogoutRequestEvent)
            {
                LogoutRequestEvent(message);
            }
        }

        void OnMessage(PingRequest message)
        {
            if(null != PingRequestEvent)
            {
                PingRequestEvent(message);
            }
        }

        void OnMessage(RemoveFromGroupRequest message)
        {

        }

        void OnMessage(SearchMessagesRequest message)
        {

        }

        void OnMessage(SendBroadcastMessageRequest message)
        {
            if (null != SendBroadcastMessageRequestEvent)
            {
                SendBroadcastMessageRequestEvent(message);
            }
        }

        void OnMessage(SendMessageToGroupRequest message)
        {

        }

        void OnMessage(SendPrivateMessageRequest message)
        {

        }

        void OnMessage(UnfollowUserRequest message)
        {
            if (null != UnfollowUserRequestEvent)
            {
                UnfollowUserRequestEvent(message);
            }
        }
    }
}
