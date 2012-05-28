using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Threading.Tasks;

using System.Runtime.CompilerServices;

using CraneChat.SQSMessages;
using CraneChat.CoreLibrary;

namespace CraneChat.Client
{
    class CraneChatClient : ICraneChatClient, IDisposable
    {
        public CraneChatClient()
        {
            MessageProcesor = new CraneChatMessageProcessor();
        }

        #region IDisposable implementation

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        #endregion

        ~CraneChatClient()
        {
            Dispose(false);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        protected virtual void Dispose(bool disposing)
        {
            if (!m_Disposed)
            {
                if (disposing)
                {
                    Logout();

                    if (null != m_RequestSender)
                    {
                        m_RequestSender.Dispose();
                        m_RequestSender = null;
                    }
                }

                m_Disposed = true;
            }
        }


        #region ICraneChatClient implementation

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Login(string username, string password)
        {
            if (null != m_UserName)
            {
                Logout();
            }

            m_UserName = username;
            m_Password = password;

            m_RequestSender.Login(new LoginRequest(m_UserName, m_Password));

            StartPingTask();

            StartSQSQueueReader();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Logout()
        {
            StopSQSQueueReader();

            StopPingTask();

            if (null == m_UserName)
            {
                return;
            }

            m_RequestSender.Logout(new LogoutRequest(m_UserName, m_Password));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SendBroadcastMessage(string body, IEnumerable<LocalResource> localResourceToAttach)
        {
            if (null == m_UserName)
            {
                return;
            }

            Task.Factory.StartNew(() => m_S3Uploader.Upload(localResourceToAttach)).
                ContinueWith((task) => m_RequestSender.SendBroadcastMessage
                    (new SendBroadcastMessageRequest(m_UserName, m_Password, body, task.Result)));

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SendMessageToGroup(string body, IEnumerable<MessageAttachment> attachments, string group)
        {
            if (null == m_UserName)
            {
                return;
            }

            m_RequestSender.SendMessageToGroup(new SendMessageToGroupRequest(m_UserName, m_Password, body, attachments, group));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SendPrivateMessage(string body, IEnumerable<MessageAttachment> attachments, string toUser)
        {
            if (null == m_UserName)
            {
                return;
            }

            m_RequestSender.SendPrivateMessage(new SendPrivateMessagepRequest(m_UserName, m_Password, body, attachments, toUser));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void FollowUser(string userToFollow)
        {
            if (null == m_UserName)
            {
                return;
            }

            m_RequestSender.FollowUser(new FollowUserRequest(m_UserName, m_Password, userToFollow));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UnfollowUser(string userToUnfollow)
        {
            if (null == m_UserName)
            {
                return;
            }
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddToGroup(string group)
        {
            if (null == m_UserName)
            {
                return;
            }

            m_RequestSender.FollowUser(new FollowUserRequest(m_UserName, m_Password, group));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveFromGroup(string groupName)
        {
            if (null == m_UserName)
            {
                return;
            }

            m_RequestSender.RemoveFromGroup(new RemoveFromGroupRequest(m_UserName, m_Password, groupName));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddContact(string anotherUserName)
        {
            if (null == m_UserName)
            {
                return;
            }

            m_RequestSender.AddContact(new AddContactRequest(m_UserName, m_Password, anotherUserName));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void GetAllGroupsList()
        {
            if (null == m_UserName)
            {
                return;
            }

            m_RequestSender.GetAllGroupsList(new GetAllGroupsListRequest(m_UserName, m_Password));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void GetMyGroupsList()
        {
            if (null == m_UserName)
            {
                return;
            }

            m_RequestSender.GetMyGroupsList(new GetMyGroupsListRequest(m_UserName, m_Password));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void GetMyContactsList()
        {
            if (null == m_UserName)
            {
                return;
            }

            m_RequestSender.GetMyContactsList(new GetMyContactsListRequest(m_UserName, m_Password));
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SearchMessages(string regularExpression)
        {
            if (null == m_UserName)
            {
                return;
            }

            m_RequestSender.SearchMessages(new SearchMessagesRequest(m_UserName, m_Password, regularExpression));
        }
        
        #endregion

        private void StartPingTask()
        {
            m_PingTaskCancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancelationToken = m_PingTaskCancellationTokenSource.Token;

            m_PingTask = new Task(() => PingTaskFunction(cancelationToken), cancelationToken);

        }

        private void PingTaskFunction(CancellationToken cancelationToken)
        {
            if (null == m_RequestSender)
            {
                return;
            }

            while (!cancelationToken.IsCancellationRequested)
            {
                m_RequestSender.Ping(new PingRequest(m_UserName, m_Password));

                // sleep 1 second, but peroodically check if not cancelled
                for(int i=1; i<10; ++i)
                {
                    cancelationToken.ThrowIfCancellationRequested();
                    Thread.Sleep(100);
                }
            }
        }

        private void StopPingTask()
        {
            if (null != m_PingTaskCancellationTokenSource)
            {
                m_PingTaskCancellationTokenSource.Cancel();
                m_PingTask.Wait();
                
                m_PingTaskCancellationTokenSource.Dispose();
                m_PingTaskCancellationTokenSource = null;

                m_PingTask.Dispose();
                m_PingTask = null;

            }
        }

        private void StartSQSQueueReader()
        {
            StopSQSQueueReader();

            if (null != m_UserName)
            {
                m_SQSQueueReader = new SQSQueueReader("Request_" + m_UserName);
                m_SQSQueueReader.SQSMessageEvent += MessageProcesor.ProcessIncomingMessage;

                m_SQSQueueReader.Run();
            }
        }

        private void StopSQSQueueReader()
        {
            if (null != m_SQSQueueReader)
            {
                m_SQSQueueReader.Dispose();
                m_SQSQueueReader = null;
            }
        }

        public ICraneChatMessageProcessor MessageProcesor { get; private set; }

        private string m_UserName = null;
        private string m_Password = null;

        private ICraneChatRequestSender m_RequestSender = new CraneChatRequestSender();
        private ICraneChatS3Uploader m_S3Uploader = new CraneChatS3Uploader();

        private ISQSQueueReader m_SQSQueueReader = null;

        private Task m_PingTask = null;
        private CancellationTokenSource m_PingTaskCancellationTokenSource = null;

        private bool m_Disposed = false;
    }
}
