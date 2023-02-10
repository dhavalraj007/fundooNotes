using Model;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
    public interface IUserRepository
    {
        public UserEntity Register(RegisterModel model);
        public string Login(LoginModel login);

        public string ForgetPassword(string Email);
        public string ResetPassword(ResetPassword resetPassword, string Email);
        public UserTicket CreateTicketForPassword(string Email,string token);

    }
}
