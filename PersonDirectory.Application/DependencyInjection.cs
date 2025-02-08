using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PersonDirectory.Application.Dtos;
using PersonDirectory.Application.Validations;

namespace PersonDirectory.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            });

            services.AddScoped<IValidator<AddPersonRequest>, AddPersonRequestValidator>();

            return services;
        }
    }
}
