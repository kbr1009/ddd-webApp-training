using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TESTWebApp.Domain.Models.Shared;

namespace TESTWebApp.Domain.Models.WorkItems
{
    public class WorkItemId : SingleValueObject<string>
    {
        public WorkItemId(string value): base(value) { }

        public static WorkItemId Generate()
        {
            return new WorkItemId(Guid.NewGuid().ToString("N"));
        }
    }
}
