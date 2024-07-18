using Microsoft.AspNetCore.Identity;

namespace Lanches.Core.Entities
{
    public class User : IdentityUser
    {
        public User(string userName, string email, string phoneNumber)
        {
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public User(string userName, string email)
        {
            UserName = userName;
            Email = email;

        }

        public User()
        {
        }

        public List<Order> Orders { get; private set; }
    }
}
