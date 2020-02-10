using ChatAPIProject.Models.InputModels.User;
using ChatAPIProject.Models.InputModels.FriendRequest;
using Service;
using Service.Contracts;
using System;
using System.Web.Http;
using System.Security.Claims;
using System.Web;
using System.Linq;

namespace ChatAPIProject.Controllers
{
    
    [RoutePrefix("api/User")]
    public class UserController : BaseController<IUserService>
    {
        private IFriendRequestSevice friendRequestSevice;

        public UserController(IUserService userService) : base(userService)
        {
            this.friendRequestSevice = new FriendRequestSevice();
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
                SenderId = 1,//user id taken from the token. this value is set just to build the project without mistakes
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
         
            

            return this.BadRequest();
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