using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TESTWebApp.Domain.Models.Users;

namespace TESTWebApp.Domain.Services.Users
{
    public interface IUserRepository
    {
        User FindByUserName(string userName);

        void SaveNewUser(User newUser);
    }
}
