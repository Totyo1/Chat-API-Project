using ChatAPIProject.Models.InputModels.Communication;

using Service.Contracts;
using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace ChatAPIProject.Controllers
{
    [Authorize]
    [RoutePrefix("api/Communication")]
    public class CommunicationController : BaseController<ICommunicationService>
    {
        private IUserService userService;

        public CommunicationController(ICommunicationService communicationService, IUserService userService) : base(communicationService)
        {
            this.userService = userService;
        }

        [HttpGet]
        [Route("All")]
        public IHttpActionResult GetAllCommunications()
        {
            var userId = this.GetUserId();
            var communicatons = this.Service.All(userId).ToList();

            if(communicatons.Count() == 0)
            {
                return this.Ok("No available communications.");
            }

            return this.Ok(communicatons);
        }

        [HttpGet]
        [Route("GetByUsersIds")]
        public IHttpActionResult GetByUsers(int firstUserId, int secondUserId)
        {
            var communication1 = this.Service.GetCommunicationByUsers(firstUserId, secondUserId);
            var communication2 = this.Service.GetCommunicationByUsers(secondUserId, firstUserId);
            if (communication1 == null && communication2 == null)
            {
                return this.BadRequest("This communication does not esixt.");
            }
            var result = communication1 == null ? communication2 : communication1;

            return this.Ok(result);
        }

        [HttpPost]
        [Route("Create")]
        public IHttpActionResult CreateCommunication(CommunicationInputModel model)
        {
            if (!this.CheckIfUserExist(model.FirstUserId))
            {
                return this.BadRequest($"User with id {model.FirstUserId} does not exist.");
            }
            if (!this.CheckIfUserExist(model.SecondUserId))
            {
                return this.BadRequest($"User with id {model.SecondUserId} does not exist.");
            }
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            try
            {
                this.Service.Create(model.FirstUserId, model.SecondUserId);
                return this.Ok();
            }
            catch (Exception)
            {
                return this.BadRequest("Invalid input.");      
            }
        }

        private int GetUserId()
        {
            var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
            var userId = claims?.FirstOrDefault(x => x.Type.Equals("http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata", StringComparison.OrdinalIgnoreCase))?.Value;
            var result = int.Parse(userId);

            return result;
        }

        private bool CheckIfUserExist(int id)
        {
            var user = this.userService.IsExist(id);

            return user != null ? true : false;
        }

    }
}