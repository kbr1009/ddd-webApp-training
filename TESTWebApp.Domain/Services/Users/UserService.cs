using TESTWebApp.Domain.Models.Users;

namespace TESTWebApp.Domain.Services.Users
{
    public sealed class UserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool IsDupulicatedUser(User user)
        {
            var duplicatedUser = _userRepository.FindByUserName(user.UserName);
            return duplicatedUser != null;
        }
    }
}
