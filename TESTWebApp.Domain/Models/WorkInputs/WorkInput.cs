using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TESTWebApp.Domain.Models.Users;

namespace TESTWebApp.Domain.Models.WorkInputs
{
    public sealed class WorkInput
    {
        public WorkInputId Id { get; }
        public UserId UserId { get; private set; }
        public string WorkItem { get; private set; }
        public WorkStatus Status { get; private set; }
        public DateTime TimeStamp { get; private set; }
        public bool IsDeleted { get; private set; } = false;

        public WorkInput(
            WorkInputId id,
            UserId userId,
            string workItem,
            WorkStatus status,
            DateTime timeStamp,
            bool isDeleted)
        {
            this.Id = id;
            this.UserId = userId;
            this.WorkItem = workItem;
            this.Status = status;
            this.TimeStamp = timeStamp;
            this.IsDeleted = isDeleted;
        }

        public static WorkInput CreateNew(
            UserId userId,
            string workItem,
            WorkStatus? workStatus
            )
        {
            var status = WorkStatus.Start;
            if (workStatus != null)
            {
                switch (workStatus)
                {
                    case WorkStatus.Start:
                        status = WorkStatus.End;
                        break;
                    case WorkStatus.End:
                        status = WorkStatus.Start;
                        break;
                }
            }
            return new WorkInput(
                id: WorkInputId.Generate(),
                userId: userId ??
                throw new ArgumentException("error!：ユーザIDが指定されていません。"),
                workItem: workItem ??
                throw new ArgumentException("error!：作業項目が指定されていません。"),
                status: status,
                timeStamp: DateTime.Now,
                false);
        }

        public void DelHistory()
        {
            if (this.IsDeleted)
            {
                return;
            }
            this.IsDeleted = true;
        }
    }
}
