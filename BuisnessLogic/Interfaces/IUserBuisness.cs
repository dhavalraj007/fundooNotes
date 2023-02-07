using Model;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLogic.Interfaces
{
    public interface IUserBuisness
    {
        public UserEntity Register(RegisterModel model);
        public string Login(LoginModel login);
    }
}
