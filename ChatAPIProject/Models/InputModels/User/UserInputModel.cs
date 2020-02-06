using ChatAPIProject.Common.Automapping;
using ChatAPIProject.Models.ServiceModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatAPIProject.Models.InputModels.User
{
    public class UserInputModel : IMapTo<UserServiceModel>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}