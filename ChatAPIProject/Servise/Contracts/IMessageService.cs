using ChatAPIProject.Models.ServiceModels.Message;
using System.Collections.Generic;

namespace Service.Contracts
{
    public interface IMessageService
    {
        List<MessageServiceModel> GetMessagesByCommunicationId(int comminucationId);

        bool SendMessage(int senderId, int receiverId, string context);
    }
}
