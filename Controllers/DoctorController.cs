using BharatMedicsV2.Models.DTOs;
using BharatMedicsV2.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BharatMedicsV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : Controller
    {
        private readonly IDoctor doctorRepository;
        public DoctorController(IDoctor _doctorRepository)
        {
            doctorRepository = _doctorRepository;
        }

        #region Login Request Doctor

        [HttpPost("Login")]   
        public async Task<IActionResult> Login([FromBody] SignInDTO model)
        {

            var login = await doctorRepository.Login(model);
            if (login.Doctor == null || String.IsNullOrEmpty(login.Token))
            {
                return BadRequest(new { Message = "UserName or password is incorrect" });
            }
            return Ok(login);
        }

        #endregion

        #region Register Doctor 
        [HttpPut("Register")]  
        public async Task<IActionResult> Register([FromBody] RegistrationAuthenticationDTO model)
        {

            bool check = doctorRepository.CheckEmail(model.Email);

            if (!check)
            {
                return BadRequest(new { Message = "User already exists" });
            }

            var doctor = doctorRepository.RegisterDoc(model);
            if (doctor == null)
            {
                return BadRequest(new { Message = "User already exists" });

            }
            return Ok();
        }
        #endregion
    }
}
