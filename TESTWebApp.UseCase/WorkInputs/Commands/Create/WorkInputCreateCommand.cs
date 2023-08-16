using TESTWebApp.Domain.Models.Users;
using TESTWebApp.Domain.Models.WorkInputs;
using TESTWebApp.Domain.Services.WorkInputs;

namespace TESTWebApp.UseCase.WorkInputs.Commands.Create
{
    public sealed class WorkInputCreateCommand : IWorkInputCreateCommand
    {
        private readonly IWorkInputRepository _repository;

        public WorkInputCreateCommand(IWorkInputRepository repository)
        {
            _repository = repository;
        }

        public void Excecute(CreateWorkInputRequest request)
        {
            WorkInput workInput = WorkInput.CreateNew(
                userId: new UserId(request.UserId),
                workItem: request.WorkItem,
                workStatus: request.Status
                );
            _repository.RegistWorkInput(workInput);
        }
    }
}
