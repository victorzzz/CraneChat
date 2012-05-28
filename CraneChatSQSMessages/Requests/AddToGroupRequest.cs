﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.SQSMessages
{
    [Serializable]
    public class AddToGroupRequest : CraneChatRequest
    {
        public AddToGroupRequest()
            : this("", "", "")
        {
        }

        public AddToGroupRequest(string userName = "", string password = "", string group = "")
            : base(userName, password)
        {
            Group = group;
        }

        public string Group { get; set; }
    }
}
