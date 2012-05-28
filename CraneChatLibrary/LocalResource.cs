using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraneChat.Client
{
    public class LocalResource
    {
        public LocalResource(string localPath = "")
        {
            LocalPath = localPath;
        }

        public string LocalPath { get; private set; }    
    }
}
