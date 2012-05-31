using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;

using Amazon;
using Amazon.SimpleDB;
using Amazon.SimpleDB.Model;
using Amazon.SimpleDB.Util;

using CraneChat.SQSMessages;
using CraneChat.CoreLibrary;

namespace CraneChat.ServerApp
{
    class SimpleDBAdapter : BaseDisposable, IFastDBAdapter
    {
        public SimpleDBAdapter()
        {
            // initialize Amazon SimpleDBClient
            AmazonSimpleDBConfig simpleDBConfig = new AmazonSimpleDBConfig();
            simpleDBConfig.ServiceURL = ConfigurationManager.AppSettings["SimpleDBServiceURL"];
            m_simpleDBClient = AWSClientFactory.CreateAmazonSimpleDBClient(simpleDBConfig);

            m_BroadcastMessagesDomain = ConfigurationManager.AppSettings["BroadcastMessagesSDBDomain"];
            m_GroupMessagesDomain = ConfigurationManager.AppSettings["GroupMessagesSDBDomain"];
            m_PrivateMessagesDomain = ConfigurationManager.AppSettings["PrivateMessagesSDBDomain"];
            m_UserStateDomain = ConfigurationManager.AppSettings["UserStateSDBDomain"];
        }

        public void ChangeUserState(string userName, CraneChatUserState state)
        {
            PutAttributesRequest request = new PutAttributesRequest()
                .WithDomainName(m_UserStateDomain)
                .WithItemName(userName)
                .WithAttribute(new ReplaceableAttribute()
                        .WithName("time")
                        .WithValue(AmazonSimpleDBUtil.FormattedCurrentTimestamp)
                        .WithReplace(true))
                .WithAttribute(new ReplaceableAttribute()
                    .WithName("state")
                    .WithValue(state.ToString())
                    .WithReplace(true));

            PutAttributesResponse resoponse = m_simpleDBClient.PutAttributes(request);
        }

        public void AddBroadcastMessage(string userName, string body, IEnumerable<MessageAttachment> attachments)
        {
            int broadcastDomainNumber = new Random().Next(0, 7);

            List<ReplaceableAttribute> attrs = new List<ReplaceableAttribute>() 
            {
                new ReplaceableAttribute()
                        .WithName("body")
                        .WithValue(body),

                new ReplaceableAttribute()
                        .WithName("time")
                        .WithValue(AmazonSimpleDBUtil.FormattedCurrentTimestamp)
            };

            int i = 0;
            foreach (var attachment in attachments)
            {
                attrs.Add(new ReplaceableAttribute()
                    .WithName(String.Format("Attachment_{0}_URL", i))
                    .WithValue(attachment.CloudFrontURI.AbsoluteUri));

                attrs.Add(new ReplaceableAttribute()
                    .WithName(String.Format("Attachment_{0}_Description", i))
                    .WithValue(attachment.Description));

                ++i;
            }

            PutAttributesRequest request = new PutAttributesRequest()
                .WithDomainName(m_BroadcastMessagesDomain + broadcastDomainNumber.ToString())
                .WithItemName(userName + "_" + Guid.NewGuid());

            request.Attribute = attrs;

            PutAttributesResponse resoponse = m_simpleDBClient.PutAttributes(request);
        }

        protected override void SafeManagedResourcesDisposing()
        {
            m_simpleDBClient.Dispose();
            m_simpleDBClient = null;
        }

        private AmazonSimpleDB m_simpleDBClient = null;
        
        private string m_BroadcastMessagesDomain = null;
        private string m_GroupMessagesDomain = null;
        private string m_PrivateMessagesDomain = null;
        private string m_UserStateDomain = null;
    }
}
