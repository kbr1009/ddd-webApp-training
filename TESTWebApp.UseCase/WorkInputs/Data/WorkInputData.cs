﻿using TESTWebApp.Domain.Models.WorkInputs;

namespace TESTWebApp.UseCase.WorkInputs.Data
{
    public class WorkInputData
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string WorkItem { get; set; }
        public WorkStatus? Status { get; private set; }
        public int? WorkStatus 
        { 
            get => (int)Status;
            set { this.Status = (WorkStatus)value; }
        }
        public DateTime TimeStamp { get; set; }
        public bool IsDeleted { get; set; }
    }
}