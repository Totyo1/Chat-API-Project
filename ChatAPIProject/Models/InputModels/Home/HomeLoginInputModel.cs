using System.ComponentModel.DataAnnotations;

namespace ChatAPIProject.Models.InputModels.Home
{
    public class HomeLoginInputModel
    {
        private const int MIN_PASSWORD_LENGHT = 5;
        private const int MAX_PASSWORD_LENGHT = 30;

        [Required]
        public string Username { get; set; }

        [Required]
        [MinLength(MIN_PASSWORD_LENGHT)]
        [MaxLength(MAX_PASSWORD_LENGHT)]
        public string Password { get; set; }
    }
}
