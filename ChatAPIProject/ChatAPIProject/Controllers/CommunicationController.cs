using ChatAPIProject.Models.InputModels.Communication;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace ChatAPIProject.Controllers
{
    [Authorize]
    [RoutePrefix("api/Communication")]
    public class CommunicationController : BaseController<ICommunicationService>
    {

        public CommunicationController(ICommunicationService communicationService) : base(communicationService)
         { }

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
            var communication = this.Service.GetCommunicationByUsers(firstUserId, secondUserId);
            if(communication == null)
            {
                return this.BadRequest("This communication does not esixt.");
            }

            return this.Ok(communication);
        }

        [HttpPost]
        [Route("Create")]
        public IHttpActionResult CreateCommunication(CommunicationInputModel model)
        {
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

    }
}