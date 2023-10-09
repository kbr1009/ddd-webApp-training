
namespace TESTWebApp.UseCase.Users.Commands.CreateSession
{
    public class CreateWebSessionRequest
    {
        public string UserId { get; }

        public CreateWebSessionRequest(string userId) 
        { 
            UserId = userId;
        }
    }
}
