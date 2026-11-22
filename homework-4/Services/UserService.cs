using System.ComponentModel.DataAnnotations;
using homework_4.Exceptions;
using homework_4.Models;
using homework_4.Repositories;

namespace homework_4.Services;

public class UserService(IUserRepository repository, IValidatorService validator) : IUserService
{
    private readonly Guid _serviceId = Guid.NewGuid();
    public Guid ServiceId { get =>  _serviceId; }
    
    public List<User> ListUsers() => repository.GetUsers();

    public User FindUserById(int id)
    {
        var user = repository.GetUser(id);
        
        return user ?? throw new UserNotFoundException(id);
    }

    public User RegisterUser(string login, string email, string password)
    {
        var user = new User(login, email, password);
        
        if (!validator.TryValidate(user, out var errors))
        {
            throw new ValidationException(string.Join(", ", errors));
        }
        
        return repository.Save(user);
    }

    public User UpdateUser(User user) => repository.Save(user);

    public void DeleteUserById(int id) => repository.DeleteUser(id);
}