using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CraneChat.SQSMessages;

namespace CraneChat.Client
{
    interface ICraneChatS3Uploader
    {
        // synchronous, may take lontime
        List<MessageAttachment> Upload(IEnumerable<LocalResource> localResources);
    }
}
