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

        public LoginController(IUserService userService)
        {
            this.userService = userService;
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
            bool isUsernamePasswordValid = false;

            if (login != null)
            {
                isUsernamePasswordValid = this.userService.IsUserExist(loginRequest.Username, loginRequest.Password);
            }

            if (isUsernamePasswordValid)
            {
                string token = createToken(loginRequest.Username);

                return Ok<string>(token);
            }
            else
            {
                loginResponse.ResponseMsg.StatusCode = HttpStatusCode.Unauthorized;
                response = ResponseMessage(loginResponse.ResponseMsg);
                return response;
            }
        }

        private string createToken(string username)
        {
            DateTime issuedAt = DateTime.UtcNow;
            DateTime expires = DateTime.UtcNow.AddHours(8);

            var tokenHandler = new JwtSecurityTokenHandler();

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username)
            });

            const string sec = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            var now = DateTime.UtcNow;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);


            var token =(JwtSecurityToken)tokenHandler
                .CreateJwtSecurityToken(issuer: "http://localhost:50191", audience: "http://localhost:50191", subject: claimsIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }


    }
}