using ChatAPIProject.Models.InputModels.User;
using ChatAPIProject.Models.InputModels.FriendRequest;
using Service;
using Service.Contracts;
using System;
using System.Web.Http;
using System.Security.Claims;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using Models.ServiceModels.FriendRequest;

namespace ChatAPIProject.Controllers
{
    [Authorize]
    [RoutePrefix("api/User")]
    public class UserController : BaseController<IUserService>
    {
        private const string STATUS_ACCEPTED = "Accepted";
        private const string STATUS_REJECTED = "Rejected";
        private const string STATUS_PENDING = "Pending";

        private IFriendRequestSevice friendRequestSevice;
        private ICommunicationService communicationService;
        private IMessageService messageService;

        public UserController(IUserService userService, IFriendRequestSevice friendRequestSevice, ICommunicationService communicationService, IMessageService messageService) : base(userService)
        {
            this.friendRequestSevice = friendRequestSevice;
            this.communicationService = communicationService;
            this.messageService = messageService;
        }

        [HttpPost]
        [Route("create")]
        public IHttpActionResult CreateUser(UserInputModel inputModel)
        {
            try
            {
                this.Service.CreateUser(inputModel);
                return this.Ok("Successfully created");
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("friendRequest")]
        public IHttpActionResult SendFriendRequest(int recieverId)
        {
            FriendRequestInputModel model = new FriendRequestInputModel
            {
                SenderId = this.GetUserId(),
                ReceiverId = recieverId,
                Status = "Pending"
            };
            try
            {
                this.friendRequestSevice.SendFriendRequest(model);
                return this.Ok("Request sent successfully");
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("AllCommunications")]
        public IHttpActionResult AllCommunications()
        {
            var userId = GetUserId();
            var allCommunications = this.communicationService.All(userId);
            if(allCommunications.Count == 0)
            {
                return this.BadRequest("No available communications.");
            }

            return this.Ok(allCommunications);
        }

        [HttpGet]
        [Route("GetFriends")]
        public IHttpActionResult GetFriends()
        {
            int userId = GetUserId();
            string status = STATUS_ACCEPTED;
            var allFriends = friendRequestSevice.GetFriends(userId, status);
            if (allFriends.Count == 0)
            {
                return this.BadRequest("No friends available.");
            }

            return this.Ok(allFriends);
        }

        [HttpGet]
        [Route("FriendRequests")]
        public IHttpActionResult GetFriendRequest()
        {
            int userId = GetUserId();
            string status = STATUS_PENDING;
            List<RequestServiceModel> allRequests = this.friendRequestSevice.GetRequests(userId, status);
            if(allRequests.Count == 0)
            {
                return this.BadRequest("No available friend requests.");
            }

            return this.Ok(allRequests);
        }

        [HttpDelete]
        [Route("delete")]
        public IHttpActionResult DeleteUser()
        {
            try
            {
                this.Service.DeleteUser(GetUserId());
                this.communicationService.DeleteUsersCommunications(GetUserId());
                this.friendRequestSevice.DeleteUserRequests(GetUserId());
                this.messageService.DeleteUsersMessages(GetUserId());
                return this.Ok("Successfully deleted");
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        private int GetUserId()
        {
            List<Claim> claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
            string userId = claims?.FirstOrDefault(x => x.Type.Equals("http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata", StringComparison.OrdinalIgnoreCase))?.Value;
            int result = int.Parse(userId);

            return result; 
        }
    }
}