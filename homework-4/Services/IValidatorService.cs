using homework_4.Models;

namespace homework_4.Services;

public interface IValidatorService : IServiceId
{
    bool TryValidate(User user, out List<string> errors);
}