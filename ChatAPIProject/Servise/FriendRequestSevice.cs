using Models.ServiceModels.FriendRequest;
using Service.Contracts;
using System;
using System.Collections.Generic;

namespace Service
{
    public class FriendRequestSevice : IFriendRequestSevice
    {
        public IEnumerable<FriendRequestServiceModel> All()
        {
            throw new NotImplementedException();
        }

        public bool ApproveRequest(int senderId)
        {
            throw new NotImplementedException();
        }

        public void DeclineRequest(int senderId)
        {
            throw new NotImplementedException();
        }

        public bool SendFriendRequest(int receiverId)
        {
            throw new NotImplementedException();
        }
    }
}
