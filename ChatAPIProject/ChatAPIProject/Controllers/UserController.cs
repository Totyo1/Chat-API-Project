using ChatAPIProject.Models.InputModels.User;
using Service.Contracts;
using System;
using System.Web.Http;

namespace ChatAPIProject.Controllers
{
    
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        [Route("create")]
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
    }
}