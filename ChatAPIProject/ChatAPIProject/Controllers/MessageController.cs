using ChatAPIProject.Models.InputModels.Message;
using Servise.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ChatAPIProject.Controllers
{
    [Authorize]
    [RoutePrefix("api/Message")]
    public class MessageController : ApiController
    {
        private IMessageService messageService;
        private ICommunicationService communicationService;

        public MessageController(IMessageService messageService, ICommunicationService communicationService)
        {
            this.messageService = messageService;
            this.communicationService = communicationService;
        }

        [HttpPost]
        [Route("SendMessage")]
        public IHttpActionResult SendMessage(MessageInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var isItSend = this.messageService.SendMessage(model.CommunicationId, model.Content, model.ReceiverId);

            if (!isItSend)
            {
                return this.BadRequest("Fail to send message.");
            }

            return this.Ok("Message send successfully.");
        }

        [HttpGet]
        [Route("GetMessages")]
        public IHttpActionResult GetMessages(int id)
        {
            if(id < 0)
            {
                return this.BadRequest("Invalid request.");
            }

            var messages = this.messageService.GetMessages(id).ToList();

            return this.Ok(messages);
        }
    }
}