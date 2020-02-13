using Service.Contracts;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ChatAPIProject
{
    public class Authenticate
    {
        private IUserService userService;

        public Authenticate(IUserService userService)
        {
            this.userService = userService;
        }


        public string AuthUser(string username, string password)
        {
            var user = this.userService.GetUser(username, password);

            if(user == null)
            {
                return string.Empty;
            }

            var token = createToken(username, user.Id.ToString());

            return token;
        }

        private string createToken(string username, string userId)
        {
            DateTime issuedAt = DateTime.UtcNow;
            DateTime expires = DateTime.UtcNow.AddHours(8);

            var tokenHandler = new JwtSecurityTokenHandler();

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.UserData, userId)
            });

            const string sec = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            var now = DateTime.UtcNow;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);


            var token = (JwtSecurityToken)tokenHandler
                .CreateJwtSecurityToken(issuer: "http://localhost:50191", audience: "http://localhost:50191", subject: claimsIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}