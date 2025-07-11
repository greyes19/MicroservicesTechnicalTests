using Auth.Domain.Services.Models;
using Auth.Infraestructure;
using Auth.Repositories.Interfaces;
using Core.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Auth.Repositories
{
    public class AuthRepositories : IAuthRepositories
    {
        private readonly ApplicationDbContext _context;

        public AuthRepositories(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException();
        }

        public async Task<UserModel> GetByUsernameAsync(string username)
        {
            username.ValidateArgumentOrThrow(nameof(username));

            return await _context.Users
                .SingleOrDefaultAsync(u => u.Username == username);
        }
    }
}
