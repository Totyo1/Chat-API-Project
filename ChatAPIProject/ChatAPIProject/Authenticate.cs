using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatAPIProject
{
    public class Authenticate
    {
        private IUserService userService;

        public Authenticate(IUserService userService)
        {
            this.userService = userService;
        }


        //public string GetToken(string username, string password)
        //{
        //    var user = 
        //}
    }
}