using TESTWebApp.Domain.Models.Users;
using TESTWebApp.Domain.Models.WorkInputs;
using TESTWebApp.Domain.Models.MajorWorkItems;
using TESTWebApp.Domain.Models.MiddleWorkItems;
using TESTWebApp.Domain.Models.MinorWorkItems;
using TESTWebApp.Domain.Services.MajorWorkItems;
using TESTWebApp.Domain.Services.WorkInputs;
using TESTWebApp.Domain.Services.Users;

namespace TESTWebApp.UseCase.WorkInputs.Commands.Create
{
    public sealed class WorkInputCreateCommand : IWorkInputCreateCommand
    {
        private readonly IWorkInputRepository _workInputRepository;
        private readonly IMajorWorkItemRepository _majorWorkItemRepository;
        private readonly IUserRepository _userRepository;

        public WorkInputCreateCommand(
            IWorkInputRepository workInputRepository,
            IMajorWorkItemRepository majorWorkItemRepository,
            IUserRepository userRepository
            )
        {
            _workInputRepository = workInputRepository;
            _majorWorkItemRepository = majorWorkItemRepository;
            _userRepository = userRepository;
        }

        public void Excecute(CreateWorkInputRequest request)
        {
            UserId userId = new(request.UserId);
            WebSessionId webSessionId = new(request.WebSessionId);

            // ユーザ操作のセッション整合性チェック
            UserService userService = new(_userRepository);
            if (!userService.WebSessionIsMach(userId, webSessionId))
            {
                _userRepository.UpdateWebSession(userId, webSessionId);
                throw new ArgumentException("他の端末での操作があったため、実行ができません。操作しなおしてください。");
            }

            WorkInput workInput = WorkInput.CreateNew(
                userId: userId,
                majorWorkItemId: new MajorWorkItemId(request.WorkItemId),
                middleWorkItemId: new MiddleWorkItemId(request.MiddleWorkItemId),
                minorWorkItemId: new MinorWorkItemId(request.MinorWorkItemId),
                workStatus: request.Status);

            WorkInputService workInputService = new(_majorWorkItemRepository);
            if (!workInputService.ExistWorkItem(workInput))
                throw new ArgumentException("存在しない作業項目が登録されようとしています。");

            // TODO; 中項目と小項目のバリデーション

            _workInputRepository.RegistWorkInput(workInput);
        }
    }
}
