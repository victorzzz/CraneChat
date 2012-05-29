using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CraneChat.SQSMessages;

namespace CraneChat.Client
{
    public interface ICraneChatRequestSender : IDisposable
    {
        void Login(LoginRequest loginRequest);
        void Logout(LogoutRequest logoutRequest);
        void Ping(PingRequest loginRequest);

        void SendBroadcastMessage(SendBroadcastMessageRequest sendBroadcastMessageRequest);
        void SendMessageToGroup(SendMessageToGroupRequest sendMessageToGroupRequest);
        void SendPrivateMessage(SendPrivateMessageRequest sendPrivateMessagepRequest);

        void FollowUser(FollowUserRequest followUserRequest);
        void UnfollowUser(UnfollowUserRequest unfollowUserRequest);

        void AddToGroup(AddToGroupRequest addToGroupRequest);
        void RemoveFromGroup(RemoveFromGroupRequest removeFromGroupRequest);

        void AddContact(AddContactRequest requestContactRequest);

        void GetAllGroupsList(GetAllGroupsListRequest getAllGroupsListtRequest);
        void GetMyGroupsList(GetMyGroupsListRequest getAllGroupsListtRequest);
        void GetMyContactsList(GetMyContactsListRequest getMyContactsListRequest);

        void SearchMessages(SearchMessagesRequest searchMessagesRequest);
    }
}
