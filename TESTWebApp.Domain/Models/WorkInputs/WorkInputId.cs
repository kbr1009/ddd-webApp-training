using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TESTWebApp.Domain.Models.Shared;

namespace TESTWebApp.Domain.Models.WorkInputs
{
    public class WorkInputId : SingleValueObject<string>
    {
        public WorkInputId(string val) : base(val) { }

        public static WorkInputId Generate()
        {
            return new WorkInputId(Guid.NewGuid().ToString("N"));
        }
    }
}
