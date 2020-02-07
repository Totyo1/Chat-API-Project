using ChatAPIProject.Models.InputModels.FriendRequest;
using ChatAPIProject.Models.ServiceModels.FriendRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IFriendRequestSevice
    {
        bool SendFriendRequest(FriendRequestInputModel model);

        IEnumerable<FriendRequestServiceModel> All();

        bool ApproveRequest(int senderId);

        void DeclineRequest(int senderId);
    }
}
