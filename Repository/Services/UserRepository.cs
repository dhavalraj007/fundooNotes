using Microsoft.Extensions.Configuration;
using Model;
using Repository.Context;
using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Services
{
    public class UserRepository:IUserRepository
    {
        private readonly FundooContext context;
        private readonly IConfiguration configuration;

        public UserRepository(FundooContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public UserEntity Register(RegisterModel model)
        {
            UserEntity userEntity = new UserEntity();
            userEntity.FirstName= model.FirstName;
            userEntity.LastName= model.LastName;
            userEntity.Email= model.Email;
            userEntity.Password= model.Password;

            context.UserTable.Add(userEntity);
            int res = context.SaveChanges();
            if (res > 0)
                return userEntity;
            else
                return null;
        }
    }
}
