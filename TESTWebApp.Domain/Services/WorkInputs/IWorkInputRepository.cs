using TESTWebApp.Domain.Models.WorkInputs;

namespace TESTWebApp.Domain.Services.WorkInputs
{
    public interface IWorkInputRepository
    {
        void RegistWorkInput(WorkInput workInput);
    }
}
