namespace AccessibilityOnlineApi.Application.Repositories;

public interface IKeyRepository
{
    Task<bool> AuthenticateAsync(string apiKey);
}