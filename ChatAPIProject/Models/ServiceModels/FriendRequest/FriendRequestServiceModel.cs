
namespace ChatAPIProject.Models.ServiceModels.FriendRequest
{
    public class FriendRequestServiceModel
    {
        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public string Status { get; set; }
    }
}
