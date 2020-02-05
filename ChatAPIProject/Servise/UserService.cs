using ChatAPIProject.Models.InputModels;
using Servise.Contracts;
using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChatAPIProject.Data;
using ChatAPIProject.Models.DataModels;
using ChatAPIProject.Models.InputModels.User;

namespace ChatAPIProject.Service
{
    public class UserService : IUserService
    {
        private UserCode userCode;
        private MapperConfiguration config;
        public UserService()
        {
            this.userCode = new UserCode();
            this.config = new MapperConfiguration(cfg => cfg.CreateMap<UserInputModel, User>());

        }  
        public void CreateUser(UserInputModel inputModel)
        {
            IMapper mapper = config.CreateMapper();
            User user = mapper.Map<User>(inputModel);

        }
    }
}