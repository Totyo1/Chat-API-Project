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
            return this.messageData.GetMessagesByCommunicationId(comminucationId);
        }

        public void DeleteUsersMessages(int id)
        {
            this.messageData.DeleteUsersMessages(id);
        }

        public void SendMessage(int communicationId, string content, int userId, int receiverId)
        {
            this.messageData.SendMessage(communicationId, content, userId, receiverId);
        }

        public void DeleteFriendMeesages(int commId)
        {
            this.messageData.DeleteFriendMeesages(commId);
        }
    }
}