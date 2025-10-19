
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VinhMicroservices.OrderService.Data;
using VinhMicroservices.OrderService.Kafka;
using VinhMicroservices.ServiceDefaults;

namespace VinhMicroservices.OrderService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        var configuration = builder.Configuration;

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        builder.Services.AddDbContext<VinhMicroOrderDbContext>(options 
            => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<IKafkaProducer, KafkaProducer>();

        var app = builder.Build();

        app.MapDefaultEndpoints();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
