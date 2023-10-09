
namespace TESTWebApp.UseCase.Users.Commands.Create
{
    public sealed class CreateUserRequest
    {
        public string UserName { get; }
        public string CreatedBy { get; }
        
        public CreateUserRequest(
            string userName, string createdBy)
        {
            UserName = userName;
            CreatedBy = createdBy;
        }
    }
}
