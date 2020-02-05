using ChatAPIProject.Models.InputModels.User;
using Servise.Contracts;

using System;
using System.Web.Http;

namespace ChatAPIProject.Controllers
{
    
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private IUserService messageService;

        public UserController()
        {
            this.messageService = new Service.UserService();
        }
        [HttpPost]
        [Route("create")]
        public IHttpActionResult createUser(UserInputModel inputModel)
        {
            try
            {
                this.messageService.CreateUser(inputModel);
                return this.Ok("Successfully created");
            }
            catch (Exception ex)
            {

                return this.BadRequest(ex.Message);
            }
        }
    }
}