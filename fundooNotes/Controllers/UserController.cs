
using BuisnessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Repository.Entity;
using System.Collections.Generic;
using System.Linq;

namespace fundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBuisness userBuisness;

        public UserController(IUserBuisness userBuisness)
        {
            this.userBuisness = userBuisness;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterModel model)
        {
            UserEntity userEntity = userBuisness.Register(model);
            if(userEntity != null) {
                return Ok(new ResponseModel<UserEntity> { Status = true,Message="Register Successful",Data=userEntity});
            }
            else
                return BadRequest(new ResponseModel<UserEntity> { Status = false, Message = "Register UnSuccessful", Data = userEntity });
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginModel login)
        {
            string jwt = userBuisness.Login(login);
            if (jwt != null)
            {
                return Ok(new ResponseModel<string> { Status = true, Message = "login Successful", Data = jwt});
            }
            else
                return BadRequest(new ResponseModel<string> { Status = false, Message = "Register UnSuccessful", Data = jwt });
        }

        [HttpPost]
        [Route("ForgetPassword")]
        public IActionResult ForgetPassword(string Email)
        {
            var forget = userBuisness.ForgetPassword(Email);

            if (forget!= null)
            {
                return Ok(new ResponseModel<string> { Status = true, Message = "Mail sent Successfully", Data = forget});
            }
            else
                return BadRequest(new ResponseModel<string> { Status = false, Message = "Mail not sent", Data = forget});
        }

        [Authorize]
        [HttpPatch]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(ResetPassword resetPassword)
        {
            var Email = User.Claims.FirstOrDefault(c => c.Type== "Email").Value;
            var reset = userBuisness.ResetPassword(resetPassword,Email.ToString());

            if (reset != null)
            {
                return Ok(new ResponseModel<string> { Status = true, Message = " reset Successfully", Data = reset });
            }
            else
                return BadRequest(new ResponseModel<string> { Status = false, Message = "reset failed", Data = reset });
        }


        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var users = userBuisness.GetAllUsers();
            if(users!=null)
            {
                return Ok(new ResponseModel<List<UserEntity>> { Status = true, Message = " GetAllUsers Successfully executed", Data = users});
            }
            else
                return BadRequest(new ResponseModel<List<UserEntity>> { Status = true, Message = " GetAllUsers failed", Data = users }); 
        }
    }
}
