using ChatAPIProject.Data;
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
        private MessageCode messageData;
        public MessageService()
        {
            this.messageData = new MessageCode();
        }
        public List<MessageServiceModel> GetMessages(int comminucationId)
        {
            throw new NotImplementedException();
        }

        public bool SendMessage(int senderId, int receiverId, string context)
        {
            throw new NotImplementedException();
        }
    }
}