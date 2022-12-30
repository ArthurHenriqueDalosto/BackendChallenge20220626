using BackendChallenge;
using BackendChallenge.Services;
using ChallangeData.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<DataContext>(
            o => o.UseNpgsql(builder.Configuration.GetConnectionString("Default"))
            );

        // Add services to the container.
        builder.Services.AddMvc();
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddHostedService<Worker>();
                services.AddSingleton<IProductRepository, ProductRepository>();

                services.AddDbContext<DataContext>(
           o => o.UseNpgsql(builder.Configuration.GetConnectionString("Default"))
           );
            })
            .Build();

        await host.RunAsync();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}