﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.InputModels.Message
{
    public class MessageInputModel
    {
        public string Content { get; set; }

        public int ReceiverId { get; set; }
        
    }
}