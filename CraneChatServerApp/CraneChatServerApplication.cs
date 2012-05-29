using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CraneChat.CoreLibrary;
using CraneChat.SQSMessages;

namespace CraneChat.ServerApp
{
    class CraneChatServerApplication : IDisposable
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
        }

        public void Run()
        {
            m_SQSQueueReader.Run(true);
        }

#region

        void OnFollowUserRequest(FollowUserRequest message)
        {
        }

        void OnGetAllGroupsListRequest(GetAllGroupsListRequest message)
        {

        }

        void OnGetMyContactsListRequest(GetMyContactsListRequest message)
        {

        }

        void OnGetMyGroupsListRequest(GetMyGroupsListRequest message)
        {

        }

        void OnLoginRequest(LoginRequest message)
        {

        }

        void OnLogoutRequest(LogoutRequest message)
        {
        }

        void OnPingRequest(PingRequest message)
        {
        }

        void OnRemoveFromGroupRequest(RemoveFromGroupRequest message)
        {

        }

        void OnSearchMessagesRequest(SearchMessagesRequest message)
        {

        }

        void OnSendBroadcastMessageRequest(SendBroadcastMessageRequest message)
        {
        }

        void OnSendMessageToGroupRequest(SendMessageToGroupRequest message)
        {

        }

        void OnSendPrivateMessageRequest(SendPrivateMessageRequest message)
        {

        }

        void OnUnfollowUserRequest(UnfollowUserRequest message)
        {
        }


#endregion

#region IDisposable implementation

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
#endregion

        ~CraneChatServerApplication()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!m_Disposed)
            {
                if (disposing)
                {
                    if (null != m_SQSQueueReader)
                    {
                        m_SQSQueueReader.Dispose();
                        m_SQSQueueReader = null;

                        m_RequestProcessor.Dispose();
                        m_RequestProcessor = null;
                    }
                }

                m_Disposed = true;
            }
        }

        private IRequestProcessor m_RequestProcessor = null;
        private ISQSQueueReader m_SQSQueueReader = null;

        private bool m_Disposed = false;
    }
}
