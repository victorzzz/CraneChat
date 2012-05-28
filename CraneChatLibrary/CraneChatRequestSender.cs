using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

using Amazon;

//using Amazon.SimpleDB;
//using Amazon.SimpleDB.Model;

using Amazon.SQS;
using Amazon.SQS.Model;

using CraneChat.SQSMessages;

namespace CraneChat.Client
{
    public class CraneChatRequestSender : ICraneChatRequestSender, IDisposable
    {
        public CraneChatRequestSender()
        {
            // initialize Amazon SimpleDBClient
//            AmazonSimpleDBConfig simpleDBConfig = new AmazonSimpleDBConfig();
//            simpleDBConfig.ServiceURL = ConfigurationManager.AppSettings["SimpleDBServiceURL"].ToString();
//            m_simpleDBClient = AWSClientFactory.CreateAmazonSimpleDBClient(simpleDBConfig);

            // initialize Amazon SQSClient
            AmazonSQSConfig sqsConfig = new AmazonSQSConfig();
            sqsConfig.ServiceURL = ConfigurationManager.AppSettings["SQSServiceURL"].ToString();
            m_sqsClient = AWSClientFactory.CreateAmazonSQSClient(sqsConfig);

            // create 'Request' queue and save its URL
            if (null != m_sqsClient)
            {
                try
                {
                    CreateQueueRequest createQueueRequest = new CreateQueueRequest().WithQueueName("Request");
                    CreateQueueResponse createQueueResponse = m_sqsClient.CreateQueue(createQueueRequest);
                    m_requestQueueUrl = createQueueResponse.CreateQueueResult.QueueUrl;
                }
                catch (AmazonSQSException /*sqsException*/)
                {
                    throw;
                }
            }
        }

        ~CraneChatRequestSender()
        {
            Dispose(false);
        }

#region IDisposable implementation
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
#endregion

        #region ICraneChatRequestSender implementation

        public void Login(LoginRequest loginRequest)
        {
            m_sqsClient.SendMessage(new SendMessageRequest().
                WithMessageBody(loginRequest.ToXML()).
                WithQueueUrl(m_requestQueueUrl));
        }

        public void Logout(LogoutRequest logoutRequest)
        {
            m_sqsClient.SendMessage(new SendMessageRequest().
                WithMessageBody(logoutRequest.ToXML()).
                WithQueueUrl(m_requestQueueUrl));
        }

        public void Ping(PingRequest pingRequest)
        {
            m_sqsClient.SendMessage(new SendMessageRequest().
                WithMessageBody(pingRequest.ToXML()).
                WithQueueUrl(m_requestQueueUrl));
        }

        public void SendBroadcastMessage(SendBroadcastMessageRequest sendBroadcastMessageRequest)
        {
            m_sqsClient.SendMessage(new SendMessageRequest().
                WithMessageBody(sendBroadcastMessageRequest.ToXML()).
                WithQueueUrl(m_requestQueueUrl));
        }

        public void SendMessageToGroup(SendMessageToGroupRequest sendMessageToGroupRequest)
        {
            m_sqsClient.SendMessage(new SendMessageRequest().
                WithMessageBody(sendMessageToGroupRequest.ToXML()).
                WithQueueUrl(m_requestQueueUrl));
        }

        public void SendPrivateMessage(SendPrivateMessageRequest sendPrivateMessagepRequest)
        {
            m_sqsClient.SendMessage(new SendMessageRequest().
                WithMessageBody(sendPrivateMessagepRequest.ToXML()).
                WithQueueUrl(m_requestQueueUrl));
        }

        public void FollowUser(FollowUserRequest followUserRequest)
        {
            m_sqsClient.SendMessage(new SendMessageRequest().
                WithMessageBody(followUserRequest.ToXML()).
                WithQueueUrl(m_requestQueueUrl));
        }

        public void UnfollowUser(UnfollowUserRequest unfollowUserRequest)
        {
            m_sqsClient.SendMessage(new SendMessageRequest().
                WithMessageBody(unfollowUserRequest.ToXML()).
                WithQueueUrl(m_requestQueueUrl));
        }


        public void AddToGroup(AddToGroupRequest addToGroupRequest)
        {
            m_sqsClient.SendMessage(new SendMessageRequest().
                WithMessageBody(addToGroupRequest.ToXML()).
                WithQueueUrl(m_requestQueueUrl));
        }

        public void RemoveFromGroup(RemoveFromGroupRequest removeFromGroupRequest)
        {
            m_sqsClient.SendMessage(new SendMessageRequest().
                WithMessageBody(removeFromGroupRequest.ToXML()).
                WithQueueUrl(m_requestQueueUrl));
        }

        public void AddContact(AddContactRequest requestContactRequest)
        {
            m_sqsClient.SendMessage(new SendMessageRequest().
                WithMessageBody(requestContactRequest.ToXML()).
                WithQueueUrl(m_requestQueueUrl));
        }

        public void GetAllGroupsList(GetAllGroupsListRequest getAllGroupsListtRequest)
        {
            m_sqsClient.SendMessage(new SendMessageRequest().
                WithMessageBody(getAllGroupsListtRequest.ToXML()).
                WithQueueUrl(m_requestQueueUrl));
        }

        public void GetMyGroupsList(GetMyGroupsListRequest getAllGroupsListtRequest)
        {
            m_sqsClient.SendMessage(new SendMessageRequest().
                WithMessageBody(getAllGroupsListtRequest.ToXML()).
                WithQueueUrl(m_requestQueueUrl));
        }

        public void GetMyContactsList(GetMyContactsListRequest getMyContactsListRequest)
        {
            m_sqsClient.SendMessage(new SendMessageRequest().
                WithMessageBody(getMyContactsListRequest.ToXML()).
                WithQueueUrl(m_requestQueueUrl));
        }


        public void SearchMessages(SearchMessagesRequest searchMessagesRequest)
        {
            m_sqsClient.SendMessage(new SendMessageRequest().
                WithMessageBody(searchMessagesRequest.ToXML()).
                WithQueueUrl(m_requestQueueUrl));
        }


#endregion

#region private methods

#endregion

#region protedcted methods

        protected virtual void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                if (disposing)
                {
//                    m_simpleDBClient.Dispose();
                    m_sqsClient.Dispose();

//                    m_simpleDBClient = null;
                    m_sqsClient = null;
                }

                m_disposed = true;
            }
        }

#endregion

#region privat data fields

 //       private AmazonSimpleDB m_simpleDBClient = null;

        private AmazonSQS m_sqsClient = null;
        private string m_requestQueueUrl = null;

        private bool m_disposed = false;
#endregion

    }
}
