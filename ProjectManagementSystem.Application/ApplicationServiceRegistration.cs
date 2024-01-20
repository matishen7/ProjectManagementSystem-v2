using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagementSystem.Application.Contracts.Features.User.Commands;
using ProjectManagementSystem.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ApplicationServiceRegistration));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly())); 
            services.AddTransient<IValidator<UpdateUserCommand>, UpdateUserCommandValidator>();

            return services;
        }
    }
}
