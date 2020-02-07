using ChatAPIProject.Models.ServiceModels.Message;
using Models.InputModels.Message;
using System.Collections.Generic;

namespace Service.Contracts
{
    public interface IMessageService
    {
        List<MessageServiceModel> GetMessagesByCommunicationId(int comminucationId);

        bool SendMessage(MessageInputModel inputModel);
    }
}
