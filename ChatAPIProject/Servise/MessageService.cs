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

        public bool SendMessage(MessageInputModel inputModel)
        {
            IMapper mapper = config.CreateMapper();
            MessageServiceModel model = mapper.Map<MessageServiceModel>(inputModel);
            return this.messageData.SendMessage(model);

            //if communication exist with senderId and receiverId get communication continue this conversation, else create new communicaion add it to database
            //create new message
            //add message to database 
            throw new NotImplementedException();
        }
        public void DeleteUsersMessages(int id)
        {
            this.messageData.DeleteUsersMessages(id);
        }

        public void DeleteFriendMeesages(int commId)
        {
            this.messageData.DeleteFriendMeesages(commId);
        }
    }
}