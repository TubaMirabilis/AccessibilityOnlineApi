using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables(prefix: "AOAPI_");
var connectionString = builder.Configuration["MariaDB:ConnectionString"];
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", b =>
        b.WithOrigins("https://accessibility.online", "https://dev02.wa-intra.net")
             .AllowAnyMethod()
             .AllowAnyHeader());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IValidator<CreateUserDTO>, CreateUserValidator>();
builder.Services.AddScoped<IMailService, MailChimpService>();
builder.Services.AddScoped<UserRegistrationHandler>();
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