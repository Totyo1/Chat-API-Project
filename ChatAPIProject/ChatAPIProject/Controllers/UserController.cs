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
using Models.InputModels.FriendRequest;
using Models.ServiceModels.FriendRequest;
using System.Collections.Generic;
using Models.InputModels.Message;

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
        private List<int> listOfFriendRequests;

        public UserController(IUserService userService, IFriendRequestSevice friendRequestSevice, ICommunicationService communicationService, IMessageService messageService) : base(userService)
        {
            this.friendRequestSevice = friendRequestSevice;
            this.communicationService = communicationService;
            this.listOfFriendRequests = new List<int>();
            this.messageService = messageService;
        }
        
        [HttpGet]
        [Route("Communications")]
        public IHttpActionResult AllCommunications()
        {
            var userId = GetUserId();
            var allCommunications = this.communicationService.All(userId);
            if (allCommunications.Count == 0)
            {
                return this.BadRequest("No available communications.");
            }

            return this.Ok(allCommunications);
        }

        [HttpGet]
        [Route("Friends")]
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
            if (allRequests.Count == 0)
            {
                return this.BadRequest("No available friend requests.");
            }

            foreach (var item in allRequests)
            {
                listOfFriendRequests.Add(item.FriendId);
            }

            return this.Ok(allRequests);
        }

        [HttpPost]
        [Route("Create")]
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
        [Route("SendFriendRequest")]
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
        
        [HttpPost]
        [Route("AcceptRequest")]
        public IHttpActionResult AcceptRequest(AcceptFriendRequestInputModel model)
        {
            var isRequestExist = listOfFriendRequests.Contains(model.FriendId);
            if (!isRequestExist)
            {
                return this.BadRequest($"You don't have request from user with id {model.FriendId}.");
            }

            var userId = GetUserId();
            try
            {
                this.friendRequestSevice.AcceptRequest(userId, model.FriendId);
                this.communicationService.Create(userId, model.FriendId);

                return Ok($"User with id {model.FriendId} friend request is accepted successfully.");
            }
            catch (Exception)
            {
                return this.BadRequest("Invalid operation.Try again.");
            }
        }

        [HttpPost]
        [Route("RejectRequest")]
        public IHttpActionResult RejectRequest(RejectFriendRequestInputModel model)
        {
            var isRequestExist = listOfFriendRequests.Contains(model.FriendId);
            if (!isRequestExist)
            {
                return this.BadRequest($"You don't have request from user with id {model.FriendId}.");
            }

            var userId = GetUserId();
            try
            {
                this.friendRequestSevice.RejectRequest(userId, model.FriendId);

                return this.Ok($"You rejected user with id {model.FriendId}.");
            }
            catch (Exception)
            {
                return this.BadRequest("Invalid operation.Try again.");
            }
        }

        [HttpPost]
        [Route("SendMessage")]
        public IHttpActionResult SendMessage(SendMessageInputModel model)
        {
            var userId = this.GetUserId();
            var communication = this.communicationService.GetCommunicationByUsers(userId, model.ReceiverId);
            if(communication == null)
            {
                return this.BadRequest($"Communication between this users(me:{userId};receiver:{model.ReceiverId}) does not exist.");
            }
            var communicationId = communication.Id;

            try
            {
                this.messageService.SendMessage(communicationId, model.Content, userId, model.ReceiverId);
            }
            catch (Exception)
            {
                return this.BadRequest("Fail to send message.");
            }

            return this.Ok("Message send successfully.");
        }

        [HttpDelete]
        [Route("deleteUser")]
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

        [HttpDelete]
        [Route("deleteFriend")]
        public IHttpActionResult DeleteFriend(int friendId)
        {
            try
            {
              int commId = this.communicationService.DeleteFriend(GetUserId(), friendId);
                this.messageService.DeleteFriendMeesages(commId);
                this.friendRequestSevice.DeleteFriendRequests(GetUserId(), friendId);
                return this.Ok("You are not friends anymore");
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetMessages")]
        public IHttpActionResult GetMessagesByCommunicationId(int communicationId)
        {
            var userId = this.GetUserId();
            var communication = this.communicationService.GetCommunicationById(communicationId);
            if(communication == null)
            {
                return this.BadRequest($"Conversation does not exist.");
            }

            var messages = this.messageService.GetMessagesByCommunicationId(communication.Id);
            if(messages.Count == 0)
            {
                return this.BadRequest("There are no messages available.");
            }

            return this.Ok(messages);
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