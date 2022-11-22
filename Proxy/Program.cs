using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;


services.AddOcelot()
        .AddCacheManager(x =>
        {
            x.WithDictionaryHandle();
        });


var app = builder.Build();
app.MapGet("/", () => "Smart Proxy");

app.UseOcelot().Wait();

builder.Configuration.AddJsonFile("ocelot.json");
builder.Logging.AddConsole();

app.Run();
