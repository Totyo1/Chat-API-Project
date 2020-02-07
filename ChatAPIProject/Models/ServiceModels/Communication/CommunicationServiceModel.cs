using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatAPIProject.Models.ServiceModels.Communication
{
    public class CommunicationServiceModel
    {
        public int Id { get; set; }

        public int FirstUserId { get; set; }

        public int SecondUserId { get; set; }
    }
}