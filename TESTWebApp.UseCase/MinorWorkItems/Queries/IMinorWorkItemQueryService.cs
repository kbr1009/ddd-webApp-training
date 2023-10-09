
namespace TESTWebApp.UseCase.MinorWorkItems.Queries
{
    public interface IMinorWorkItemQueryService
    {
        /// <summary>
        /// 中項目IDをキーに小項目一覧を取得する
        /// </summary>
        /// <param name="middleWorkItemId">親項目ID</param>
        /// <returns>小項目クエリレスポンスデータコレクション</returns>
        IEnumerable<MinorWorkItemDataResponse> GetAllMinorWorkItem(string middleWorkItemId);

        /// <summary>
        /// 小項目IDの詳細情報を取得する
        /// </summary>
        /// <param name="minorWorkItemId">小項目ID</param>
        /// <returns>小項目クエリレスポンスデータ</returns>
        MinorWorkItemDataResponse GetMinorWorkItem(string minorWorkItemId);
    }
}
