using WebAPI.Models;

namespace WebAPI.Service.Interface
{
    public interface IUserService
    {
        List<User> GetUsers();
        User GetUserById(int id);
        bool AddUser(User user);
        bool UpdateUser(User user);
        User DeleteUser(int id);
    }
}
