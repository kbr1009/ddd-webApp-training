namespace TESTWebApp.Domain.Models.WorkInputs
{
    public interface IWorkInputRepository
    {
        void RegistWorkInput(WorkInput workInput);
        IEnumerable<WorkInput> FindAllToModel(DateTime date);
    }
}
