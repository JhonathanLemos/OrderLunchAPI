using Lanches.Core.Entities;

namespace Lanches.Application.Dtos.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel(string id, string userName, string email)
        {
            Id = id;
            UserName = userName;
            Email = email;
        }

        public static UserViewModel FromEntity(User user)
        {
            return new UserViewModel(user.Id, user.UserName, user.Email);
        }

        public string Id { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
    }
}
