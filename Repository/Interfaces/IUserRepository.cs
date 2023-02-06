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
    }
}
