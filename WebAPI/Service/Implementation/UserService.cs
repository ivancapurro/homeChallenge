using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Repository.Interface;
using WebAPI.Service.Interface;

namespace WebAPI.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        public User GetUserById(int id)
        {
            return _userRepository.GetUserById(id);

        }

        public bool AddUser(User user)
        {
             var isUserAdded = _userRepository.AddUser(user);
            return isUserAdded;
        }

        public bool UpdateUser(User user)
        {
            bool updated = _userRepository.UpdateUser(user);
            return updated; // Return the result of the update operation
        }

        public User DeleteUser(int id)
        {
            return _userRepository.DeleteUser(id);
        }
    }

}
