using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESTWebApp.UseCase.Users.Commands.Create
{
    public sealed class CreateUserRequest
    {
        public string UserName { get; }
        //public string EmailAddress { get; }
        public string CreatedBy { get; }
        public CreateUserRequest(string userName, string createdBy)
        {
            UserName = userName;
            CreatedBy = createdBy;
        }
    }
}
