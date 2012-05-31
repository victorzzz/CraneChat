using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using System.Threading;

using CraneChat.Client;

namespace SimpleTestApplication
{

    #region Some Stupyd tests
    class Message
    {
        public int i {get; set;}
    }

    class Message1 : Message
    {
        public string s {get;set;}
    }

    class Message2 : Message
    {
        public double d {get;set;}
    }

    
    class BaseProcessor
    {   
        public void ProcessIncomingMessage(Message message)
        {

            // Thanks C# 4 for this "Visitor" implementation with "dynamic"
            OnMessage((dynamic) message);
        }

        public virtual void OnMessage(Message message)
        {
            throw new Exception("!!!");
        }

        public virtual void OnMessage(Message1 m1)
        {
            Console.WriteLine("1");
        }
    }

    class ProcessorA : BaseProcessor
    {
        public override void OnMessage(Message1 m1)
        {
            Console.WriteLine("1111");
        }

        public virtual void OnMessage(Message2 m1)
        {
            Console.WriteLine("2");
        }
    }
    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            List<Task> tasks = new List<Task>()
            {
                Task.Factory.StartNew(()=>
                    {
                        using (ICraneChatClient client = new CraneChatClient())
                        {
                            client.Login("User1", "123456");

                            client.FollowUser("User2");
                            client.FollowUser("User3");

                            Thread.Sleep(5000);

                            client.Logout();
                        }
                    }),


                Task.Factory.StartNew(()=>
                    {
                        using (ICraneChatClient client = new CraneChatClient())
                        {
                            client.Login("User2", "123456");

                            client.SendBroadcastMessage("Hello from User2!", null);

                            client.Logout();
                        }
                    }),


                    Task.Factory.StartNew(()=>
                    {
                        using (ICraneChatClient client = new CraneChatClient())
                        {
                            client.Login("User3", "123456");

                            client.SendBroadcastMessage("Hello from User3!", null);

                            client.Logout();
                        }
                    })
            };


            Task.WaitAll(tasks.ToArray());

            Console.ReadLine();
        }
    }
}
