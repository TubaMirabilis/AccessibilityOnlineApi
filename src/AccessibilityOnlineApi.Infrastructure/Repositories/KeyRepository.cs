using AccessibilityOnlineApi.Application.Repositories;
using AccessibilityOnlineApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace AccessibilityOnlineApi.Infrastructure.Repositories;

public class KeyRepository : IKeyRepository
{
    private ApplicationDbContext _ctx;

    public KeyRepository(ApplicationDbContext ctx)
    {
        _ctx = ctx;
    }
    public async Task<bool> AuthenticateAsync(string apiKey)
        => await _ctx.Keys!.AnyAsync(x => x.Secret.ToString() == apiKey);
}