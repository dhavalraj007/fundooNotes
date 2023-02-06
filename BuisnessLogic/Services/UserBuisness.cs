using BuisnessLogic.Interfaces;
using Model;
using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLogic.Services
{
    public class UserBuisness:IUserBuisness
    {
        private readonly IUserRepository userRepository;
        public UserBuisness(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }   

        public UserEntity Register(RegisterModel model)
        {
            return userRepository.Register(model);
        }
    }
}
