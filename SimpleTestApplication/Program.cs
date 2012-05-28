using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SimpleTestApplication
{
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


    class Program
    {
        static void Main(string[] args)
        {
/*
            LoginRequest request1 = new LoginRequest("User1", "Password1");
            string xml1 = request1.ToXML();
            Console.WriteLine(xml1);


            LoginRequest request2 = CraneChatMessage.FromXML(xml1) as LoginRequest;
            string xml2 = request2.ToXML();
            Console.WriteLine(xml2);
*/

            ProcessorA processorA = new ProcessorA();
            processorA.ProcessIncomingMessage(new Message1());
            processorA.ProcessIncomingMessage(new Message2());

            Console.ReadLine();
        }
    }
}
