using UTM.SyncNode.Services;
using UTM.SyncNode.Settings;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

RegisterConfiguration(services);
RegisterServices(services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();


void RegisterConfiguration(IServiceCollection services)
{
    services.Configure<BookAPIOptions>(builder.Configuration.GetSection("BookAPISettings"));
    services.AddSingleton<IBookAPISettings, BookAPISettings>();
}

void RegisterServices(IServiceCollection services)
{
    services.AddSingleton<SyncWorkJobService>();
    services.AddHostedService(provider => provider.GetService<SyncWorkJobService>());
}