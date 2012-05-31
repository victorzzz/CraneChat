using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

using CraneChat.Client;

namespace CraneChatWPFClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App() : base()
        {
            Client = new CraneChatClient();
        }

        public CraneChatClient Client {get; private set;}
    }
}
