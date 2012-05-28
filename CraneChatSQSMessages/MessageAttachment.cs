using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.SQSMessages
{
    [Serializable]
    public class MessageAttachment
    {
        public MessageAttachment(string cloudFrontURL = "")
        {
            CloudFrontURI = new Uri(cloudFrontURL);
        }

        public MessageAttachment(Uri cloudFrontURI)
        {
            CloudFrontURI = cloudFrontURI;
        }

        public Uri CloudFrontURI { get; private set; }
     }
}
