using ChatAPIProject.Models.InputModels.User;

using Service.Contracts;
using System;
using System.Web.Http;

namespace ChatAPIProject.Controllers
{
    [Authorize]
    [RoutePrefix("api/Admin")]
    public class AdminController : ApiController
    {
        private IUserService userService;
        private IFriendRequestSevice friendRequestSevice;
        private ICommunicationService communicationService;
        private IMessageService messageService;

        public AdminController(IUserService userService, IFriendRequestSevice friendRequestSevice, ICommunicationService communicationService, IMessageService messageService)
        {
            this.userService = userService;
            this.friendRequestSevice = friendRequestSevice;
            this.communicationService = communicationService;
            this.messageService = messageService;
        }

        [HttpPost]
        [Route("Create")]
        public IHttpActionResult CreateUser(UserInputModel inputModel)
        {
            try
            {
                this.userService.CreateUser(inputModel);
                return this.Ok("Successfully created");
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("deleteUser")]
        public IHttpActionResult DeleteUser(int id)
        {
            try
            {
                this.userService.DeleteUser(id);
                this.communicationService.DeleteUsersCommunications(id);
                this.friendRequestSevice.DeleteUserRequests(id);
                this.messageService.DeleteUsersMessages(id);
                return this.Ok("Successfully deleted");
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}
