
using ChatAPIProject.Models.InputModels.User;

namespace Service.Contracts
{
    public interface IUserService
    {
         void CreateUser(UserInputModel inputModel);

    }
}
