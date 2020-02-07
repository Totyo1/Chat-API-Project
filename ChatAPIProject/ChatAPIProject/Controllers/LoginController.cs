using ChatAPIProject.Models;
using Service.Contracts;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace ChatAPIProject.Controllers
{
    [RoutePrefix("api/Login")]
    public class LoginController : ApiController
    {
        private IUserService userService;
        private Authenticate authenticate;

        public LoginController(IUserService userService)
        {
            this.userService = userService;
            this.authenticate = new Authenticate(userService);
        }

        [HttpPost]
        public IHttpActionResult Authenticate([FromBody]LoginRequest login)
        {
            var loginResponse = new LoginResponse { };
            LoginRequest loginRequest = new LoginRequest { };
            loginRequest.Username = login.Username.ToLower();
            loginRequest.Password = login.Password;

            IHttpActionResult response;
            HttpResponseMessage responseMsg = new HttpResponseMessage();

            string token = this.authenticate.AuthUser(loginRequest.Username, loginRequest.Password);

            if (!string.IsNullOrWhiteSpace(token))
            {
                return Ok<string>(token);
            }
            else
            {
                loginResponse.ResponseMsg.StatusCode = HttpStatusCode.Unauthorized;
                response = ResponseMessage(loginResponse.ResponseMsg);
                return response;
            }
        }
    }
}