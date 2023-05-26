using AccessibilityOnlineApi.Application.Dtos;

namespace AccessibilityOnlineApi.Application.Interfaces;

public interface IMailService
{
    Task<HttpResponseMessage> AddContactAsync(CreateUserDTO dto);
}