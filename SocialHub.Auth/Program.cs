using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SocialHub.Auth.Persistance;

namespace SocialHub.Auth;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        #region Serilog

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Seq("http://localhost:5341")
            .CreateLogger();
        
        builder.Host.UseSerilog();

        #endregion

        #region DI

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlDB"), settings =>
                settings.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                    .EnableRetryOnFailure()
                    .CommandTimeout(10)
                    .MigrationsHistoryTable("__EFMigrationsHistory", "Auth")));
        
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterAssemblyModules(typeof(Program).Assembly);

            containerBuilder.RegisterType<AppDbContext>()
                .As<DbContext>()
                .InstancePerLifetimeScope();
        });
        
        builder.Services.AddAutofac();

        #endregion

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "SocialHub", Version = "v1" });
        });        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        
        app.MapControllers();

        Log.Information("Запуск приложения");
        
        app.Run();
    }
}