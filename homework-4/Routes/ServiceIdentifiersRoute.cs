using homework_4.Services;

namespace homework_4.Routes;

public static class ServiceIdentifiersRoute
{
    public static void MapServiceIdentifiers(this IEndpointRouteBuilder endpoints)
    {
        endpoints.Map("/lifetimes", (
                HttpContext context,
                IClockService clockService,
                IUserService userService,
                IValidatorService validatorService) =>
            Results.Ok(new
            {
                FromMiddleware = context.Items["MiddlewareIds"],
                FromEndpoint = new
                {
                    Singleton = clockService.ServiceId,
                    Scoped = userService.ServiceId,
                    Transient = validatorService.ServiceId
                }
            })
        );

        endpoints.Map("/scoped", (
                HttpContext _,
                IClockService clockService,
                IUserService userService,
                IValidatorService validatorService) =>
            Results.Ok(new
            {
                FromEndpoint = new
                {
                    Singleton = clockService.ServiceId,
                    Scoped = userService.ServiceId, //чтобы посмотреть изменения вот этого
                    Transient = validatorService.ServiceId
                }
            })
        );
        
    }
}