using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CraneChat.SQSMessages;
using CraneChatWeb;

namespace CraneChat.Client
{
    #region EventArgs
/*
    public class BroadcastMessageNotificationEventArgs : EventArgs
    {
        public BroadcastMessageNotificationEventArgs(BroadcastMessageNotification message)
        {
            Message = message;
        }

        public BroadcastMessageNotification Message { get; private set; }
    }

    public class GroupMessageNotificationEventArgs : EventArgs
    {
        public GroupMessageNotificationEventArgs(GroupMessageNotification message)
        {
            Message = message;
        }

        public GroupMessageNotification Message { get; private set; }
    }

    public class PrivateMessageNotificationEventArgs : EventArgs
    {
        public PrivateMessageNotificationEventArgs(PrivateMessageNotification message)
        {
            Message = message;
        }

        public PrivateMessageNotification Message { get; private set; }
    }

    //-------------------------------

    public class SendBroadcastMessageResponseEventArgs : EventArgs
    {
        public SendBroadcastMessageResponseEventArgs(SendBroadcastMessageResponse message)
        {
            Message = message;
        }

        public SendBroadcastMessageResponse Message { get; private set; }
    }

    public class SendGroupMessageResponseEventArgs : EventArgs
    {
        public SendGroupMessageResponseEventArgs(SendBroadcastMessageResponse message)
        {
            Message = message;
        }

        public SendGroupMessageResponseEventArgs Message { get; private set; }
    }

    //--------------------------------

    public class AllGroupsListEventArgs : EventArgs
    {
        public AllGroupsListEventArgs(AllGroupsListResponse message)
        {
            Message = message;
        }

        public AllGroupsListResponse Message { get; private set; }
    }

    public class MyGroupsListEventArgs : EventArgs
    {
        public MyGroupsListEventArgs(MyGroupsListResponse message)
        {
            Message = message;
        }

        public MyGroupsListResponse Message { get; private set; }
    }

    public class SearchMessagesEventArgs : EventArgs
    {
        public SearchMessagesEventArgs(SearchMessagesResponse message)
        {
            Message = message;
        }

        public SearchMessagesResponse Message { get; private set; }
    }

    public class ErrorCraneChatMessageEventArgs : EventArgs
    {
        public ErrorCraneChatMessageEventArgs(ErrorCraneChatMessageResponse message)
        {
            Message = message;
        }

        public ErrorCraneChatMessageResponse Message { get; private set; }
    }

    public class MyContactListEventArgs : EventArgs
    {
        public MyContactListEventArgs(MyContactListResponse message)
        {
            Message = message;
        }

        public MyContactListResponse Message { get; private set; }
    }
*/
    #endregion

    #region delegates
    public delegate void BroadcastMessageEventHandler(object sender, BroadcastMessageEventArgs e);
    public delegate void GroupMessageEventHandler(object sender, GroupMessageEventArgs e);
    public delegate void PrivateMessageEventHandler(object sender, PrivateMessageEventArgs e);
    public delegate void AllGroupsListEventHandler(object sender, AllGroupsListEventArgs e);
    public delegate void MyGroupsListEventHandler(object sender, MyGroupsListEventArgs e);
    public delegate void MyContactListEventHandler(object sender, MyContactListEventArgs e);

    public delegate void SearchMessagesEventHandler(object sender, SearchMessagesEventArgs e);
    public delegate void ErrorCraneChatMessageEventHandler(object sender, ErrorCraneChatMessageEventArgs e);

    #endregion

    public interface ICraneChatMessageSubscriber : IDisposable
    {
        void SubscribeForUser(string userName);

        event BroadcastMessageEventHandler BroadcastMessageEvent;
        event GroupMessageEventHandler GroupMessageEvent;
        event PrivateMessageEventHandler privateMessageEvent;

        event AllGroupsListEventHandler AllGroupListEvent;
        event MyGroupsListEventHandler MyGroupsListEvent;
        event MyContactListEventHandler MyContactListEven;

        event SearchMessagesEventHandler SearchMessagesEvent;

        event ErrorCraneChatMessageEventHandler errorCraneChatMessageEvent;
    }
}
