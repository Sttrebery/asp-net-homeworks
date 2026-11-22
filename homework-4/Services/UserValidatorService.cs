using System.ComponentModel.DataAnnotations;
using homework_4.Models;

namespace homework_4.Services;

public class UserValidatorService : IValidatorService
{
    private readonly Guid _serviceId = Guid.NewGuid();
    public Guid ServiceId { get =>  _serviceId; }
    
    public bool TryValidate(User user, out List<string> errors)
    {
        if (user == null)
        {
            errors = ["Данные не переданы"];
            return false;
        }
        
        var context = new ValidationContext(user);
        var results = new List<ValidationResult>();
        
        bool isValid = Validator.TryValidateObject(
            user,
            context,
            results,
            validateAllProperties: true);

        errors = results
            .Select(r => r.ErrorMessage ?? "Ошибка валидации")
            .ToList();

        return isValid;
    }
}