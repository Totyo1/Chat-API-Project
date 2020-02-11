using ChatAPIProject.Models.InputModels.Message;
using ChatAPIProject.Models.ServiceModels.Message;
using Service.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Security;

namespace ChatAPIProject.Controllers
{
    [Authorize]
    [RoutePrefix("api/Message")]
    public class MessageController : BaseController<IMessageService>
    {
        IMessageService messageService;

        public MessageController(IMessageService messageService) : base(messageService)
        {
            this.messageService = messageService;
        }

        [HttpGet]
        [Route("GetMessages")]
        public IHttpActionResult GetMessages(int id)
        {
            if(id < 0)
            {
                return this.BadRequest("Invalid request.");
            }

            List<MessageServiceModel> messages = this.Service.GetMessagesByCommunicationId(id).ToList();

            return this.Ok(messages);
        }
    }
}