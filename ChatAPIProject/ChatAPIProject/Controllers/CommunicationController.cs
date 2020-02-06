using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [Route("GetCommunications")]
        public IHttpActionResult GetCommunications()
        {
            var communicatons = this.Service.All().ToList();

            if(communicatons.Count() == 0)
            {
                return this.Ok("No available communications.");
            }

            return this.Ok(communicatons);
        }
    }
}