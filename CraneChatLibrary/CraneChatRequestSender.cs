using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;

using CraneChat.CoreLibrary;
using CraneChat.SQSMessages;

namespace CraneChat.Client
{
    public class CraneChatRequestSender : BaseDisposable, ICraneChatRequestSender, IDisposable
    {
        public CraneChatRequestSender()
        {
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

        #region ICraneChatRequestSender implementation

        public void Login(LoginRequest loginRequest)
        {
            SendRequest(loginRequest.ToXML());
        }

        public void Logout(LogoutRequest logoutRequest)
        {
            SendRequest(logoutRequest.ToXML());
        }

        public void Ping(PingRequest pingRequest)
        {
            SendRequest(pingRequest.ToXML());
        }

        public void SendBroadcastMessage(SendBroadcastMessageRequest sendBroadcastMessageRequest)
        {
            SendRequest(sendBroadcastMessageRequest.ToXML());
        }

        public void SendMessageToGroup(SendMessageToGroupRequest sendMessageToGroupRequest)
        {
            SendRequest(sendMessageToGroupRequest.ToXML());
        }

        public void SendPrivateMessage(SendPrivateMessageRequest sendPrivateMessagepRequest)
        {
            SendRequest(sendPrivateMessagepRequest.ToXML());
        }

        public void FollowUser(FollowUserRequest followUserRequest)
        {
            SendRequest(followUserRequest.ToXML());
        }

        public void UnfollowUser(UnfollowUserRequest unfollowUserRequest)
        {
            SendRequest(unfollowUserRequest.ToXML());
        }


        public void AddToGroup(AddToGroupRequest addToGroupRequest)
        {
            SendRequest(addToGroupRequest.ToXML());
        }

        public void RemoveFromGroup(RemoveFromGroupRequest removeFromGroupRequest)
        {
            SendRequest(removeFromGroupRequest.ToXML());
        }

        public void AddContact(AddContactRequest requestContactRequest)
        {
            SendRequest(requestContactRequest.ToXML());
        }

        public void GetAllGroupsList(GetAllGroupsListRequest getAllGroupsListtRequest)
        {
            SendRequest(getAllGroupsListtRequest.ToXML());
        }

        public void GetMyGroupsList(GetMyGroupsListRequest getAllGroupsListtRequest)
        {
            SendRequest(getAllGroupsListtRequest.ToXML());
        }

        public void GetMyContactsList(GetMyContactsListRequest getMyContactsListRequest)
        {
            SendRequest(getMyContactsListRequest.ToXML());
        }


        public void SearchMessages(SearchMessagesRequest searchMessagesRequest)
        {
            SendRequest(searchMessagesRequest.ToXML());
        }


#endregion

        private void SendRequest(string requestBody)
        {
            m_sqsClient.SendMessage(new SendMessageRequest().
                WithMessageBody(requestBody).
                WithQueueUrl(m_requestQueueUrl));
        }

        protected override void SafeManagedResourcesDisposing()
        {
            m_sqsClient.Dispose();
            m_sqsClient = null;
        }

#region privat data fields
        private AmazonSQS m_sqsClient = null;
        private string m_requestQueueUrl = null;
#endregion

    }
}
