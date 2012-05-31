using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.Client
{
    public class LocalResource
    {
        public LocalResource(string localPath = "", string desciption = "")
        {
            LocalPath = localPath;
            Description = desciption;
        }

        public string LocalPath { get; private set; }
        public string Description { get; private set; }
    }
}
