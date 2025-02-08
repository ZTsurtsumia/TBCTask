using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonDirectory.Application.Data;
using PersonDirectory.Domain.Abstractions;
using PersonDirectory.Domain.Persons;
using PersonDirectory.Infrastructure.Data;
using PersonDirectory.Infrastructure.Repositories;

namespace PersonDirectory.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("Database") ??
                                      throw new ArgumentNullException(nameof(configuration));

            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(connectionString));

            services.AddScoped<IPersonRepository, PersonRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

            services.AddSingleton<ISqlConnectionFactory>(_ =>
                    new SqlConnectionFactory(connectionString));

            return services;
        }
    }
}
