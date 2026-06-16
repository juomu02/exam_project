using App.Entities;

namespace App.Services
{
    public interface IUserService
    {
        Task<int> AddAsync(string userName, string email, string password);
        // GetAsync turbūt nereikės, nes yra Login
        Task<User> GetAsync(int userId);
        Task<bool> ChangePasswordAsync(int userId, string currentPassword,
            string newPassword);
        Task<bool> ChangeEmailAsync(int userId, string email);
        Task<bool> DeleteAsync(int userId);
        Task<User> LoginAsync(string userEmail, string password);
    }
}