using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

public class UserRegistrationHandler
{
    private readonly ApplicationDbContext _ctx;
    private readonly IValidator<CreateUserDTO> _validator;
    private readonly IMailService _mailService;

    public UserRegistrationHandler(ApplicationDbContext ctx, IValidator<CreateUserDTO> validator, IMailService mailService)
    {
        _ctx = ctx;
        _validator = validator;
        _mailService = mailService;
    }

    public async Task<IResult> HandleUserRegistration(CreateUserDTO dto, HttpRequest request)
    {
        if (!IsApiKeyValid(request))
        {
            return Results.Unauthorized();
        }
        if (EmailAlreadyRegistered(dto.Email))
        {
            return Results.Conflict("Email address is already registered.");
        }
        var validationResult = await ValidateDto(dto);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }
        try
        {
            var addContactResponse = await AddContactToMailService(dto);
            if (!addContactResponse.IsSuccessStatusCode)
            {
                var problemDetails = CreateProblemDetails("MailChimp could not add the new contact");
                return Results.Problem(problemDetails);
            }
        }
        catch (Exception ex)
        {
            var problemDetails = CreateProblemDetails(ex.Message);
            return Results.Problem(problemDetails);
        }
        return await SaveUser(dto);
    }
    private bool IsApiKeyValid(HttpRequest request)
    {
        string? apiKey = request.Headers["apikey"].FirstOrDefault();
        if (string.IsNullOrEmpty(apiKey))
        {
            return false;
        }
        return _ctx.Keys?.Any(x => x.Secret.ToString() == apiKey) ?? false;
    }
    private bool EmailAlreadyRegistered(string email)
        => _ctx.Users is not null && _ctx.Users.Any(x => x.Email == email);

    private async Task<ValidationResult> ValidateDto(CreateUserDTO dto)
        => await _validator.ValidateAsync(dto);

    private async Task<HttpResponseMessage> AddContactToMailService(CreateUserDTO dto)
        => await _mailService.AddContactAsync(dto);

    private ProblemDetails CreateProblemDetails(string errorMessage)
    {
        return new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = errorMessage,
            Detail = errorMessage
        };
    }
    private async Task<IResult> SaveUser(CreateUserDTO dto)
    {
        var user = new UserMapper().MapCreateUserDtoToUser(dto);
        _ctx.Users!.Add(user);
        await _ctx.SaveChangesAsync();
        return Results.Created($"/users/{user.Id}", user);
    }
}