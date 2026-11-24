namespace homework_4.Services;

//сервис для 3 задания с областью Scope
public class ScopedService : IServiceId
{
    private readonly Guid _serviceId = Guid.NewGuid();
    public Guid ServiceId { get =>  _serviceId; }
}