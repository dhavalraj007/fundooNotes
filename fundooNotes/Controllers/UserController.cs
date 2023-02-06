
using BuisnessLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Repository.Entity;

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
    }
}
