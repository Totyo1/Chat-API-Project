using ChatAPIProject.Models.ServiceModels.Message;
using Servise.Contracts;
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

        public bool SendMessage(int communicationId, string context, int receiverId)
        {
            throw new NotImplementedException();
        }
    }
}