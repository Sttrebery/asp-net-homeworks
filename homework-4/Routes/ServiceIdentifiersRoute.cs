using homework_4.Services;

namespace homework_4.Routes;

public static class ServiceIdentifiersRoute
{
    public static void MapServiceIdentifiers(this IEndpointRouteBuilder endpoints)
    {
        endpoints.Map("/lifetimes", async (
                HttpContext context,
                IClockService clockService,
                IUserService userService,
                IValidatorService validatorService) =>
            
            await context.Response.WriteAsJsonAsync(new
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

        endpoints.Map("/scoped",  async(
                HttpContext context,
                IClockService clockService,
                IUserService userService,
                IValidatorService validatorService) =>
            
            await context.Response.WriteAsJsonAsync(new
            {
                FromEndpoint = new
                {
                    Singleton = clockService.ServiceId,
                    Scoped = userService.ServiceId, //чтобы посмотреть изменения вот этого
                    Transient = validatorService.ServiceId
                }
            })
        );
        
        
        // 3 задание
        endpoints.Map("/scoped-compare", async (
            HttpContext context) =>
        {
            var fromRequestServices = context.RequestServices.GetRequiredService<IServiceId>();

            Guid _isolatedScopeId;
            using (var scope = context.RequestServices.CreateScope())
            {
                var isolatedService = scope.ServiceProvider.GetRequiredService<IServiceId>();
                _isolatedScopeId = isolatedService.ServiceId;
            }

            await context.Response.WriteAsJsonAsync(new
            {
                requestServicesId = fromRequestServices.ServiceId,
                isolatedScopeId = _isolatedScopeId,
                areEqual = fromRequestServices.ServiceId == _isolatedScopeId
            });
        });
    }
}