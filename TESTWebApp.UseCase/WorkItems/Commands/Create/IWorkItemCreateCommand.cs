
namespace TESTWebApp.UseCase.WorkItems.Commands.Create
{
    public interface IWorkItemCreateCommand
    {
        void Execute(CreateWorkItemRequest request);
    }
}
