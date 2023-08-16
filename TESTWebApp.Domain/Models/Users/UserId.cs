using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TESTWebApp.Domain.Models.Shared;

namespace TESTWebApp.Domain.Models.Users
{
    public sealed class UserId : SingleValueObject<string>
    {
        public UserId(string value) : base(value) { }

        public static UserId Generate()
        {
            return new UserId(Guid.NewGuid().ToString("N"));
        }
    }
}
