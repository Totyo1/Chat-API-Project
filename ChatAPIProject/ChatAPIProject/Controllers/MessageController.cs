using ChatAPIProject.Models.ServiceModels.Message;

using Service.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ChatAPIProject.Controllers
{
    [Authorize]
    [RoutePrefix("api/Message")]
    public class MessageController : BaseController<IMessageService>
    {
        IMessageService messageService;
        ICommunicationService communicationService;

        public MessageController(IMessageService messageService, ICommunicationService communicationService) : base(messageService)
        {
            this.messageService = messageService;
            this.communicationService = communicationService;
        }

        [HttpGet]
        [Route("GetMessages")]
        public IHttpActionResult GetMessages(int communicationId)
        {
            var communication = this.communicationService.GetCommunicationById(communicationId);
            if (communication == null)
            {
                return this.BadRequest($"Conversation does not exist.");
            }

            List<MessageServiceModel> messages = this.Service.GetMessagesByCommunicationId(communicationId).ToList();

            return this.Ok(messages);
        }
    }
}