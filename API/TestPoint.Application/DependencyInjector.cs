using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TestPoint.Application.Pipeline;
using TestPoint.Application.Users.Commands.CreateUser;

namespace TestPoint.Application;

public static class DependencyInjector
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssemblyContaining(typeof(CreateUserValidator));

        return services;
    }
}