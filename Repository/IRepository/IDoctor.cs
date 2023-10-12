using BharatMedicsV2.Models;
using BharatMedicsV2.Models.DTOs;

namespace BharatMedicsV2.Repository.IRepository
{
    public interface IDoctor
    {
        Task<SignInActionDTO> Login(SignInDTO signInDTO);

        Task<Doctor> RegisterDoc(RegistrationAuthenticationDTO registrationAuthenticationDTO);

        bool CheckEmail(string Email);
    }
}
