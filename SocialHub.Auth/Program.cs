using Serilog;

namespace SocialHub.Auth;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()  // Устанавливаем минимальный уровень логирования
            .WriteTo.Seq("http://localhost:5341")  // Указываем адрес сервера Seq
            .CreateLogger();
        
        builder.Host.UseSerilog();
        
        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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