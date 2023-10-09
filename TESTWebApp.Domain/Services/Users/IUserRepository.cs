using TESTWebApp.Domain.Models.Users;

namespace TESTWebApp.Domain.Services.Users
{
    public interface IUserRepository
    {
        User FindByUserName(string userName);
        User FindByUserId(string userId);
        void SaveNewUser(User newUser);
        void UpdateWebSession(User user);
        void UpdateWebSession(UserId user, WebSessionId webSessionId);
    }
}
