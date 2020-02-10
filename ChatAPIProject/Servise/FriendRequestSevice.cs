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

        IEnumerable<FriendRequestServiceModel> IFriendRequestSevice.All()
        {
            throw new NotImplementedException();
        }


        public List<FriendServiceModel> GetFriends(int userId, string status)
        {
            var friends = this.friendRequestData.GetFriends(userId, status);

            return friends;
        }

        public List<RequestServiceModel> GetRequests(int userId, string status)
        {
            var requests = this.friendRequestData.GetFriendRequests(userId, status);

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
    }
}
