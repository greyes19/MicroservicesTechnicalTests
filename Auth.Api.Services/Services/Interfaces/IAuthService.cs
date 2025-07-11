using Auth.Api.Services.Model;

namespace Auth.Api.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginDto> AuthenticateAsync(string username, string password);
    }
}
