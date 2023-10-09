using TESTWebApp.UseCase.MiddleWorkItems.Queries;

namespace TESTWebApp.Models.MiddleWorkItem
{
    public class MiddleWorkItemEditViewModel
    {
        /// <summary>
        /// ログインユーザ名
        /// </summary>
        public string LoginUserName { get; set; }

        /// <summary>
        /// 中項目情報詳細
        /// </summary>
        public MiddleWorkItemDataResponse CurrentMiddleWorkItem { get; set; }

        /// <summary>
        /// 編集する中項目のID
        /// </summary>
        public string EditMiddleWorkItemId { get; set; } = string.Empty;

        /// <summary>
        /// 編集後の中項目名
        /// </summary>
        public string NewMiddleWorkItemName { get; set; } = string.Empty;

        /// <summary>
        /// 使用禁止状態
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 大項目（親）のID
        /// </summary>
        public string MajorWorkItemId { get; set; } = string.Empty;

        /// <summary>
        /// 大項目（親）の名前
        /// </summary>
        public string MajorWorkItemName { get; set; } = string.Empty;
    }
}
