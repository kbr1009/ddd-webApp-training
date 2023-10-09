using TESTWebApp.Domain.Models.WorkInputs;
using Microsoft.AspNetCore.Mvc.Rendering;
using TESTWebApp.UseCase.Users.Queries;
using TESTWebApp.UseCase.WorkInputs.Queries;
using TESTWebApp.UseCase.MajorWorkItems.Queries;

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
                    return $"{this.LatestWorkData.MajorWorkItemName}を作業中";
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

        public IEnumerable<MajorWorkItemDataResponse> MajorWorkItems { get; set; } 
            = new List<MajorWorkItemDataResponse>();

        public IEnumerable<SelectListItem> SelectMajorWorkItems
        {
            get
            {
                foreach (var i in this.MajorWorkItems)
                {
                    yield return new SelectListItem
                    {
                        Text = i.MajorWorkItemName,
                        Value = i.MajorWorkItemId
                    };
                }
            }
        }

        /// <summary>
        /// Tmpフォームから送信される大項目ID
        /// </summary>
        public string MajorWorkItemId { get; set; }

        /// <summary>
        /// Tmpフォームから送信される中項目ID
        /// </summary>
        public string MiddleWorkItemId { get; set; }

        /// <summary>
        /// Tmpフォームから送信される小項目ID
        /// </summary>
        public string MinorWorkItemId { get; set; }

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
