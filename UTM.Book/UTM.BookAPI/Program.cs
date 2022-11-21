using AutoMapper;
using Microsoft.Extensions.Options;
using UTM.Domain.Models;
using UTM.Repository.MongoDB;
using UTM.Repository.MongoDB.Settings;
using UTM.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var services = builder.Services;
var configuration = builder.Configuration;


// Auto Mapper Configurations
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});


IMapper mapper = mapperConfig.CreateMapper();
services.AddSingleton(mapper);

RegisterConfiguration(services);
RegisterRepositories(services);
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
    services.Configure<MongoDBOptions>(builder.Configuration.GetSection("mongodb"));
    services.AddSingleton<IMongoDBSettings, MongoDBSettings>();
}

void RegisterRepositories(IServiceCollection services)
{
    services.AddScoped<IMongoRepository<Book>, MongoRepository<Book>>();
}

void RegisterServices(IServiceCollection services)
{
    services.AddScoped<IBookService, BookService>();
}