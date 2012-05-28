using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

using CraneChat.SQSMessages;

using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;

namespace CraneChat.Client
{
    class CraneChatS3Uploader : ICraneChatS3Uploader, IDisposable
    {
        public CraneChatS3Uploader()
        {
            AmazonS3Config s3Config = new AmazonS3Config().WithServiceURL(ConfigurationManager.AppSettings["SQSServiceURL"].ToString());
            AmazonS3 s3Client = AWSClientFactory.CreateAmazonS3Client(s3Config);
            m_s3transferUtility = new TransferUtility(s3Client);
        }

        ~CraneChatS3Uploader()
        {
            Dispose(false);
        }

        #region ICraneChatS3Uploader implemntation

        public List<MessageAttachment> Upload(IEnumerable<LocalResource> localResources)
        {
            List<MessageAttachment> result = new List<MessageAttachment>();

            foreach (var res in localResources)
            {
                Guid guid = Guid.NewGuid();

                string key = guid.ToString() + Path.GetFileName(res.LocalPath);

                TransferUtilityUploadRequest request = new TransferUtilityUploadRequest()
                    .WithBucketName(BucketName)
                    .WithFilePath(res.LocalPath)
                    .WithSubscriber(this.UploadFileProgressCallback)
                    .WithCannedACL(S3CannedACL.PublicRead)
                    .WithKey(key);
                m_s3transferUtility.Upload(request);

                MessageAttachment attachment = new MessageAttachment(new Uri(CloudFrontRoot, key));
                result.Add(attachment);
            }

            return result;
        }

        #endregion

        #region IDisposable implementation
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        protected virtual void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                if (disposing)
                {
                    m_s3transferUtility.Dispose();
                    m_s3transferUtility = null;
                }

                m_disposed = true;
            }
        }

        private void UploadFileProgressCallback(object sender, UploadProgressArgs e)
        {

        }

        private bool m_disposed = false;

        private TransferUtility m_s3transferUtility = null;

        static readonly string BucketName = "cranechat";
        static readonly Uri CloudFrontRoot = new Uri("https://diakfw4vwoegb.cloudfront.net/");
    }
}
