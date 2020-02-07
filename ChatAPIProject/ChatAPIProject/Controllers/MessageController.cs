using ChatAPIProject.Models.ServiceModels.Message;
using Models.InputModels.Message;
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

        [HttpPost]
        [Route("SendMessage")]
        public IHttpActionResult SendMessage(MessageInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            int senderId = 1; //User.FindFirstValue(ClaimTypes.Name);
            bool isSent = this.messageService.SendMessage(model);

            if (!isSent)
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

            List<MessageServiceModel> messages = this.Service.GetMessages(id).ToList();

            return this.Ok(messages);
        }
    }
}