using homework_4.Models;
using homework_4.Services;

namespace homework_4.Repositories;

public interface IUserRepository
{
    List<User> GetUsers();
    User? GetUser(int id);
    User Save(User user);
    void DeleteUser(int id);
}