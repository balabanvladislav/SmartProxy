using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var services = builder.Services;

services.AddOcelot();

app.MapGet("/", () => "SmartProxy");
app.UseOcelot().Wait();
app.Run();
