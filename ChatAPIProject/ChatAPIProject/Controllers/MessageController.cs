using Models.InputModels.Message;
using Service.Contracts;
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
        public MessageController(IMessageService messageService) : base(messageService)
        {}

        [HttpPost]
        [Route("SendMessage")]
        public IHttpActionResult SendMessage(MessageInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var senderId = 1; //User.FindFirstValue(ClaimTypes.Name);
            bool isSent = this.messageService.SendMessage(senderId, model.ReceiverId, model.Content);

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

            var messages = this.Service.GetMessages(id).ToList();

            return this.Ok(messages);
        }
    }
}