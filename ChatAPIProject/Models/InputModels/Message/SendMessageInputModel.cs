
using System.ComponentModel.DataAnnotations;

namespace Models.InputModels.Message
{
    public class SendMessageInputModel
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public int ReceiverId { get; set; }
    }
}
