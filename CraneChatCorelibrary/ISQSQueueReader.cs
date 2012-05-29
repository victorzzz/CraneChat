using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace CraneChat.CoreLibrary
{
    public class SQSMessageEventArgs : EventArgs
    {
        public SQSMessageEventArgs(Message message)
        {
            SQSMessage = message;
        }

        public Message SQSMessage { get; private set; }
    }

    public delegate void SQSMessageEventHandler(object sender, SQSMessageEventArgs e);

    public interface ISQSQueueReader : IDisposable
    {
        event SQSMessageEventHandler SQSMessageEvent;
        
        void Run(bool inCurrentThread = false);
        void Stop();
    }
}
