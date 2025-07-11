using Auth.Domain.Services.Models;

namespace Auth.Repositories.Interfaces
{
    public interface IAuthRepositories
    {
        Task<UserModel> GetByUsernameAsync(string username);
    }
}
