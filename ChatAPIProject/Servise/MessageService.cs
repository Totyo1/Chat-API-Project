using ChatAPIProject.Models.ServiceModels.Message;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatAPIProject.Service
{
    public class MessageService : IMessageService
    {
        public List<MessageServiceModel> GetMessages(int comminucationId)
        {
            throw new NotImplementedException();
        }

        public bool SendMessage(int communicationId, string content, int receiverId)
        {
            throw new NotImplementedException();
        }
    }
}