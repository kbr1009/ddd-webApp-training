using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESTWebApp.UseCase.Users.Commands.Create
{
    public interface IUserCreateCommand
    {
        void Execute(CreateUserRequest request);
    }
}
