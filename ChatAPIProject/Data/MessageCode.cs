using ChatAPIProject.Models.DataModels;
using ChatAPIProject.Models.ServiceModels.Message;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ChatAPIProject.Data
{
    public class MessageCode
    {
        private readonly string connString;

        public MessageCode()
        {
            this.connString = ConfigurationManager.AppSettings["myDbConnection"];
        }
        
        public bool SendMessage(MessageServiceModel model)
        {
            throw new NotImplementedException();
        }
    }
}