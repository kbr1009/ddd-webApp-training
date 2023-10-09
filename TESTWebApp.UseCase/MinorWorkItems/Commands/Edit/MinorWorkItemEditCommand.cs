using TESTWebApp.Domain.Models.Users;
using TESTWebApp.Domain.Models.MajorWorkItems;
using TESTWebApp.Domain.Models.MinorWorkItems;
using TESTWebApp.Domain.Services.MinorWorkItems;

namespace TESTWebApp.UseCase.MinorWorkItems.Commands.Edit
{
    /// <summary>
    /// 小項目編集ケース
    /// </summary>
    public class MinorWorkItemEditCommand : IMinorWorkItemEditCommand
    {
        /// <summary>
        /// 小項目リポジトリ
        /// </summary>
        private readonly IMinorWorkItemRepository _repository;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="repository">小項目リポジトリ</param>
        public MinorWorkItemEditCommand(IMinorWorkItemRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 小項目編集実行メソッド
        /// </summary>
        /// <param name="request">リクエスト</param>
        /// <exception cref="ArgumentException"></exception>
        public void Execute(EditMinorWorkItemRequest request)
        {
            MinorWorkItem minorWorkItem = _repository.FindById(request.MinorWorkItemId)
                ?? throw new ArgumentException("指定された作業（小）項目は存在しないため編集できません。");

            MinorWorkItemService minorWorkItemService = new(_repository);
            WorkItemName newWorkItemName = new(request.MinorWorkItemName);
            if (minorWorkItemService.IsDupulicatedWorkItemForEdit(minorWorkItem, newWorkItemName))
                throw new ArgumentException($"'{request.MinorWorkItemName}' はすでに登録されています。");

            minorWorkItem.UpdateWorkItem(
                minorWorkItemName: newWorkItemName,
                isDeleted: request.IsDeleted,
                modifiedBy: new UserId(request.ModifiedBy));

            _repository.ExecuteUpdateWorkItem(minorWorkItem);
        }
    }
}
