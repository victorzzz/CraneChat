using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;

using CraneChat.CoreLibrary;

namespace CraneChat.ServerApp
{
    interface IRequestProcessor : IDisposable
    {
        void ProcessIncomingMessage(object sender, SQSMessageEventArgs e);
    }
}
