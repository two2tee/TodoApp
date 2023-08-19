using Microsoft.Extensions.DependencyInjection;
using Todo.Logic.ApiV1.Status;
using Todo.Logic.ApiV1.Users;
using Todo.Logic.DomainObjects.Entities;
using Todo.Logic.DomainObjects.Repositories;
using Todo.Logic.Interfaces;

namespace Todo.Logic;

public static class Dependencies
{
    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IHandler<GetStatusRequest,GetStatusResponse>, GetStatusHandler>();
        services.AddScoped<IHandler<CreateUserRequest, CreateUserResponse>, CreateUserHandler>();
        services.AddScoped<IHandler<GetUsersRequest, GetUsersResponse>, GetUsersHandler>();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IRepository<UserEntity>, MemoryRepository<UserEntity>>();
        services.AddSingleton<IRepository<TodoEntity>, MemoryRepository<TodoEntity>>();
        return services;
    }
}