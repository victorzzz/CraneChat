using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;

using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using Amazon.SQS.Util;

using CraneChat.CoreLibrary;
using CraneChat.SQSMessages;

namespace CraneChat.ServerApp
{
    class ResponseSender : BaseDisposable, IResponseSender
    {
        public ResponseSender()
        {
            // initialize Amazon SQSClient
            AmazonSQSConfig sqsConfig = new AmazonSQSConfig();
            sqsConfig.ServiceURL = ConfigurationManager.AppSettings["SQSServiceURL"].ToString();
            m_sqsClient = AWSClientFactory.CreateAmazonSQSClient(sqsConfig);
        }

        // returns queue url
        private string CreateUserResponseQueue(string userName)
        {
            string queueURL = null;

            if (!m_ResponseQueueURLCach.TryGetValue(userName, out queueURL))
            {
                // create 'Request' queue and save its URL
                try
                {
                    CreateQueueRequest createQueueRequest = new CreateQueueRequest().WithQueueName(CraneChatUtility.MakeUserResponseQueueName(userName));
                    CreateQueueResponse createQueueResponse = m_sqsClient.CreateQueue(createQueueRequest);
                    queueURL = createQueueResponse.CreateQueueResult.QueueUrl;
                }
                catch (AmazonSQSException /*sqsException*/)
                {
                    throw;
                }

                m_ResponseQueueURLCach.Add(userName, queueURL);
            }

            return queueURL;
        }

        private void SendResponse(CraneChatRequest request, CraneChatResponse response)
        {
            response.RequestGuid = request.RequestGuid;

            string queueURL = CreateUserResponseQueue(request.UserName);

            m_sqsClient.SendMessage(new SendMessageRequest().
                WithMessageBody(response.ToXML()).
                WithQueueUrl(queueURL));
        }

#region IResponseSender implementation

        public void SendErrorResponse(CraneChatRequest request, string error)
        {
            ErrorCraneChatMessageResponse response = new ErrorCraneChatMessageResponse(error);
            SendResponse(request, response);
        }

        public void SendOKResponse(CraneChatRequest request)
        {
            CraneChatResponse response = new CraneChatResponse();
            SendResponse(request, response);
        }

        public void SendBroadcastMessageResponse(CraneChatRequest request)
        {
            SendBroadcastMessageResponse response = new SendBroadcastMessageResponse();
            SendResponse(request, response);
        }

        public void SendLoginResponse(CraneChatRequest request)
        {
            LoginResponse response = new LoginResponse();
            SendResponse(request, response);
        }

        public void SendBroadcastMessageNotification(string toUser, string body, List<MessageAttachment> attachments, string fromUser)
        {
            BroadcastMessageNotification response = new BroadcastMessageNotification(body, attachments, fromUser);
            string queueURL = CreateUserResponseQueue(toUser);

            m_sqsClient.SendMessage(new SendMessageRequest().
                WithMessageBody(response.ToXML()).
                WithQueueUrl(queueURL));
        }

        public void SendSearchMessagesResponse(CraneChatRequest request, IEnumerable<SearchMessagesResponseItem> items)
        {
            SearchMessagesResponse response = new SearchMessagesResponse(items);
            SendResponse(request, response);
        }

        protected override void SafeManagedResourcesDisposing()
        {
            m_sqsClient.Dispose();
            m_sqsClient = null;
        }

#endregion

#region privat data fields
        private AmazonSQS m_sqsClient = null;

        // <userName, response queue url>
        private Dictionary<string, string> m_ResponseQueueURLCach = new Dictionary<string, string>();
#endregion
    }
}
