using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TESTWebApp.UseCase.WorkInputs.Commands;

namespace TESTWebApp.UseCase.WorkInputs
{
    public interface IWorkInputUseCase
    {
        void Excecute(CreateWorkInputRequest createWorkInputRequest);
    }
}
