using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CraneChat.SQSMessages;

namespace CraneChat.Client
{
    interface ICraneChatClient
    {
        void Login(string username, string password);

        void Logout();

        void SendBroadcastMessage(string body, IEnumerable<LocalResource> localResourceToAttach);

        void SendMessageToGroup(string body, IEnumerable<MessageAttachment> attachments, string group);

        void SendPrivateMessage(string body, IEnumerable<MessageAttachment> attachments, string toUser);

        void FollowUser(string userToFollow);

        void UnfollowUser(string userToUnfollow);

        void AddToGroup(string group);

        void RemoveFromGroup(string groupName);

        void AddContact(string anotherUserName);

        void GetAllGroupsList();

        void GetMyGroupsList();

        void GetMyContactsList();

        void SearchMessages(string regularExpression);
    }
}
