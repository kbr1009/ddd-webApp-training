using TESTWebApp.UseCase.MinorWorkItems.Queries;

namespace TESTWebApp.Models.MinorWorkItem
{
    public class MinorWorkItemEditViewModel
    {
        /// <summary>
        /// ログインユーザ名
        /// </summary>
        public string LoginUserName { get; set; }

        /// <summary>
        /// 中項目情報詳細
        /// </summary>
        public MinorWorkItemDataResponse CurrentMinorWorkItem { get; set; }

        /// <summary>
        /// 編集する小項目のID
        /// </summary>
        public string EditMinorWorkItemId { get; set; } = string.Empty;

        /// <summary>
        /// 編集後の小項目名
        /// </summary>
        public string NewMinorWorkItemName { get; set; } = string.Empty;

        /// <summary>
        /// 編集後の使用禁止状態
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 大項目（大親）のID
        /// </summary>
        public string MajorWorkItemId { get; set; } = string.Empty;

        /// <summary>
        /// 大項目（大親）の名前
        /// </summary>
        public string MajorWorkItemName { get; set; } = string.Empty;

        /// <summary>
        /// 中項目（親）のID
        /// </summary>
        public string MiddleWorkItemId { get; set; } = string.Empty;

        /// <summary>
        /// 中項目（大親）の名前
        /// </summary>
        public string MiddleWorkItemName { get; set; } = string.Empty;
    }
}
