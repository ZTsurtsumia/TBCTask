using PersonDirectory.Api.Extensions;
using PersonDirectory.Api.Filters;
using PersonDirectory.Application;
using PersonDirectory.Infrastructure;

namespace PersonDirectory.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddApplicationDependencies();
            builder.Services.AddInfrastructureDependencies(builder.Configuration);
            builder.Services.AddScoped<ValidateModelStateActionFilter>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                app.ApplyMigrations();
            }

            app.UseHttpsRedirection();

            app.UseCustomExceptionHandler();

            app.MapControllers();

            app.Run();
        }
    }
}
