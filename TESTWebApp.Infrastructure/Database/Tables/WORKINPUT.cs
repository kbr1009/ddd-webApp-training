using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESTWebApp.Infrastructure.Database.Tables
{
    public class WORKINPUT
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string WorkItem { get; set; }
        public int Status { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool IsDeleted { get; set; }
    }
}
