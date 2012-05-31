using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.SQSMessages
{
    [Serializable]
    public class MessageAttachment
    {
        public MessageAttachment(string cloudFrontURL = "", string description = "")
        {
            CloudFrontURI = new Uri(cloudFrontURL);
            Description = description;
        }

        public MessageAttachment(Uri cloudFrontURL, string description)
        {
            CloudFrontURI = cloudFrontURL;
            Description = description;
        }

        public Uri CloudFrontURI { get; set; }
        public string Description { get; set; }
     }
}
