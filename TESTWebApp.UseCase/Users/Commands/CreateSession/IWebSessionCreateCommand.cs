
namespace TESTWebApp.UseCase.Users.Commands.CreateSession
{
    public interface IWebSessionCreateCommand
    {
        string Execute(CreateWebSessionRequest createWebSessionRequest);
    }
}
