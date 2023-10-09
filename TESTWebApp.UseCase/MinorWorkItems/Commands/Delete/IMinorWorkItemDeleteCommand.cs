
namespace TESTWebApp.UseCase.MinorWorkItems.Commands.Delete
{
    public interface IMinorWorkItemDeleteCommand
    {
        void Execute(DeleteMinorWorkItemRequest request);
    }
}
