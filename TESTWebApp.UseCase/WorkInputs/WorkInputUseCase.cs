using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TESTWebApp.Domain.Models.WorkInputs;
using TESTWebApp.UseCase.WorkInputs.Commands;

namespace TESTWebApp.UseCase.WorkInputs
{
    public class WorkInputUseCase : IWorkInputUseCase
    {
        private readonly IWorkInputRepository _workInputRepository;

        public WorkInputUseCase(IWorkInputRepository workInputRepository)
        {
            _workInputRepository = workInputRepository;
        }

        public void Excecute(CreateWorkInputRequest createWorkInputRequest)
        {
            WorkInput workInput = WorkInput.CreateNew(
                userId: createWorkInputRequest.UserId,
                workItem: createWorkInputRequest.WorkItem,
                workStatus: createWorkInputRequest.Status
                );
            _workInputRepository.RegistWorkInput(workInput);
        }
    }
}
