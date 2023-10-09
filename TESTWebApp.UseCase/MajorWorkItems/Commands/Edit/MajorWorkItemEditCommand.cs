using TESTWebApp.Domain.Models.MajorWorkItems;
using TESTWebApp.Domain.Models.Users;
using TESTWebApp.Domain.Services.MajorWorkItems;

namespace TESTWebApp.UseCase.MajorWorkItems.Commands.Edit
{
    /// <summary>
    /// 作業項目（大項目）を編集するケース
    /// </summary>
    public class MajorWorkItemEditCommand : IMajorWorkItemEditCommand
    {
        /// <summary>
        /// リポジトリ
        /// </summary>
        private readonly IMajorWorkItemRepository _repository;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="repository"></param>
        public MajorWorkItemEditCommand(IMajorWorkItemRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 編集実行メソッド
        /// </summary>
        /// <param name="request">大項目リクエストオブジェクト</param>
        /// <exception cref="ArgumentException">ダメ―</exception>
        public void Execute(EditMajorWorkItemRequest request)
        {
            MajorWorkItem majorWorkItem = _repository.FindByWorkItemId(request.MajorWorkItemId)
                ?? throw new ArgumentException("指定された作業（大）項目は存在しないため編集できません。");

            MajorWorkItemService workItemService = new(_repository);
            WorkItemName newWorkItemName = new(request.MajorWorkItemName);
            if (workItemService.IsDupulicatedWorkItemForEdit(majorWorkItem, newWorkItemName))
                throw new ArgumentException($"{request.MajorWorkItemName} はすでに登録されています。");

            // データをアップデートする
            majorWorkItem.UpdateWorkItem(
                workItemName: newWorkItemName,
                isDelete: request.IsDeleted,
                modifiedBy: new UserId(request.ModifiedBy));

            _repository.ExecuteUpdateWorkItem(majorWorkItem);
        }
    }
}
