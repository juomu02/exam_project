using Microsoft.EntityFrameworkCore;
using App.Data;
using App.Entities;

namespace App.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<int> AddAsync(User user)
        {

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
            return user.Id;
        }

        public async Task<User> GetByIdAsync(int userId)
        {
            return await dbContext.Users.FindAsync(userId);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetByUserNameAsync(string userName)
        {
            return await dbContext.Users.SingleOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<bool> UpdateAsync(User user)
        {
            dbContext.Users.Update(user);
            int result = await dbContext.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int userId)
        {
            dbContext.Users.Remove(new User { Id = userId });
            int result = await dbContext.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }
            return false;
        }
    }
}