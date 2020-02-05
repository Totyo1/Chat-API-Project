
using ChatAPIProject.Models.InputModels.User;

namespace Servise.Contracts
{
    public interface IUserService
    {
         void CreateUser(UserInputModel inputModel);

    }
}
