global using Microsoft.EntityFrameworkCore;
global using SuperHeroAPI.Data;
global using dotenv.net;

DotEnv.Load();

// Using .env file instead of appsettings.json
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

Console.WriteLine($"Connection String from Environment Variable: {connectionString}");
Console.WriteLine($"Environment Variable Value: {Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")}");


if (string.IsNullOrEmpty(connectionString))
{
    // Handle the case where the environment variable is not set
    throw new InvalidOperationException("CONNECTION_STRING environment variable is not set.");
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(connectionString);
});

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