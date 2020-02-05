using Models.ServiceModels.FriendRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IFriendRequestSevice
    {
        bool SendFriendRequest(int receiverId);

        IEnumerable<FriendRequestServiceModel> All();

        bool ApproveRequest(int senderId);

        void DeclineRequest(int senderId);
    }
}
