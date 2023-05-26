using AccessibilityOnlineApi.Application;
using AccessibilityOnlineApi.Application.Dtos;
using AccessibilityOnlineApi.Application.Users;
using AccessibilityOnlineApi.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables(prefix: "AOAPI_");
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", b =>
        b.WithOrigins("https://accessibility.online", "https://dev02.wa-intra.net")
             .AllowAnyMethod()
             .AllowAnyHeader());
});
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.MapPost("/users", async (CreateUserDTO dto, UserRegistrationHandler registrationHandler, HttpRequest request) =>
{
    return await registrationHandler.HandleUserRegistration(dto, request);
})
.Produces(StatusCodes.Status201Created)
.Produces(StatusCodes.Status400BadRequest)
.Produces(StatusCodes.Status401Unauthorized)
.Produces(StatusCodes.Status409Conflict);
app.Run();