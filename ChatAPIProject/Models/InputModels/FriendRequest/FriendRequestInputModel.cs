using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAPIProject.Models.InputModels.FriendRequest
{
    public class FriendRequestInputModel
    {
        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public string Status { get; set; }
    }
}
