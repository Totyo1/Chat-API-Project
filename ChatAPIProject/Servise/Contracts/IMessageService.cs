using ChatAPIProject.Models.ServiceModels.Message;
using System.Collections.Generic;

namespace Servise.Contracts
{
    public interface IMessageService
    {
        List<MessageServiceModel> GetMessages(int comminucationId);

        bool SendMessage(int communicationId, string context, int receiverId);
    }
}
