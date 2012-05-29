using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;

using CraneChat.CoreLibrary;
using CraneChat.SQSMessages;

namespace CraneChat.Client
{
    public delegate void UserStatusNotificationEventHandler(UserStatusNotification message);
    public delegate void BroadcastMessageNotificationEventHandler(BroadcastMessageNotification message);
    public delegate void PrivateMessageNotificationEventHandler(PrivateMessageNotification message);
    public delegate void GroupMessageNotificationEventHandler(GroupMessageNotification message);
    public delegate void AllGroupsListResponseEventHandler(AllGroupsListResponse message);
    public delegate void CraneChatGroupResponseEventHandler(CraneChatGroupResponse message);
    public delegate void ErrorCraneChatMessageResponseEventHandler(ErrorCraneChatMessageResponse message);
    public delegate void MyContactListResponseEventHandler(MyContactListResponse message);
    public delegate void MyGroupsListResponseEventHandler(MyGroupsListResponse message);
    public delegate void SearchMessagesResponseEventHandler(SearchMessagesResponse message);
    public delegate void SendBroadcastMessageResponseEventHandler(SendBroadcastMessageResponse message);
    public delegate void SendGroupMessageResponseEventHandler(SendGroupMessageResponse message);
    public delegate void SendPrivateMessageResponseEventHandler(SendPrivateMessageResponse message);
    public delegate void LoginResponseEventHandler(LoginResponse message);

    interface ICraneChatMessageProcessor
    {
        event UserStatusNotificationEventHandler UserStatusNotificationEvent;
        event BroadcastMessageNotificationEventHandler BroadcastMessageNotificationEvent;
        event PrivateMessageNotificationEventHandler PrivateMessageNotificationEvent;
        event GroupMessageNotificationEventHandler GroupMessageNotificationEvent;
        event AllGroupsListResponseEventHandler AllGroupsListResponseEvent;
        event CraneChatGroupResponseEventHandler CraneChatGroupResponseEvent;
        event ErrorCraneChatMessageResponseEventHandler ErrorCraneChatMessageResponseEvent;
        event MyContactListResponseEventHandler MyContactListResponseEvent;
        event MyGroupsListResponseEventHandler MyGroupsListResponseEvent;
        event SearchMessagesResponseEventHandler SearchMessagesResponseEvent;
        event SendBroadcastMessageResponseEventHandler SendBroadcastMessageResponseEvent;
        event SendGroupMessageResponseEventHandler SendGroupMessageResponseEvent;
        event SendPrivateMessageResponseEventHandler SendPrivateMessageResponseEvent;
        event LoginResponseEventHandler LoginResponseEvent;

        void ProcessIncomingMessage(object sender, SQSMessageEventArgs e);
    }
}
