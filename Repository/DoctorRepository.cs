using BharatMedicsV2.DataFiles;
using BharatMedicsV2.Models;
using BharatMedicsV2.Models.DTOs;
using BharatMedicsV2.Repository.IRepository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BharatMedicsV2.Repository
{
    public class DoctorRepository : IDoctor
    {
        private readonly DataContext dataContext;
        private string SecretKey;

        public DoctorRepository(DataContext _dataContext)
        {
            this.dataContext = _dataContext;
            //SecretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }
        public bool CheckEmail(string Email)
        {
            var temp = dataContext.Doctors.Where(u => u.Email == Email).FirstOrDefault();
            if (temp != null)
            {
                return false;
            }
            return true;

        }

        public async Task<SignInActionDTO> Login(SignInDTO signInDTO)
        {
           var doc = dataContext.Doctors.Where(u => u.Email.ToLower() ==  signInDTO.Email.ToLower() && u.Password== signInDTO.Password).FirstOrDefault();
            if (doc==null)
            {
                return new SignInActionDTO()
                {
                    Token = "",
                    Doctor = null
                };

            }
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);


            var TokenDiscript = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, doc.Id.ToString()),
                    new Claim(ClaimTypes.Email,doc.Email.ToString())


                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)


            };

            var token = tokenHandler.CreateToken(TokenDiscript);

            SignInActionDTO signInActionDTO = new SignInActionDTO()
            {
                Token = tokenHandler.WriteToken(token),
                Doctor = doc
            };

            return signInActionDTO;

        }

        public async Task<Doctor> RegisterDoc(RegistrationAuthenticationDTO registrationAuthenticationDTO)
        {
            Doctor dct = new()
            {

                Email = registrationAuthenticationDTO.Email,Password = registrationAuthenticationDTO.Password,
            };

            await dataContext.Doctors.AddAsync(dct);
            await dataContext.SaveChangesAsync();
            dct.Password = "";
            return dct;
        }
    }
}
