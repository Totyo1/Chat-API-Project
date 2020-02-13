using ChatAPIProject.Models.ServiceModels.Message;

using System.Collections.Generic;

namespace Service.Contracts
{
    public interface IMessageService
    {
        List<MessageServiceModel> GetMessagesByCommunicationId(int comminucationId);

        void SendMessage(int communicationId, string content, int userId, int receiverId);

        void DeleteUsersMessages(int id);
        void DeleteFriendMeesages(int commId);
    }
}
