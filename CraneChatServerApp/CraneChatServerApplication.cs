using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CraneChat.CoreLibrary;
using CraneChat.SQSMessages;

namespace CraneChat.ServerApp
{
    class CraneChatServerApplication : BaseDisposable, IDisposable
    {
        public CraneChatServerApplication()
        {
            m_SQSQueueReader = new SQSQueueReader("Request", 0);
            m_RequestProcessor = new RequestProcessor();

            m_SQSQueueReader.SQSMessageEvent += m_RequestProcessor.ProcessIncomingMessage;

            m_RequestProcessor.LoginRequestEvent += OnLoginRequest;
            m_RequestProcessor.LogoutRequestEvent += OnLogoutRequest;
            m_RequestProcessor.PingRequestEvent += OnPingRequest;
            m_RequestProcessor.FollowUserRequestEvent += OnFollowUserRequest;
            m_RequestProcessor.UnfollowUserRequestEvent += OnUnfollowUserRequest;
            m_RequestProcessor.SendBroadcastMessageRequestEvent += OnSendBroadcastMessageRequest;
            m_RequestProcessor.SearchMessageRequestEvent += OnSearchMessagesRequest;

            m_MembershipAdapter = new ASPMembershipAdapter();
            m_SQLAdapter = new MSSQLAdapter();
            m_SimpleDBAdapter = new SimpleDBAdapter();
            m_ResponseSender = new ResponseSender();

        }

        public void Run()
        {
            m_SQSQueueReader.Run(true);
        }

        private bool ValidateCredentials(CraneChatRequest request)
        {
            return m_MembershipAdapter.ValidateUserCredamtial(request.UserName, request.Password);
        }

        private void SendBadCredentialsMessage(CraneChatRequest request)
        {
            m_ResponseSender.SendErrorResponse(request, "Bad credentials");
        }
#region

        void OnFollowUserRequest(FollowUserRequest message)
        {
            if(!ValidateCredentials(message))
            {
                SendBadCredentialsMessage(message);
            }

            bool ok = m_SQLAdapter.FollowUser(message.UserName, message.UserToFollow);
            if (ok)
            {
                m_ResponseSender.SendOKResponse(message);
            }
            else
            {
                m_ResponseSender.SendErrorResponse(message, "Error while trying to follow user " + message.UserToFollow);
            }
        }

        void OnGetAllGroupsListRequest(GetAllGroupsListRequest message)
        {
            if (!ValidateCredentials(message))
            {
                SendBadCredentialsMessage(message);
            }

        }

        void OnGetMyContactsListRequest(GetMyContactsListRequest message)
        {
            if (!ValidateCredentials(message))
            {
                SendBadCredentialsMessage(message);
            }

        }

        void OnGetMyGroupsListRequest(GetMyGroupsListRequest message)
        {
            if (!ValidateCredentials(message))
            {
                SendBadCredentialsMessage(message);
            }

        }

        void OnLoginRequest(LoginRequest message)
        {
            if (!ValidateCredentials(message))
            {
                SendBadCredentialsMessage(message);
            }

            m_SimpleDBAdapter.ChangeUserState(message.UserName, CraneChatUserState.ONLINE);
            m_ResponseSender.SendLoginResponse(message);
        }

        void OnLogoutRequest(LogoutRequest message)
        {
            if (!ValidateCredentials(message))
            {
                SendBadCredentialsMessage(message);
            }
 
            m_SimpleDBAdapter.ChangeUserState(message.UserName, CraneChatUserState.OFFLINE);
            m_ResponseSender.SendOKResponse(message);
        }

        void OnPingRequest(PingRequest message)
        {
            if (!ValidateCredentials(message))
            {
                SendBadCredentialsMessage(message);
            }

            m_SimpleDBAdapter.ChangeUserState(message.UserName, CraneChatUserState.ONLINE);
        }

        void OnRemoveFromGroupRequest(RemoveFromGroupRequest message)
        {
            if (!ValidateCredentials(message))
            {
                SendBadCredentialsMessage(message);
            }

        }

        void OnSearchMessagesRequest(SearchMessagesRequest message)
        {
            if (!ValidateCredentials(message))
            {
                SendBadCredentialsMessage(message);
            }

            // TODO
        }

        void OnSendBroadcastMessageRequest(SendBroadcastMessageRequest message)
        {
            if (!ValidateCredentials(message))
            {
                SendBadCredentialsMessage(message);
            }

            m_SimpleDBAdapter.AddBroadcastMessage(message.UserName, message.MessageBody, message.Attachments);
            m_ResponseSender.SendBroadcastMessageResponse(message);

            // notify all followers
            IEnumerable<string> followers = m_SQLAdapter.GetFollowers(message.UserName);
            foreach (var follower in followers)
            {
                m_ResponseSender.SendBroadcastMessageNotification(follower, message.MessageBody, message.Attachments, message.UserName);
            }
        }

        void OnSendMessageToGroupRequest(SendMessageToGroupRequest message)
        {
            if (!ValidateCredentials(message))
            {
                SendBadCredentialsMessage(message);
            }

        }

        void OnSendPrivateMessageRequest(SendPrivateMessageRequest message)
        {
            if (!ValidateCredentials(message))
            {
                SendBadCredentialsMessage(message);
            }

        }

        void OnUnfollowUserRequest(UnfollowUserRequest message)
        {
            if (!ValidateCredentials(message))
            {
                SendBadCredentialsMessage(message);
            }

            bool ok = m_SQLAdapter.FollowUser(message.UserName, message.UserToUnfollow);
            if (ok)
            {
                m_ResponseSender.SendOKResponse(message);
            }
            else
            {
                m_ResponseSender.SendErrorResponse(message, "Error while trying to unfollow user " + message.UserToUnfollow);
            }
        }


#endregion

        protected override void SafeManagedResourcesDisposing()
        {
            m_SQSQueueReader.Dispose();
            m_SQSQueueReader = null;

            m_RequestProcessor.Dispose();
            m_RequestProcessor = null;

            m_MembershipAdapter.Dispose();
            m_MembershipAdapter = null;

            m_SQLAdapter.Dispose();
            m_SQLAdapter = null;

            m_SimpleDBAdapter.Dispose();
            m_SimpleDBAdapter = null;
        }

        private IRequestProcessor m_RequestProcessor = null;
        private ISQSQueueReader m_SQSQueueReader = null;
        private IMembershipAdapter m_MembershipAdapter = null;
        private IRelationalDBAdapter m_SQLAdapter = null;
        private IFastDBAdapter m_SimpleDBAdapter = null;
        private IResponseSender m_ResponseSender = null;
    }
}
