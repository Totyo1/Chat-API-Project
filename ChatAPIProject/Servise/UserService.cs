using AutoMapper;

using ChatAPIProject.Data;
using ChatAPIProject.Models.DataModels;
using ChatAPIProject.Models.InputModels.User;
using Models.ServiceModels.User;
using Service.Contracts;

namespace ChatAPIProject.Service
{
    public class UserService : IUserService
    {
        private UserCode userCode;
        private MapperConfiguration config;

        public UserService()
        {
            this.userCode = new UserCode();
            this.config = new MapperConfiguration(cfg => cfg.CreateMap<UserInputModel, UserDataModel>());
        }  

        public void CreateUser(UserInputModel inputModel)
        {
            IMapper mapper = config.CreateMapper();
            UserDataModel user = mapper.Map<UserDataModel>(inputModel);
            this.userCode.CreateUser(user);
        }

        public void DeleteUser(int id)
        {
            this.userCode.DeleteUser(id);
        }

        public UserDataModel GetUser(string username, string password)
        {
            return this.userCode.GetUserByUsernameAndPassword(username, password);
        }

        public IsExistUserServiceModel IsExist(int id)
        {
            return this.userCode.IsExist(id);
        }
    }
}