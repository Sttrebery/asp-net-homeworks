using homework_4.Repositories;
using homework_4.Services;

namespace homework_4.Middlewares;

public class LifetimeMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync( HttpContext context, IClockService clockService,
        IUserService userService, IValidatorService validatorService)
    {
        // Сохраняем ID в Items чтобы сравнить их с теми, что придут в endpoint
        context.Items["MiddlewareIds"] = new
        {
            Singleton = clockService.ServiceId,
            Scoped = userService.ServiceId,
            Transient = validatorService.ServiceId
        };

        await next(context);
    }
}