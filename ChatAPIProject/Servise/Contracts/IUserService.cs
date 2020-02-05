using ChatAPIProject.Models.ServiceModels;
using ChatAPIProject.Models.ServiceModels.User;

namespace Servise.Contracts
{
    public interface IUserService
    {
        UserServiceModel CreateUser(string username, string password);

    }
}
