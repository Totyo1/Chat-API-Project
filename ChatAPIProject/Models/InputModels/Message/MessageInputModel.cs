using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatAPIProject.Models.InputModels.Message
{
    public class MessageInputModel
    {
        public int CommunicationId { get; set; }

        public string Content { get; set; }

        public int ReceiverId { get; set; }
    }
}