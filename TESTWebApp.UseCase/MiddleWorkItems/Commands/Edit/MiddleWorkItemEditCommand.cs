using TESTWebApp.Domain.Models.MiddleWorkItems;
using TESTWebApp.Domain.Services.MiddleWorkItems;
using TESTWebApp.Domain.Models.MajorWorkItems;
using TESTWebApp.Domain.Models.Users;

namespace TESTWebApp.UseCase.MiddleWorkItems.Commands.Edit
{
    /// <summary>
    /// 中項目編集ユースケース
    /// </summary>
    public class MiddleWorkItemEditCommand : IMiddleWorkItemEditCommand
    {
        /// <summary>
        /// リポジトリ
        /// </summary>
        private readonly IMiddleWorkItemRepository _repository;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="repository">リポジトリ</param>
        public MiddleWorkItemEditCommand(IMiddleWorkItemRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 実行メソッド
        /// </summary>
        /// <param name="request">中項目リクエストオブジェクト</param>
        /// <exception cref="ArgumentException"></exception>
        public void Execute(EditMiddleWorkItemRequest request)
        {
            MiddleWorkItem middleWorkItem = _repository.FindById(request.MiddleWorkItemId)
                ?? throw new ArgumentException("指定された作業（中）項目は存在しないため編集できません。");

            MiddleWorkItemService middleWorkItemService = new(_repository);
            WorkItemName newWorkItemName = new(request.MiddleWorkItemName);
            if (middleWorkItemService.IsDupulicatedWorkItemForEdit(middleWorkItem, newWorkItemName))
                throw new ArgumentException($"'{request.MiddleWorkItemName}' はすでに登録されています。");

            middleWorkItem.UpdateWorkItem(
                workItemName: newWorkItemName,
                isDeleted: request.IsDeleted,
                modifiedBy: new UserId(request.ModifiedBy));

            _repository.ExecuteUpdateWorkItem(middleWorkItem);
        }
    }
}
