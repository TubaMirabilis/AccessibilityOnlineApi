using AccessibilityOnlineApi.Application.Dtos;
using AccessibilityOnlineApi.Application.Interfaces;
using AccessibilityOnlineApi.Application.Mapping;
using AccessibilityOnlineApi.Application.Repositories;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccessibilityOnlineApi.Application.Users;

public class UserRegistrationHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IKeyRepository _keyRepository;
    private readonly IValidator<CreateUserDTO> _validator;
    private readonly IMailService _mailService;

    public UserRegistrationHandler(IUserRepository userRepository, IKeyRepository keyRepository, IValidator<CreateUserDTO> validator, IMailService mailService)
    {
        _userRepository = userRepository;
        _keyRepository = keyRepository;
        _validator = validator;
        _mailService = mailService;
    }

    public async Task<IResult> HandleUserRegistration(CreateUserDTO dto, HttpRequest request)
    {
        if (!await IsApiKeyValid(request))
        {
            return Results.Unauthorized();
        }
        if (await EmailAlreadyRegistered(dto.Email))
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
    private async Task<bool> IsApiKeyValid(HttpRequest request)
    {
        string? apiKey = request.Headers["apikey"].FirstOrDefault();
        if (string.IsNullOrEmpty(apiKey))
        {
            return false;
        }
        return await _keyRepository.AuthenticateAsync(apiKey);
    }
    private async Task<bool> EmailAlreadyRegistered(string email)
        => await _userRepository.AnyAsync(x => x.Email == email);
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
        await _userRepository.AddUser(user);
        return Results.Created($"/users/{user.Id}", user);
    }
}