using Microsoft.Extensions.DependencyInjection;
using Todo.Logic.ApiV1.Status;
using Todo.Logic.ApiV1.Todos;
using Todo.Logic.ApiV1.Users;
using Todo.Logic.DomainObjects.Entities;
using Todo.Logic.DomainObjects.Repositories;
using Todo.Logic.Interfaces;
using Todo.Logic.Services;

namespace Todo.Logic;

public static class Dependencies
{
    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IHandler<GetStatusRequest,GetStatusResponse>, GetStatusHandler>();

        services.AddScoped<IHandler<CreateUserRequest, CreateUserResponse>, CreateUserHandler>();
        services.AddScoped<IHandler<GetUsersRequest, GetUsersResponse>, GetUsersHandler>();
        services.AddScoped<IHandler<DeleteUserRequest, DeleteUserResponse>, DeleteUserHandler>();

        services.AddScoped<IHandler<CreateTodoRequest, CreateTodoResponse>, CreateTodoHandler>();
        services.AddScoped<IHandler<GetTodosRequest, GetTodosResponse>, GetTodosHandler>();
        services.AddScoped<IHandler<DeleteTodoRequest, DeleteTodoResponse>, DeleteTodoHandler>();
        services.AddScoped<IHandler<UpdateTodoRequest, UpdateTodoResponse>, UpdateTodoHandler>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICleanupService, CleanupService>();
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IRepository<UserEntity>, MemoryRepository<UserEntity>>();
        services.AddSingleton<IRepository<TodoEntity>, MemoryRepository<TodoEntity>>();
        return services;
    }
}