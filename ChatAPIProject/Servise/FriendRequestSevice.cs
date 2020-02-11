using ChatAPIProject.Data;
using ChatAPIProject.Models.InputModels.FriendRequest;
using ChatAPIProject.Models.ServiceModels.FriendRequest;
using Models.ServiceModels.FriendRequest;
using Service.Contracts;
using System;
using System.Collections.Generic;

namespace Service
{
    public class FriendRequestSevice : IFriendRequestSevice
    {
        private FriendRequestCode friendRequestData;

        public FriendRequestSevice()
        {
            this.friendRequestData = new FriendRequestCode();
        }

        public bool SendFriendRequest(FriendRequestInputModel model)
        {
            try
            {
                this.friendRequestData.SendFriendRequest(model);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<FriendServiceModel> GetFriends(int userId, string status)
        {
            List<FriendServiceModel> friends = this.friendRequestData.GetFriends(userId, status);

            return friends;
        }

        public List<RequestServiceModel> GetRequests(int userId, string status)
        {
            List<RequestServiceModel> requests = this.friendRequestData.GetFriendRequests(userId, status);

            return requests;
        }

        public void AcceptRequest(int userId, int receiverId)
        {
            this.friendRequestData.AcceptRequest(userId, receiverId);
        }

        public void RejectRequest(int userId, int receiverId)
        {
            this.friendRequestData.RejectRequest(userId, receiverId);
        }

        public void DeleteUserRequests(int id)
        {
            this.friendRequestData.DeleteUserRequests(id);
        }

        public void DeleteFriendRequests(int myId, int friendId)
        {
            this.friendRequestData.DeleteFriendRequests(myId, friendId);
        }
    }
}
