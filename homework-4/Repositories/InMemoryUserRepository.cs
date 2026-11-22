using homework_4.Models;

namespace homework_4.Repositories;

public class InMemoryUserRepository : IUserRepository
{
    private readonly Dictionary<int, User> _users = new();
    private int _idCounter = 1;
    
    public List<User> GetUsers() => _users.Values.ToList();

    public User? GetUser(int id) => _users.GetValueOrDefault(id, null);

    public User Save(User user)
    {
        if (user.Id == -1)
        {
            user.Id = _idCounter++;
            _users.Add(user.Id, user);
        }
        else
        {
            _users[user.Id] = user;
        }
        
        return user;
    }

    public void DeleteUser(int id) => _users.Remove(id);
}