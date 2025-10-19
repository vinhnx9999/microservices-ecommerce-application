
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VinhMicroservices.ProductService.Data;
using VinhMicroservices.ProductService.Kafka;
using VinhMicroservices.ServiceDefaults;

namespace VinhMicroservices.ProductService;

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

        builder.Services.AddDbContext<VinhMicroProductDbContext>(options 
            => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddHostedService<KafkaConsumer>();

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
