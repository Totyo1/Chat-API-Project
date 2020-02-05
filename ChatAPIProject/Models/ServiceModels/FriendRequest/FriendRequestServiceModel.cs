using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ServiceModels.FriendRequest
{
    public class FriendRequestServiceModel
    {
        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public string Status { get; set; }
    }
}
