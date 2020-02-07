using System.ComponentModel.DataAnnotations;

namespace ChatAPIProject.Models.InputModels.Communication
{
    public class CommunicationInputModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int FirstUserId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int SecondUserId { get; set; }
    }
}