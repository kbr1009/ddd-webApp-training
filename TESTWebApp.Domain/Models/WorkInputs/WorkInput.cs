using TESTWebApp.Domain.Models.MajorWorkItems;
using TESTWebApp.Domain.Models.MiddleWorkItems;
using TESTWebApp.Domain.Models.MinorWorkItems;
using TESTWebApp.Domain.Models.Users;

namespace TESTWebApp.Domain.Models.WorkInputs
{
    public sealed class WorkInput
    {
        public WorkInputId Id { get; }
        public UserId UserId { get; private set; }
        public MajorWorkItemId MajorWorkItemId { get; private set; }
        public MiddleWorkItemId MiddleWorkItemId { get; private set; }
        public MinorWorkItemId MinorWorkItemId { get; private set; }
        public WorkStatus Status { get; private set; }
        public DateTime TimeStamp { get; private set; }

        public WorkInput(
            WorkInputId id,
            UserId userId,
            MajorWorkItemId majorWorkItemId,
            MiddleWorkItemId middleWorkItemId,
            MinorWorkItemId minorWorkItemId,
            WorkStatus status,
            DateTime timeStamp)
        {
            this.Id = id;
            this.UserId = userId;
            this.MajorWorkItemId = majorWorkItemId;
            this.MiddleWorkItemId = middleWorkItemId;
            this.MinorWorkItemId = minorWorkItemId;
            this.Status = status;
            this.TimeStamp = timeStamp;
        }

        public static WorkInput CreateNew(
            UserId userId,
            MajorWorkItemId majorWorkItemId,
            MiddleWorkItemId middleWorkItemId,
            MinorWorkItemId minorWorkItemId,
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

            if (string.IsNullOrWhiteSpace(majorWorkItemId.Value))
                throw new ArgumentException("大作業項目は必須選択項目です。");

            return new WorkInput(
                id: WorkInputId.Generate(),
                userId: userId,
                majorWorkItemId: majorWorkItemId,
                middleWorkItemId: middleWorkItemId,
                minorWorkItemId: minorWorkItemId,
                status: status,
                timeStamp: DateTime.Now);
        }
    }
}
