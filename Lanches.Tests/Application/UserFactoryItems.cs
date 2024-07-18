using Lanches.Core.Entities;
using Lanches.Infraestructure.Auth;
using Lanches.Infraestructure.Context;
using Lanches.Infraestructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Lanches.Tests.Application.Users
{
    public class UserFactorItems
    {
        private readonly Mock<Microsoft.Extensions.Configuration.IConfiguration> _configuration;
        private readonly Mock<UserManager<User>> _userManager;
        private readonly Mock<SignInManager<User>> _signInManager;
        private readonly Mock<RoleManager<IdentityRole>> _roleManager;

        public UserFactorItems(LanchesDbContext context)
        {
            _signInManager = new Mock<SignInManager<User>>();
            _userManager = new Mock<UserManager<User>>();
            _roleManager = new Mock<RoleManager<IdentityRole>>();
            _configuration = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
        }

        public async Task<IActionResult> CreateItem()
        {
            var result = await new AuthService(_userManager.Object, _signInManager.Object, _configuration.Object, _roleManager.Object)
                     .RegisterUser(new Login() { Email = "teste@teste.com", Password = "123123123Fc!" });

            return result;
        }
    }
}

