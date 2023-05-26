using AccessibilityOnlineApi.Application.Dtos;
using AccessibilityOnlineApi.Domain;
using Riok.Mapperly.Abstractions;

namespace AccessibilityOnlineApi.Application.Mapping;

[Mapper]
public partial class UserMapper
{
    public User MapCreateUserDtoToUser(CreateUserDTO dto)
    {
        var user = CreateUserDtoToUser(dto);
        user.Id = Guid.NewGuid();
        user.CreatedAt = DateTime.UtcNow;
        return user;
    }

    private partial User CreateUserDtoToUser(CreateUserDTO dto);
}