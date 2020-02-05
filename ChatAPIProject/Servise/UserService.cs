using ChatAPIProject.Models.ServiceModels.User;
using Servise.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatAPIProject.Service
{
    public class UserService : IUserService
    {
        public UserServiceModel CreateUser(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}