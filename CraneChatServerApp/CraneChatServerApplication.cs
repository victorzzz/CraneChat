using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CraneChat.CoreLibrary;

namespace CraneChat.ServerApp
{
    class CraneChatServerApplication : IDisposable
    {
        public CraneChatServerApplication()
        {
            m_SQSQueueReader = new SQSQueueReader("Request", 1);
            m_RequestProcessor = new RequestProcessor();

            m_SQSQueueReader.SQSMessageEvent += m_RequestProcessor.ProcessIncomingMessage;


        }

#region IDisposable implementation

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
#endregion

        ~CraneChatServerApplication()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!m_Disposed)
            {
                if (disposing)
                {
                    if (null != m_SQSQueueReader)
                    {
                        m_SQSQueueReader.Dispose();
                        m_SQSQueueReader = null;

                        m_RequestProcessor.Dispose();
                        m_RequestProcessor = null;
                    }
                }

                m_Disposed = true;
            }
        }

        private IRequestProcessor m_RequestProcessor = null;
        private ISQSQueueReader m_SQSQueueReader = null;

        private bool m_Disposed = false;
    }
}
