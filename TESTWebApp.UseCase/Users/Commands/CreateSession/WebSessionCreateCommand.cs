using TESTWebApp.Domain.Models.Users;
using TESTWebApp.Domain.Services.Users;

namespace TESTWebApp.UseCase.Users.Commands.CreateSession
{
    public class WebSessionCreateCommand : IWebSessionCreateCommand
    {
        private readonly IUserRepository _userRepository;

        public WebSessionCreateCommand(IUserRepository userRepository) 
        { 
            _userRepository = userRepository;
        }

        public string Execute(CreateWebSessionRequest request)
        {
            var userData = _userRepository.FindByUserId(request.UserId)
                ?? throw new ArgumentException(null, nameof(request));

            WebSessionId webSessionId = new();
            userData.UpdateUserLoginSession(webSessionId);

            _userRepository.UpdateWebSession(userData);

            return webSessionId.Value;
        }
    }
}
