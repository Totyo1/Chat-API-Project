using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChatAPIProject.Controllers
{
    public class BaseController<T> : ApiController
    {
        public BaseController(T service)
        {
            this.Service = service;
        }

        protected T Service { get; set; }
    }
}
