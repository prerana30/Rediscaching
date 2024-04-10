using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(provider =>
{
    var configuration = ConfigurationOptions.Parse("localhost:6379"); // Redis server address with port
    return ConnectionMultiplexer.Connect(configuration);
});

builder.Services.AddSingleton<IRedisCacheService, RedisCacheService>();

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

app.Run();
