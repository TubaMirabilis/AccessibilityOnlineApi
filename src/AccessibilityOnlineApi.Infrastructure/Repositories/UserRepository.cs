using System.Linq.Expressions;
using AccessibilityOnlineApi.Application.Repositories;
using AccessibilityOnlineApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace AccessibilityOnlineApi.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private ApplicationDbContext _ctx;

    public UserRepository(ApplicationDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task AddUser(User user)
    {
        _ctx.Users?.Add(user);
        await _ctx.SaveChangesAsync();
    }
    public async Task<bool> AnyAsync(Expression<Func<User, bool>> predicate)
        => await _ctx.Users!.AnyAsync(predicate);
}