using WebApplication_Project1.DTOs;

namespace WebApplication_Project1.Services
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginDTO userDTO);

        Task<string> CreateToken();
    }
}
