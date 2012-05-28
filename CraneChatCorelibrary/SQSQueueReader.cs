using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;

using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace CraneChat.CoreLibrary
{
    public class SQSQueueReader : ISQSQueueReader, IDisposable
    {
        public event SQSMessageEventHandler SQSMessageEvent;

        public SQSQueueReader(string queueName, int sleepTime = 100)
        {
            ProcessorTask = null;
            m_TaskCancellationTokenSource = null;
            m_SleepTime = sleepTime;

            // initialize Amazon SQSClient
            AmazonSQSConfig sqsConfig = new AmazonSQSConfig();
            sqsConfig.ServiceURL = ConfigurationManager.AppSettings["SQSServiceURL"].ToString();
            m_sqsClient = AWSClientFactory.CreateAmazonSQSClient(sqsConfig);

            // create queue and save its URL
            if (null != m_sqsClient)
            {
                try
                {
                    CreateQueueRequest createQueueRequest = new CreateQueueRequest().WithQueueName(queueName);
                    CreateQueueResponse createQueueResponse = m_sqsClient.CreateQueue(createQueueRequest);
                    m_requestQueueUrl = createQueueResponse.CreateQueueResult.QueueUrl;
                }
                catch (AmazonSQSException /*sqsException*/)
                {
                    throw;
                }
            }
        }

        public void Run(bool inCurrentThread = false)
        {
            m_TaskCancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancelationToken = m_TaskCancellationTokenSource.Token;

            ProcessorTask = new Task(() => Run(cancelationToken), cancelationToken);

            if (inCurrentThread)
            {
                ProcessorTask.RunSynchronously();
            }
            else
            {
                ProcessorTask.Start();
            }
        }

        public void Stop()
        {
            if (null != m_TaskCancellationTokenSource)
            {
                m_TaskCancellationTokenSource.Cancel();
            }
        }

        private void Run(CancellationToken cancelationToken)
        {
            if (null == m_sqsClient || null == m_requestQueueUrl)
            {
                return;
            }

            while (!cancelationToken.IsCancellationRequested)
            {
                ReceiveMessageRequest request = new ReceiveMessageRequest().WithQueueUrl(m_requestQueueUrl);
                ReceiveMessageResponse response = m_sqsClient.ReceiveMessage(request);
                if (response.IsSetReceiveMessageResult())
                {
                    ReceiveMessageResult result = response.ReceiveMessageResult;
                    if (result.IsSetMessage())
                    {
                        List<Message> messages = result.Message;
                        foreach (var message in messages)
                        {
                            cancelationToken.ThrowIfCancellationRequested();

                            string receiptHandle = message.ReceiptHandle;
                            
                            OnSQSMessageEvent(message);

                            DeleteMessageRequest deleteMessageRequest = new DeleteMessageRequest().
                                WithReceiptHandle(receiptHandle).WithQueueUrl(m_requestQueueUrl);
                            DeleteMessageResponse deleteMEssageResponse = m_sqsClient.DeleteMessage(deleteMessageRequest);
                        }
                    }
                }

                Thread.Sleep(m_SleepTime);
            }
        }

        protected virtual void OnSQSMessageEvent(Message message)
        {
            if (null != SQSMessageEvent)
            {
                SQSMessageEventArgs eventArgs = new SQSMessageEventArgs(message);
                SQSMessageEvent(this, eventArgs);
            }
        }

        #region IDisposable implementation
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        ~SQSQueueReader()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                if (disposing)
                {
                    Stop();

                    if (null != ProcessorTask)
                    {
                        ProcessorTask.Wait();

                        m_TaskCancellationTokenSource.Dispose();
                        m_TaskCancellationTokenSource = null;

                        ProcessorTask.Dispose();
                        ProcessorTask = null;
                    }

                    if (null != m_sqsClient)
                    {
                        m_sqsClient.Dispose();
                        m_sqsClient = null;
                    }
                }

                m_disposed = true;
            }
        }

        public Task ProcessorTask { get; private set; }

        private CancellationTokenSource m_TaskCancellationTokenSource = null;

        private AmazonSQS m_sqsClient = null;
        private string m_requestQueueUrl = null;

        private bool m_disposed = false;
        private int m_SleepTime = 100;
    }
}
