using System.Runtime.CompilerServices;
using App.Entities;

namespace App.Repositories
{
    public interface IUserRepository
    {
        Task<int> AddAsync(User user);
        Task<User> GetByIdAsync(int userId);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByUserNameAsync(string userName);
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteAsync(int userId);
    }
}