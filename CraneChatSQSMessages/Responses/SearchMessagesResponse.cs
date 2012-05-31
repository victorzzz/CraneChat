using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.SQSMessages
{
    [Serializable]
    public class SearchMessagesResponseItem
    {
        public SearchMessagesResponseItem()
            : this("", "", null)
        {
        }

        public SearchMessagesResponseItem(string from = "", string body = "", IEnumerable<MessageAttachment> attachments = null)
        {
            From = from;
            Body = body;
            if (null != attachments)
            {
                Attachments = new List<MessageAttachment>(attachments);
            }
        }

        public string From { get; set; }
        public string Body { get; set; }
        public List<MessageAttachment> Attachments { get; set; }
    }


    [Serializable]
    public class SearchMessagesResponse : CraneChatResponse
    {
        public SearchMessagesResponse()
            : this(null)
        {
        }

        public SearchMessagesResponse(IEnumerable<SearchMessagesResponseItem> items = null)
        {
            if (null != items)
            {
                Items = new List<SearchMessagesResponseItem>(items);
            }
        }

        public List<SearchMessagesResponseItem> Items { get; set; }
    }
}
