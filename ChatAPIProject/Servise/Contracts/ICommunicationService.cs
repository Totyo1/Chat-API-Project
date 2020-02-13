using ChatAPIProject.Models.DataModels;
using ChatAPIProject.Models.ServiceModels.Communication;

using System.Collections.Generic;

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
