using TESTWebApp.Domain.Models.MajorWorkItems;

namespace TESTWebApp.Domain.Services.Shared
{
    /// <summary>
    /// 項目マスタで使用する抽象メソッド
    /// </summary>
    /// <typeparam name="T">項目アイテムオブジェクト</typeparam>
    public abstract class WorkItemsService<T> where T : class
    {
        /// <summary>
        /// 新規登録時項目名が使用されているか精査する
        /// </summary>
        /// <param name="workItem">項目アイテムドメインモデルオブジェクト</param>
        /// <returns>true; 使用済み false; 未使用</returns>
        public abstract bool IsDupulicatedWorkItem(T workItem);

        /// <summary>
        /// 編集時項目名が使用されているか精査する
        /// 項目名を編集していない場合、処理をスキップする
        /// </summary>
        /// <param name="updatedWorkItem"></param>
        /// <param name="beforeWorkItemName"></param>
        /// <returns></returns>
        public abstract bool IsDupulicatedWorkItemForEdit(T updatedWorkItem, WorkItemName beforeWorkItemName);

        /// <summary>
        /// 項目が作業記録としてすでに使用されている
        /// </summary>
        /// <param name="workItem">項目アイテムドメインモデルオブジェクト</param>
        /// <returns>true; 使用済 false; 未使用</returns>
        public abstract bool UsedAsRelatedData(T workItem);

        /// <summary>
        /// 不正な関連データ
        /// </summary>
        /// <param name="workItem">項目アイテムドメインモデルオブジェクト</param>
        /// <returns>true; 不正名関連データ false; 正式な関連データ</returns>
        public abstract bool RelatedIsMismatch(T workItem);
    }
}
