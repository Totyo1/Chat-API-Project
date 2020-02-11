using ChatAPIProject.Models.DataModels;
using ChatAPIProject.Models.ServiceModels;
using ChatAPIProject.Models.ServiceModels.Communication;
using ChatAPIProject.Models.ServiceModels.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface ICommunicationService
    {
        List<CommunicationServiceModel> All(int userId);

        void Create(int firstUserId, int secondUserId);

        Communication GetCommunicationByUsers(int firstUserId, int secondUserId);

        void DeleteUsersCommunications(int id);
        int DeleteFriend(int myId, int friendId);

        Communication GetCommunicationById(int communicationId);
    }
}
