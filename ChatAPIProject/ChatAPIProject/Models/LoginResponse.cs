using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace ChatAPIProject.Models
{
    public class LoginResponse
    {
        public HttpResponseMessage ResponseMsg;

        public LoginResponse()
        {
            this.ResponseMsg = new HttpResponseMessage();
        }
    }
}