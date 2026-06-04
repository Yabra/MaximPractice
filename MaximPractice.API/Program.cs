using MaximPractice.Algorithms;
using MaximPractice.API.Middlewares;
using MaximPractice.API.Services;
using MaximPractice.Data;
using MaximPractice.Interfaces;

namespace MaximPractice.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var mapSection = builder.Configuration.GetSection("MapSettings");
        var width = mapSection.GetValue<int>("Width");
        var height = mapSection.GetValue<int>("Height");

        var settingsSection = builder.Configuration.GetSection("Settings");
        var parallelLimit = settingsSection.GetValue<int>("ParallelLimit");

        ParallelLimitMiddleware.Limit = parallelLimit;

        builder.Services.AddSingleton<Map>(_ => new Map(width, height));
        builder.Services.AddSingleton<DriverService>();

        builder.Services.AddHttpClient<RandomService>();

        builder.Services.AddSingleton<IDriverSearchAlgorithm, TopKSearch>();
        builder.Services.AddScoped<OrderService>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseMiddleware<ParallelLimitMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
