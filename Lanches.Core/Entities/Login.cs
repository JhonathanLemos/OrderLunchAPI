
using System.Diagnostics.CodeAnalysis;

namespace Lanches.Core.Entities
{
    [ExcludeFromCodeCoverage]
    public class Login
    {
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
