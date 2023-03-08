using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Week07_2_WebAPI.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Week07_2_WebAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Connection string 'Week07_2_WebAPIContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
