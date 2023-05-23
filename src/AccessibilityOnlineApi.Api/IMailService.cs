public interface IMailService
{
    Task<HttpResponseMessage> AddContactAsync(CreateUserDTO dto);
}