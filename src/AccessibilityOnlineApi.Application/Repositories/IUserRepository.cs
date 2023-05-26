using System.Linq.Expressions;
using AccessibilityOnlineApi.Domain;

namespace AccessibilityOnlineApi.Application.Repositories;

public interface IUserRepository
{
    Task AddUser(User user);
    Task<bool> AnyAsync(Expression<Func<User, bool>> predicate);
}