﻿
namespace ChatAPIProject.Models.InputModels.Message
{
    public class MessageInputModel
    {
        public string Content { get; set; }

        public int SenderID { get; set; }

        public int ReceiverID { get; set; }

    }
}