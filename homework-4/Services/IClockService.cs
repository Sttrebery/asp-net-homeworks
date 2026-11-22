namespace homework_4.Services;

public interface IClockService : IServiceId
{
    string GetServerTime();
}