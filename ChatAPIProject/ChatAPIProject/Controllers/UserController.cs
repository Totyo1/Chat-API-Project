using ChatAPIProject.Models.InputModels.User;
using Service.Contracts;
using System;
using System.Web.Http;

namespace ChatAPIProject.Controllers
{
    
    [RoutePrefix("api/User")]
    public class UserController : BaseController<IUserService>
    {
        public UserController(IUserService userService) : base(userService)
        { }

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
    }
}