using ChatAPIProject.Models.InputModels.Home;
using ChatAPIProject.Service;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatAPIProject.Controllers
{
    public class HomeController : Controller
    {
        private const string ERROR_MESSAGE = "Incorrect username or password!Username is required.Password must be minimum 5 and maximum 30 symbols.";
        private const string SWAGGER_INDEX_PAGE = "http://localhost:52661/swagger/ui/index";

        private IUserService userService;
        private Authenticate authenticate;

        public HomeController()
        {
            this.userService = new UserService();
            this.authenticate = new Authenticate(userService);
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [HttpPost]
        public ActionResult Index(HomeLoginInputModel model)
        {
            var token = this.authenticate.AuthUser(model.Username, model.Password);

            if (!ModelState.IsValid || String.IsNullOrWhiteSpace(token))
            {
                this.ViewData["ErrorMessage"] = ERROR_MESSAGE;
                return this.View(model);
            }

            this.Response.Cookies.Set(new HttpCookie("Token", token));

            return this.Redirect(SWAGGER_INDEX_PAGE);
        }
    }
}
