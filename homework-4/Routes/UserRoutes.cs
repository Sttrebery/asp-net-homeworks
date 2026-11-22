using System.ComponentModel.DataAnnotations;
using homework_4.DTOs;
using homework_4.Exceptions;
using homework_4.Models;
using homework_4.Services;

namespace homework_4.Routes;

public static class UserRoutes
{
    public static void MapUsers(this IEndpointRouteBuilder endpoints)
    {
        endpoints.Map("/users", async (HttpContext context, IUserService userService) =>
        {
            switch (context.Request.Method)
            {
                case "GET":
                {
                    await context.Response.WriteAsJsonAsync(userService.ListUsers());
                    break;
                }
                case "POST":
                {
                    var dto = await context.Request.ReadFromJsonAsync<UserCreateDTO>();

                    if (dto == null)
                    {
                        context.Response.StatusCode = 400;
                    }
                    else
                    {
                        try
                        {
                            var user = userService.RegisterUser(dto.Login, dto.Email, dto.Password);
                        
                            context.Response.StatusCode = 201;
                            await context.Response.WriteAsJsonAsync(user);
                        }
                        catch (ValidationException e)
                        {
                            context.Response.StatusCode = 400;
                            await context.Response.WriteAsJsonAsync(e.Message);
                        }
                        
                    }
                    
                    break;
                }
                case "PUT":
                {
                    var user = await context.Request.ReadFromJsonAsync<User>();

                    if (user == null)
                    {
                        context.Response.StatusCode = 400;
                    }
                    else
                    {
                        user = userService.UpdateUser(user);
                        
                        await context.Response.WriteAsJsonAsync(user);
                    }
                    
                    break;
                }
                default:
                {
                    context.Response.StatusCode = 405;
                    break;
                }
            }
        });
        
        endpoints.Map("/users/{id:int}", async (HttpContext context, IUserService userService, int id) =>
        {
            switch (context.Request.Method)
            {
                case "GET":
                {
                    try
                    {
                        var user = userService.FindUserById(id);
                        
                        await context.Response.WriteAsJsonAsync(user);
                    }
                    catch (UserNotFoundException e)
                    {
                        context.Response.StatusCode = 404;
                        await context.Response.WriteAsync(e.Message);
                    }
                    
                    break;
                }
                case "DELETE":
                {
                    userService.DeleteUserById(id);
                    
                    context.Response.StatusCode = 204;
                    
                    break;
                }
                default:
                {
                    context.Response.StatusCode = 405;
                    break;
                }
            }
        });
    }
}