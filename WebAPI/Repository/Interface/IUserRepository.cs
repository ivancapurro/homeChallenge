using Microsoft.AspNetCore.Mvc;
using System.Runtime.ConstrainedExecution;
using WebAPI.Models;

namespace WebAPI.Repository.Interface
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        User GetUserById(int id);
        bool AddUser(User user);
        bool UpdateUser(User user);
        User DeleteUser(int id);
    }
}
