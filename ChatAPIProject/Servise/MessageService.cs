using AutoMapper;
using ChatAPIProject.Data;
using ChatAPIProject.Models.ServiceModels.Message;
using Models.InputModels.Message;
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
        public List<MessageServiceModel> GetMessages(int comminucationId)
        {
            throw new NotImplementedException();
        }

        public bool SendMessage(MessageInputModel inputModel)
        {
            IMapper mapper = config.CreateMapper();
            MessageServiceModel model = mapper.Map<MessageServiceModel>(inputModel);
            return this.messageData.SendMessage(model);

        }
    }
}