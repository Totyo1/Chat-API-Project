using ChatAPIProject.Common.Automapping;
using ChatAPIProject.Models.ServiceModels.User;
using System.ComponentModel.DataAnnotations;

namespace ChatAPIProject.Models.InputModels.User
{
    public class UserInputModel : IMapTo<UserServiceModel>
    {
        private const int MIN_PASSWORD_LENGHT = 5;
        private const int MAX_PASSWORD_LENGHT = 120;

        [Required]
        public string Username { get; set; }

        [Required]
        [Range(MIN_PASSWORD_LENGHT, MAX_PASSWORD_LENGHT,ErrorMessage = "Password must be between 5 and 120 symbols.")]
        public string Password { get; set; }
    }
}