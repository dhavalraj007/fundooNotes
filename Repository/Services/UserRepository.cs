using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model;
using Repository.Context;
using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
            userEntity.Password= EncryptPassword(model.Password);

            context.UserTable.Add(userEntity);
            int res = context.SaveChanges();
            if (res > 0)
                return userEntity;
            else
                return null;
        }

        public string EncryptPassword(string password)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(bytes);
        }

        public string Login(LoginModel login)
        {
            string encodedPassword = EncryptPassword(login.Password);
            var userEntity = (from user in context.UserTable
                           where user.Email == login.Email && user.Password == encodedPassword
                           select user).FirstOrDefault();
            if (userEntity!=null)
            {
                return GenerateJWTToken(userEntity.Email, userEntity.UserId);
            }
            else
                return null;

        }

        private string GenerateJWTToken(string Email,long UserId)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]));
            var credientials = new SigningCredentials(secretKey,SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim("Email",Email),
                new Claim("UserId",UserId.ToString())
            };
            var tocken = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims,
                expires:DateTime.Now.AddHours(4),
                signingCredentials:credientials
                );
            return new JwtSecurityTokenHandler().WriteToken(tocken);
        }
    }
}
