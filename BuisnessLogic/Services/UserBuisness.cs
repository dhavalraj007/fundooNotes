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

        public string Login(LoginModel login)
        {
            return userRepository.Login(login);
        }
        public string ForgetPassword(string Email)
        {
            return userRepository.ForgetPassword(Email);
        }

        public string ResetPassword(ResetPassword resetPassword, string Email)
        {
            return userRepository.ResetPassword(resetPassword, Email);
        }

        public UserTicket CreateTicketForPassword(string Email, string Password)
        {
            return userRepository.CreateTicketForPassword(Email, Password);
        }
    }
}
