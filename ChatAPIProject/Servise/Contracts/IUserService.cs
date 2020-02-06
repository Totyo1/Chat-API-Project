
using ChatAPIProject.Models.DataModels;
using ChatAPIProject.Models.InputModels.User;

namespace Service.Contracts
{
    public interface IUserService
    {
         void CreateUser(UserInputModel inputModel);

         User GetUser(string username, string password);
    }
}
