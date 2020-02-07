
using ChatAPIProject.Models.InputModels.FriendRequest;
using ChatAPIProject.Models.ServiceModels.FriendRequest;
using Service.Contracts;
using System;
using System.Collections.Generic;

namespace Service
{
    public class FriendRequestSevice : IFriendRequestSevice
    {
        
        public bool SendFriendRequest(FriendRequestInputModel model)
        {
            throw new NotImplementedException();
        }

        IEnumerable<FriendRequestServiceModel> IFriendRequestSevice.All()
        {
            throw new NotImplementedException();
        }

        bool IFriendRequestSevice.ApproveRequest(int senderId)
        {
            throw new NotImplementedException();
        }

        void IFriendRequestSevice.DeclineRequest(int senderId)
        {
            throw new NotImplementedException();
        }

       
    }
}
