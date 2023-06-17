using Microsoft.EntityFrameworkCore;
using WebAPI.Exception;
using WebAPI.Models;
using WebAPI.Repository.Data;
using WebAPI.Repository.Interface;

namespace WebAPI.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApiDbContext _dbContext;

        public UserRepository(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<User> GetUsers()
        {
            return _dbContext.Users.ToList();
        }


        public User GetUserById(int id)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Id == id);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            return user;
        }

        public bool AddUser(User user)
        {
            try
            {
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                return true; // User added successfully
            }
            catch
            {
                return false; // Failed to add user
            }
        }


        public bool UpdateUser(User user)
        {
            var existingUser = _dbContext.Users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser == null)
            {
                return false; // User not found, return false indicating failure
            }

            try
            {
                _dbContext.Entry(existingUser).CurrentValues.SetValues(user);
                _dbContext.SaveChanges();
                return true; // User updated successfully
            }
            catch (DbUpdateException)
            {
                // Handle specific database update exception if needed
                return false; // Failed to update user
            }
        }



        public User DeleteUser(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return null;
            }

            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();

            return user;
        }
    }
}
