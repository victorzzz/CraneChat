using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.SQSMessages
{
    public enum CraneChatUserState
    {
        ONLINE = 0,
        OFFLINE
    }

    [Serializable]
    public class UserStatusNotification : CraneChatResponse
    {
        UserStatusNotification()
        {

        }

        UserStatusNotification(IEnumerable<CraneChatUserState> statuses)
        {
            Statuses = new List<CraneChatUserState>(statuses);
        }

        public List<CraneChatUserState> Statuses { get; set; }
    }
}
