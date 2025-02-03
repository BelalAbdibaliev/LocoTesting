using System.Reflection;
using LocoTesting.Application;
using LocoTesting.Domain.Models;
using LocoTesting.Infrastructure;
using LocoTesting.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.AddSwaggerWithAuth();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        await Seed.SeedRolesAsync(scope.ServiceProvider);
    }
}

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseSwaggerWithAuth();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
