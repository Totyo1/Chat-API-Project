using ChatAPIProject.Models.InputModels.FriendRequest;
using ChatAPIProject.Models.ServiceModels.FriendRequest;
using Models.ServiceModels.FriendRequest;
using System.Collections.Generic;

namespace Service.Contracts
{
    public interface IFriendRequestSevice
    {
        bool SendFriendRequest(FriendRequestInputModel model);

        IEnumerable<FriendRequestServiceModel> All();

        bool ApproveRequest(int senderId);

        void DeclineRequest(int senderId);

        List<FriendServiceModel> GetFriends(int userId, string status);

        List<RequestServiceModel> GetRequests(int userId, string status);
    }
}
