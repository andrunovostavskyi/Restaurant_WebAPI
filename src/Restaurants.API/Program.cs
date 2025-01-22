using Microsoft.OpenApi.Models;
using Restaurants.API.Extencions;
using Restaurants.API.Middlewares;
using Restaurants.Application.Extensions;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructures.Extensions;
using Restaurants.Infrastructures.Seeders;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddExtencions();

builder.Services.AddServicesExtencions();
builder.Services.AddInfrastructures(builder.Configuration);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeed>();
await seeder.Seed();

// Configure the HTTP request pipeline.
app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddle>();
app.UseMiddleware<RequestTimeHandlingMiddle>();

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapGroup("/api/identity")
    .WithTags("Identity")
    .MapIdentityApi<User>();


app.MapControllers();

app.Run();

public partial class Program();


