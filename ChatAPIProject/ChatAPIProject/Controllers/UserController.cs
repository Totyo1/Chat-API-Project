using ChatAPIProject.Models.DataModels;
using ChatAPIProject.Models.InputModels.FriendRequest;
using ChatAPIProject.Models.ServiceModels.Communication;
using ChatAPIProject.Models.ServiceModels.Message;
using Models.InputModels.FriendRequest;
using Models.InputModels.Message;
using Models.InputModels.User;
using Models.ServiceModels.FriendRequest;
using Models.ServiceModels.User;

using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;

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
        
        [HttpGet]
        [Route("Communications")]
        public IHttpActionResult AllCommunications()
        {
            int userId = GetUserId();
            List<CommunicationServiceModel> allCommunications = this.communicationService.All(userId);
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
            List<FriendServiceModel> allFriends = friendRequestSevice.GetFriends(userId, status);
            if (allFriends.Count == 0)
            {
                return this.BadRequest("No friends available.");
            }

            return this.Ok(allFriends);
        }

        [HttpGet]
        [Route("SendedFriendRequests")]
        public IHttpActionResult GetSendedFriendRequest()
        {
            int userId = GetUserId();
            string status = STATUS_PENDING;
            List<RequestServiceModel> allRequests = this.friendRequestSevice.GetRequests(userId, status);
            if (allRequests.Count == 0)
            {
                return this.BadRequest("No available friend requests.");
            }

            return this.Ok(allRequests);
        }

        [HttpGet]
        [Route("FriendRequests")]
        public IHttpActionResult GetFriendRequest()
        {
            int userId = GetUserId();
            string status = STATUS_PENDING;
            List<RequestServiceModel> allRequests = this.friendRequestSevice.GetRequestsToMe(userId, status);
            if (allRequests.Count == 0)
            {
                return this.BadRequest("No available friend requests.");
            }

            return this.Ok(allRequests);
        }

        [HttpGet]
        [Route("GetMessages")]
        public IHttpActionResult GetMessagesByCommunicationId(int communicationId)
        {
            int userId = this.GetUserId();
            Communication communication = this.communicationService.GetCommunicationById(communicationId);
            if (communication == null)
            {
                return this.BadRequest($"Conversation does not exist.");
            }

            List<MessageServiceModel> messages = this.messageService
                .GetMessagesByCommunicationId(communication.Id)
                .OrderByDescending(x => x.Date)
                .ToList();

            if (messages.Count == 0)
            {
                return this.BadRequest("There are no messages available.");
            }

            return this.Ok(messages);
        }

        [HttpPost]
        [Route("SendFriendRequest")]
        public IHttpActionResult SendFriendRequest(int recieverId)
        {
            if (!this.CheckIfUserExist(recieverId))
            {
                return this.BadRequest($"User with id {recieverId} does not exist.");
            }

            int userId = this.GetUserId();

            if (userId == recieverId)
            {
                return this.BadRequest("You can`t sennd friend request to yourself.");
            }

            FriendRequestInputModel model = new FriendRequestInputModel
            {
                SenderId = userId,
                ReceiverId = recieverId,
                Status = "Pending"
            };

            bool doesRequestExist = this.ChechIfRequestExist(userId, STATUS_PENDING, model.ReceiverId);
            bool doesOpositeRequestExist = this.ChechIfRequestExist(model.ReceiverId, STATUS_PENDING, userId);

            if (doesRequestExist )
            {
                return this.BadRequest($"You already send friend request to user with id {model.ReceiverId}. Wait for response.");
            }

            if (doesOpositeRequestExist)
            {
                return this.BadRequest($"You already have friend request from user with id {model.ReceiverId}. You have to respond.");
            }

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
            if (!this.CheckIfUserExist(model.FriendId))
            {
                return this.BadRequest($"User with id {model.FriendId} does not exist.");
            }

            int userId = GetUserId();
            bool isRequestExist = this.ChechIfRequestExist(userId, STATUS_PENDING, model.FriendId);
            if (!isRequestExist)
            {
                return this.BadRequest($"You don't have request from user with id {model.FriendId}.");
            }

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
            if (!this.CheckIfUserExist(model.FriendId))
            {
                return this.BadRequest($"User with id {model.FriendId} does not exist.");
            }

            int userId = GetUserId();
            bool isRequestExist = this.ChechIfRequestExist(userId, STATUS_PENDING, model.FriendId);
            if (!isRequestExist)
            {
                return this.BadRequest($"You don't have request from user with id {model.FriendId}.");
            }
            
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
            if (!this.CheckIfUserExist(model.ReceiverId))
            {
                return this.BadRequest($"User with id {model.ReceiverId} does not exist.");
            }

            int userId = this.GetUserId();
            Communication communication = this.communicationService.GetCommunicationByUsers(userId, model.ReceiverId);
            if(communication == null)
            {
                return this.BadRequest($"Communication between this users(me:{userId};receiver:{model.ReceiverId}) does not exist.");
            }
            int communicationId = communication.Id;

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
            if (!this.CheckIfUserExist(friendId))
            {
                return this.BadRequest($"User with id {friendId} does not exist.");
            }
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
        
        private int GetUserId()
        {
            List<Claim> claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
            string userId = claims?.FirstOrDefault(x => x.Type.Equals("http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata", StringComparison.OrdinalIgnoreCase))?.Value;
            int result = int.Parse(userId);

            return result; 
        }

        private bool ChechIfRequestExist(int userId,string status, int friendId)
        {
            List<RequestServiceModel> allRequests = this.friendRequestSevice.GetRequestsToMe(userId, STATUS_PENDING);
            return allRequests.Any(x => x.FriendId == friendId);
        }

        private bool CheckIfUserExist(int id)
        {
            IsExistUserServiceModel user = this.Service.IsExist(id);

            return user != null ? true : false;
        }
    }
}