using BackendChallenge;
using BackendChallenge.Repository;
using BackendChallenge.Services;
using ChallangeData.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<DataBaseContext>(
            o => o.UseNpgsql(builder.Configuration.GetConnectionString("Default"))
            );

        // Add services to the container.
        builder.Services.AddMvc();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddTransient<IProductService , ProductService>();
        builder.Services.AddTransient<IProductRepository, ProductRepository>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        await app.StartAsync();
        IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddHostedService<Worker>();
                services.AddSingleton<IProductRepository, ProductRepository>();
                services.AddDbContext<DataBaseContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("Default")));
            }).Build();
        await host.RunAsync();

    }
}