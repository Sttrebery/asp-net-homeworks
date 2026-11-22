using homework_4.Models;
namespace homework_4.Services;

public interface IUserService : IServiceId
{
    List<User> ListUsers();
    User FindUserById(int id);
    User RegisterUser(string login, string email, string password);
    User UpdateUser(User user);
    void DeleteUserById(int id);
}