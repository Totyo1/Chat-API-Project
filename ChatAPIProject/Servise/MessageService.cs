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
            //get take all messages where communication id is input communicationId
            throw new NotImplementedException();
        }

        public bool SendMessage(int senderId, int receiverId, string context)
        {
            //if communication exist with senderId and receiverId get communication continue this conversation, else create new communicaion add it to database
            //create new message
            //add message to database 
            throw new NotImplementedException();
        }
    }
}