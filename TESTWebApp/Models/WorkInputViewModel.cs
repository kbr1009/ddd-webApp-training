using TESTWebApp.Domain.Models.WorkInputs;
using Microsoft.AspNetCore.Mvc.Rendering;
using TESTWebApp.UseCase.Users.Queries;
using TESTWebApp.UseCase.WorkInputs.Queries;

namespace TESTWebApp.Models
{
    public class WorkInputViewModel
    {
        public UserDataResponse userData { get; set; }

        public bool Work 
        {
            get
            {
                if (string.IsNullOrEmpty(WodrkStatusMsg))
                {
                    return false;
                }
                return true;
            }
        }

        public string? WodrkStatusMsg
        {
            get
            {
                if (this.LatestWorkData is null) return null;
                if (this.LatestWorkData.Status == (int)WorkStatus.Start)
                {
                    return $"{this.LatestWorkData.WorkItem}を作業中";
                }
                return null;
            }
        }

        public WorkInputDataResponse? LatestWorkData
        {
            get
            {
                if (this.WorkInputDatas is null) return null;
                if (!this.WorkInputDatas.Any()) return null;

                var data = this.WorkInputDatas
                    .OrderBy(x => Math.Abs((DateTime.Now - x.TimeStamp).TotalSeconds))
                    .FirstOrDefault();

                if (data == null) return null;

                return data;
            }
        }

        public IEnumerable<WorkInputDataResponse> WorkInputDatas { get; set; }

        public IEnumerable<SelectListItem> WorkItemList { get; } = new List<SelectListItem>
        {
            new SelectListItem { Text = "清掃", Value = "清掃" },
            new SelectListItem { Text = "売り出し", Value = "売り出し" },
            new SelectListItem { Text = "えさやり", Value = "えさやり" }
        };

        public string InputWorkItem { get; set; }
        public WorkStatus? InputWorkStatus { get; set; }
        public WorkStatus? BeforeWorkStatus
        {
            get 
            {
                if (LatestWorkData is null)
                {
                    return null;
                }
                return this.LatestWorkData.Status; 
            }
        }
    }
}
