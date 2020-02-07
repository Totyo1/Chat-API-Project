using ChatAPIProject.Models.ServiceModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatAPIProject.Models.DataModels
{
    public class UserDataModel 
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}