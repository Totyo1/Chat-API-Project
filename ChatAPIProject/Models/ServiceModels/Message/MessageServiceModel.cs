
namespace ChatAPIProject.Models.ServiceModels.Message
{
    public class MessageServiceModel
    {
        public int Id { get; set; }

        public int CommunicationId { get; set; }

        public string Date { get; set; }

        public string Content { get; set; }

        public int AuthorId { get; set; }

        public int ReceiverId { get; set; }
    }
}