using ChatAPIProject.Models.InputModels.User;
using ChatAPIProject.Models.InputModels.FriendRequest;
using Service;
using Service.Contracts;
using System;
using System.Web.Http;
using System.Security.Claims;
using System.Web;
using System.Linq;
using Models.InputModels.User;

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

        public UserController(IUserService userService, ICommunicationService communicationService, IFriendRequestSevice friendRequestSevice) : base(userService)
        {
            this.friendRequestSevice = friendRequestSevice;
            this.communicationService = communicationService;
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
            var userId = GetUserId();
            var status = STATUS_ACCEPTED;
            var allFriends = this.friendRequestSevice.GetFriends(userId, status);
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
            var userId = GetUserId();
            var status = STATUS_PENDING;
            var allRequests = this.friendRequestSevice.GetRequests(userId, status);
            if(allRequests.Count == 0)
            {
                return this.BadRequest("No available friend requests.");
            }

            return this.Ok(allRequests);
        }

        [HttpPost]
        [Route("AcceptRequest")]
        public IHttpActionResult AcceptRequest(AcceptFriendRequestInputModel model)
        {
            var userId = GetUserId();
            try
            {
                this.friendRequestSevice.AcceptRequest(userId, model.FriendId);
                this.communicationService.Create(userId, model.FriendId);

                return Ok("User friend request is accepted successfully.");
            }
            catch (Exception)
            {
                return this.BadRequest("Invalid operation.Try again.");
            }
        }

        private int GetUserId()
        {
            var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
            var userId = claims?.FirstOrDefault(x => x.Type.Equals("http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata", StringComparison.OrdinalIgnoreCase))?.Value;
            var result = int.Parse(userId);

            return result; 
        }
    }
}