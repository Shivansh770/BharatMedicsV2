using BharatMedicsV2.Models.DTOs;
using BharatMedicsV2.Models;
using BharatMedicsV2.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BharatMedicsV2.Repository.IRepository;
using BharatMedicsV2.Security;

namespace BharatMedicsV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class UserController : Controller
    {
        public readonly IUser userRepository;
        public UserController(IUser userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpPost]
        [Route("login")]

        public async Task<IActionResult> Login([FromBody] UserDTO user)
        {


            if (user == null)
            {

                return BadRequest();
            }

            var temp = await userRepository.GetUser(user);
            if (temp == null)
            {
                return NotFound(new { Message = "User Name does not exists" });
            }

            if (!HashPassword.Verify(user.Password, temp.Password))
            {

                return BadRequest(new { Message = "Password is incorrect" });
            }

            temp.Token = await userRepository.SignIn(user);


            return Ok(new
            {
                Token = temp.Token,
                Message = "Login Success"
            });

        }

        [HttpPost]
        [Route("SignUp")]

        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (user == null)
            {
                return NotFound();
            }

            if (await userRepository.CheckUserName(user.UserName))
            {
                return BadRequest(new
                {
                    Message = "User Name already Exists"
                });

            }

            if (await userRepository.CheckEmail(user.Email))
            {
                return BadRequest(
                    new
                    {
                        Message = "Email ALready Exists"
                    }

                    );
            }
            

            bool dz = await userRepository.RegisterUser(user);


            return Ok(new { Message = "SUCCESS ADDED" });

        }

        [Authorize]
        [HttpGet]
        [Route("GetAllUser")]

        public async Task<ActionResult<User>> GetAllUser()
        {
            var temp = await userRepository.GetAllUsers();
            return Ok(temp);
        }
        [HttpDelete]
        [Route("DeleteUser")]

        public async Task<ActionResult> DeleteUser(int id)
        {


            var temp = await userRepository.DeleteUser(id);
            return Ok(temp);
        }

    }
}

