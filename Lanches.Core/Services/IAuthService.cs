using Lanches.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Lanches.Core.Services
{
    public interface IAuthService
    {
        Task<IActionResult> Login(Login login);
        Task<IActionResult> RegisterUser(Login login);
    }
}
