using ChatAPIProject.Models.InputModels.FriendRequest;
using ChatAPIProject.Models.ServiceModels.FriendRequest;
using Models.ServiceModels.FriendRequest;
using System;
using System.Collections.Generic;

namespace Service.Contracts
{
    public interface IFriendRequestSevice
    {
        bool SendFriendRequest(FriendRequestInputModel model);

        void AcceptRequest(int userId, int receiverId);

        void RejectRequest(int userId, int receiverId);

        List<FriendServiceModel> GetFriends(int userId, string status);

        List<RequestServiceModel> GetRequests(int userId, string status);

        void DeleteUserRequests(int id);

        void DeleteFriendRequests(int myId, int friendId);
    }
}
