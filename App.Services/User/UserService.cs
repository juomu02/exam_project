using Microsoft.AspNetCore.Identity;
using App.Entities;
using App.Repositories;

namespace App.Services
{
    public class UserService : IUserService
    {
        public readonly IUserRepository userRepository;
        private readonly IPasswordHasher<User> passwordHasher;

        public UserService(IUserRepository userRepository,
            IPasswordHasher<User> passwordHasher)
        {
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
        }
        public async Task<int> AddAsync(string userName, string email,
            string password)
        {
            if (await userRepository.GetByUserNameAsync(userName) != null)
            {
                throw new ArgumentException("User name is already in use.");
            }

            if (await userRepository.GetByEmailAsync(email) != null)
            {
                throw new ArgumentException("Email is already in use.");
            }

            var user = new User
            {
                UserName = userName,
                Email = email,
                Role = "user"
            };

            user.PasswordHash = passwordHasher.HashPassword(user, password);

            return await userRepository.AddAsync(user);
        }

        public async Task<User> GetAsync(int userId)
        {
            return await userRepository.GetByIdAsync(userId);
        }

        public async Task<bool> ChangePasswordAsync(int userId,
            string currentPassword, string newPassword)
        {
            var user = await userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }
            if (!passwordHasher.VerifyHashedPassword(user, user.PasswordHash,
                currentPassword).Equals(PasswordVerificationResult.Success))
            {
                throw new ArgumentException("You have entered an incorrect password.");
            }

            user.PasswordHash = passwordHasher.HashPassword(user, newPassword);
            return await userRepository.UpdateAsync(user);
        }

        public async Task<bool> ChangeEmailAsync(int userId, string email)
        {
            var user = await userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (await userRepository.GetByEmailAsync(email) != null)
            {
                throw new ArgumentException("Email is already in use.");
            }

            user.Email = email;
            return await userRepository.UpdateAsync(user);
        }

        public async Task<bool> DeleteAsync(int userId)
        {
            return await userRepository.DeleteAsync(userId);
        }
        public async Task<User> LoginAsync(string userEmail, string password)
        {
            var user = await userRepository.GetByEmailAsync(userEmail);
            if (user == null)
            {
                return null;
            }

            var result = passwordHasher.VerifyHashedPassword(user,
                user.PasswordHash, password);
            if (result == PasswordVerificationResult.Success)
            {
                return user;
            }
            return null;
        }
    }
}