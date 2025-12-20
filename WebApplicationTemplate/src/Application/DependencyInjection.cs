using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Application.Common.Behaviours;
using Application.Common.Models;
using Application.TodoItems.Commands;
using Application.TodoItems.Models;
using Application.TodoItems.Queries;
using Domain.Common;
using FluentValidation;
using Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

/// <summary>
/// Provides extension methods for registering application services in the dependency injection container.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers application-specific services to the provided <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddScoped<IMediator, Mediator.Mediator>();
        services.AddScoped<IRequestHandler<GetTodoItemsQuery, BaseResponse<PaginatedEnumerable<TodoItemDto>>>,
            GetTodoItemsQueryHandler>();
        services.AddScoped<IRequestHandler<CreateTodoItemCommand, BaseResponse<TodoItemDto>>, CreateTodoItemCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateTodoItemComand, BaseResponse<TodoItemDto>>, UpdateTodoItemCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteTodoItemCommand, BaseResponse<object>>, DeleteTodoItemCommandHandler>();
        return services;
    }
}
