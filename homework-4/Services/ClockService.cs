namespace homework_4.Services;

public class ClockService : IClockService
{
    private readonly Guid _serviceId = Guid.NewGuid();
    public Guid ServiceId { get =>  _serviceId; }
    
    public string GetServerTime()
    {
        return $"Server time: {DateTime.Now:T}\n";
    }
}