using BharatMedicsV2.Models;
using BharatMedicsV2.Models.DTOs;

namespace BharatMedicsV2.Repository.IRepository
{
    public interface IUser
    {
        Task<bool> CheckUserName(string userName);
       
        Task<bool> CheckEmail(string email);

        Task<bool> RegisterUser(User user);

        Task<string> SignIn(UserDTO userDTO);
        Task<User> GetUser(UserDTO userDTO);

        Task<List<User>> GetAllUsers();

        Task<bool> DeleteUser(int UserId);

        string JwtToken(User user);

    }
}
