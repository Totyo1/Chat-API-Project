using ChatAPIProject.Models.DataModels;
using ChatAPIProject.Models.InputModels;
using ChatAPIProject.Models.ServiceModels;
using ChatAPIProject.Models.ServiceModels.User;

namespace Servise.Contracts
{
    public interface IUserService
    {
         void CreateUser(UserInputModel inputModel);

    }
}
