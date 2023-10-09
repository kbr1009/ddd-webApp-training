
namespace TESTWebApp.UseCase.Users.Queries
{
    public sealed class UserDataResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool IsDeleted { get; set; }
    }
}
