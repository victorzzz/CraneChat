using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;

using CraneChat.SQSMessages;
using CraneChat.CoreLibrary;

namespace CraneChat.Client
{
    class CraneChatS3Uploader : BaseDisposable, ICraneChatS3Uploader, IDisposable
    {
        public CraneChatS3Uploader()
        {
            m_CloudFrontRoot = new Uri(ConfigurationManager.AppSettings["CloudFrontRoot"]);
            m_BucketName = ConfigurationManager.AppSettings["BucketName"];

            AmazonS3Config s3Config = new AmazonS3Config().WithServiceURL(ConfigurationManager.AppSettings["S3ServiceURL"].ToString());
            AmazonS3 s3Client = AWSClientFactory.CreateAmazonS3Client(s3Config);
            m_s3transferUtility = new TransferUtility(s3Client);
        }

        #region ICraneChatS3Uploader implemntation

        public List<MessageAttachment> Upload(IEnumerable<LocalResource> localResources)
        {
            List<MessageAttachment> result = new List<MessageAttachment>();

            if (null != localResources)
            {
                foreach (var res in localResources)
                {
                    Guid guid = Guid.NewGuid();

                    string fileName = Path.GetFileName(res.LocalPath);
                    string key = guid.ToString() + fileName;

                    TransferUtilityUploadRequest request = new TransferUtilityUploadRequest()
                        .WithBucketName(m_BucketName)
                        .WithFilePath(res.LocalPath)
                        .WithSubscriber(this.UploadFileProgressCallback)
                        .WithCannedACL(S3CannedACL.PublicRead)
                        .WithKey(key);
                    m_s3transferUtility.Upload(request);

                    MessageAttachment attachment = new MessageAttachment(new Uri(m_CloudFrontRoot, key), res.Description ?? fileName);
                    result.Add(attachment);
                }
            }

            return result;
        }

        #endregion

        protected override void SafeManagedResourcesDisposing()
        {
            m_s3transferUtility.Dispose();
            m_s3transferUtility = null;
        }

        private void UploadFileProgressCallback(object sender, UploadProgressArgs e)
        {

        }

        private TransferUtility m_s3transferUtility = null;

        private string m_BucketName = null;
        private Uri m_CloudFrontRoot = null;
    }
}
