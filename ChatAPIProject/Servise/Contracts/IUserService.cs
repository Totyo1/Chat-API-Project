using ChatAPIProject.Models.DataModels;
using ChatAPIProject.Models.InputModels.User;
using Models.ServiceModels.User;

namespace Service.Contracts
{
    public interface IUserService
    {
         void CreateUser(UserInputModel inputModel);

         UserDataModel GetUser(string username, string password);

        void DeleteUser(int id);

        IsExistUserServiceModel IsExist(int id);
    }
}
