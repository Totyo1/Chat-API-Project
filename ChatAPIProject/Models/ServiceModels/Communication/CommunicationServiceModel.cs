using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatAPIProject.Models.ServiceModels.Communication
{
    public class CommunicationServiceModel
    {
        public int Id { get; set; }

        public int FirstUser { get; set; }

        public int SecondUser { get; set; }
    }
}