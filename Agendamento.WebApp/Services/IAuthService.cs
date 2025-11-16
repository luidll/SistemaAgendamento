using Agendamento.Core.Entities;
using System.Security.Claims;

namespace Agendamento.WebApp.Services
{
    public interface IAuthService
    {
        Task<Usuario?> LoginAsync(string login, string senha);
        Task LogoutAsync();
    }
}