using PersonDirectory.Api.Extensions;
using PersonDirectory.Api.Filters;
using PersonDirectory.Application;
using PersonDirectory.Infrastructure;
using Serilog;
using System.Reflection;

namespace PersonDirectory.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((context, loggerConfig) =>
            loggerConfig.ReadFrom.Configuration(context.Configuration));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddApplicationDependencies();
            builder.Services.AddInfrastructureDependencies(builder.Configuration);
            builder.Services.AddScoped<ValidateModelStateActionFilter>();

            builder.Services.AddSwaggerGen(options =>
            {
                // Include XML comments for the main API project
                var apiXmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var apiXmlPath = Path.Combine(AppContext.BaseDirectory, apiXmlFile);
                options.IncludeXmlComments(apiXmlPath);

                // Include XML comments for the separate request models project
                var modelsAssembly = Assembly.Load("PersonDirectory.Application");
                var modelsXmlFile = $"{modelsAssembly.GetName().Name}.xml";
                var modelsXmlPath = Path.Combine(AppContext.BaseDirectory, modelsXmlFile);
                options.IncludeXmlComments(modelsXmlPath);
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                app.ApplyMigrations();

                app.SeedData();
            }

            app.UseHttpsRedirection();

            app.UseSerilogRequestLogging();

            app.UseCustomExceptionHandler();

            app.MapControllers();

            app.Run();
        }
    }
}
