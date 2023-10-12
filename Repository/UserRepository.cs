using BharatMedicsV2.DataFiles;
using BharatMedicsV2.Models;
using BharatMedicsV2.Models.DTOs;
using BharatMedicsV2.Repository.IRepository;
using BharatMedicsV2.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace BharatMedicsV2.Repository
{
    public class UserRepository : IUser
    {

        private readonly DataContext dataContext;
        private string SecretKey;

        public UserRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
            //SecretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }
        public async Task<bool> CheckEmail(string email)
        {
            return await dataContext.Users.AnyAsync(u => u.Email == email);
        }


        public async Task<bool> CheckUserName(string userName)
        {
            return await dataContext.Users.AnyAsync(u => u.UserName == userName);
        }

        public async Task<bool> DeleteUser(int UserId)
        {
            var user = await dataContext.Users.FirstOrDefaultAsync(u => u.UserId == UserId);
            dataContext.Users.Remove(user);
            dataContext.SaveChanges();
            return true;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await dataContext.Users.ToListAsync();
        }

        public async Task<User> GetUser(UserDTO userDTO)
        {
            var user = await dataContext.Users.FirstOrDefaultAsync(u => u.UserName == userDTO.UserName);
            return user;
        }

        public string JwtToken(User user)
        {
            var temp = dataContext.Users.Where(u => u.UserName.ToLower() == user.UserName && u.Password == user.Password).FirstOrDefault();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);


            var TokenDiscript = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, temp.Name),
                    new Claim(ClaimTypes.Email, temp.Email),
                    new Claim(ClaimTypes.Role, temp.Role)


                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(TokenDiscript);
            return tokenHandler.WriteToken(token);
        }

        public async Task<bool> RegisterUser(User user)
        {
            user.Password = HashPassword.PasswordHasher(user.Password);
            user.Token = "";
            user.Role = "Doctor";

            await dataContext.Users.AddAsync(user);
            await dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<string> SignIn(UserDTO userDTO)
        {
            var user = await dataContext.Users.FirstOrDefaultAsync(u => u.UserName == userDTO.UserName);
            user.Token = JwtToken(user);
            return user.Token;
        }
    }
}
