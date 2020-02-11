using AutoMapper;
using ChatAPIProject.Data;
using ChatAPIProject.Models.InputModels.Message;
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
        private MapperConfiguration config;

        public MessageService()
        {
            this.messageData = new MessageCode();
            this.config = new MapperConfiguration(cfg => cfg.CreateMap<MessageInputModel, MessageServiceModel>());
        }

        public List<MessageServiceModel> GetMessagesByCommunicationId(int comminucationId)
        {
            //get take all messages where communication id is input communicationId
            throw new NotImplementedException();
        }

        public void DeleteUsersMessages(int id)
        {
            this.messageData.DeleteUsersMessages(id);
        }

        public void SendMessage(int communicationId, string content, int userId, int receiverId)
        {
            this.messageData.SendMessage(communicationId, content, userId, receiverId);
        }
    }
}