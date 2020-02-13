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
