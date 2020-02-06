using ChatAPIProject.Models.ServiceModels.Friendship;
using System.Collections.Generic;

namespace Service.Contracts
{
    public interface IFriendshipService
    {
        IEnumerable<FriendshipServiceModel> GetFriends();

        FriendshipServiceModel AddFriend(int id);



        void DeleteFriend(int id);


    }
}
