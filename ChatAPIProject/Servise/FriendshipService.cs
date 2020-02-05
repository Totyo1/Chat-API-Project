using ChatAPIProject.Models.ServiceModels.Friendship;
using Servise.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatAPIProject.Service
{
    public class FriendshipService : IFriendshipService
    {
        public FriendshipServiceModel AddFriend(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteFriend(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IFriendshipService> GetFriends()
        {
            throw new NotImplementedException();
        }

        IEnumerable<FriendshipServiceModel> IFriendshipService.GetFriends()
        {
            throw new NotImplementedException();
        }
    }
}