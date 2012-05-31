using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.CoreLibrary
{
    public static class CraneChatUtility
    {
        public static string MakeUserResponseQueueName(string userName)
        {
            return "Request_" + userName;
        }
    }
}
