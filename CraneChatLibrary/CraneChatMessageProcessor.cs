using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;

using CraneChat.SQSMessages;
using CraneChat.CoreLibrary;

namespace CraneChat.Client
{
    class CraneChatMessageProcessor : ICraneChatMessageProcessor
    {
        public event UserStatusNotificationEventHandler UserStatusNotificationEvent;
        public event BroadcastMessageNotificationEventHandler BroadcastMessageNotificationEvent;
        public event PrivateMessageNotificationEventHandler PrivateMessageNotificationEvent;
        public event GroupMessageNotificationEventHandler GroupMessageNotificationEvent;
        public event AllGroupsListResponseEventHandler AllGroupsListResponseEvent;
        public event CraneChatGroupResponseEventHandler CraneChatGroupResponseEvent;
        public event ErrorCraneChatMessageResponseEventHandler ErrorCraneChatMessageResponseEvent;
        public event MyContactListResponseEventHandler MyContactListResponseEvent;
        public event MyGroupsListResponseEventHandler MyGroupsListResponseEvent;
        public event SearchMessagesResponseEventHandler SearchMessagesResponseEvent;
        public event SendBroadcastMessageResponseEventHandler SendBroadcastMessageResponseEvent;
        public event SendGroupMessageResponseEventHandler SendGroupMessageResponseEvent;
        public event SendPrivateMessageResponseEventHandler SendPrivateMessageResponseEvent;
        public event LoginResponseEventHandler LoginResponseEvent;

        public CraneChatMessageProcessor()
        {
        }

        #region ICraneChatMessageProcessor implementation

        public void ProcessIncomingMessage(object sender, SQSMessageEventArgs e)
        {
            Message sqsMessage = e.SQSMessage;
            string sqsMessageBody = sqsMessage.Body;
            CraneChatMessage message = CraneChatMessage.FromXML(sqsMessageBody);

            // Thanks C# 4 for this "Visitor" implementation with "dynamic"
            OnMessage((dynamic) message);
        }
        
        #endregion

        #region Method for each message-type

        void OnMessage(CraneChatMessage message)
        {
            throw new Exception("We should not be here!");
        }

        void OnMessage(UserStatusNotification message)
        {
            if (null != UserStatusNotificationEvent)
            {
                UserStatusNotificationEvent(message);
            }
        }

        void OnMessage(BroadcastMessageNotification message)
        {
            if (null != BroadcastMessageNotificationEvent)
            {
                BroadcastMessageNotificationEvent(message);
            }
        }

        void OnMessage(PrivateMessageNotification message)
        {
            if (null != PrivateMessageNotificationEvent)
            {
                PrivateMessageNotificationEvent(message);
            }
        }

        void OnMessage(GroupMessageNotification message)
        {
            if (null != GroupMessageNotificationEvent)
            {
                GroupMessageNotificationEvent(message);
            }
        }

        void OnMessage(AllGroupsListResponse message)
        {
            if (null != AllGroupsListResponseEvent)
            {
                AllGroupsListResponseEvent(message);
            }
        }

        void OnMessage(CraneChatGroupResponse message)
        {
            if (null != CraneChatGroupResponseEvent)
            {
                CraneChatGroupResponseEvent(message);
            }
        }

        void OnMessage(ErrorCraneChatMessageResponse message)
        {
            if (null != ErrorCraneChatMessageResponseEvent)
            {
                ErrorCraneChatMessageResponseEvent(message);
            }
        }

        void OnMessage(MyContactListResponse message)
        {
            if (null != MyContactListResponseEvent)
            {
                MyContactListResponseEvent(message);
            }
        }

        void OnMessage(MyGroupsListResponse message)
        {
            if (null != MyGroupsListResponseEvent)
            {
                MyGroupsListResponseEvent(message);
            }
        }

        void OnMessage(SearchMessagesResponse message)
        {
            if (null != SearchMessagesResponseEvent)
            {
                SearchMessagesResponseEvent(message);
            }
        }

        void OnMessage(SendBroadcastMessageResponse message)
        {
            if (null != SendBroadcastMessageResponseEvent)
            {
                SendBroadcastMessageResponseEvent(message);
            }
        }

        void OnMessage(SendGroupMessageResponse message)
        {
            if (null != SendGroupMessageResponseEvent)
            {
                SendGroupMessageResponseEvent(message);
            }
        }

        void OnMessage(SendPrivateMessageResponse message)
        {
            if (null != SendPrivateMessageResponseEvent)
            {
                SendPrivateMessageResponseEvent(message);
            }
        }

        void OnMessage(LoginResponse message)
        {
            if (null != LoginResponseEvent)
            {
                LoginResponseEvent(message);
            }
        }

        #endregion
    }
}
