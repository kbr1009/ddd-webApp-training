using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TESTWebApp.UseCase.WorkInputs.Queries;
using TESTWebApp.Models;
using TESTWebApp.UseCase.WorkInputs;
using TESTWebApp.UseCase.WorkInputs.Commands;

namespace TESTWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWorkInputDataQuery _workInputDataQuery;
        private readonly IWorkInputUseCase _workInputUseCase;

        public HomeController(
            ILogger<HomeController> logger,
            IWorkInputDataQuery workInputDataQuery,
            IWorkInputUseCase workInputUseCase
            )
        {
            _logger = logger;
            _workInputDataQuery = workInputDataQuery;
            _workInputUseCase = workInputUseCase;
        }

        public IActionResult Index()
        {
            var viewModel = new WorkInputViewModel();
            viewModel.WorkInputDatas = _workInputDataQuery.FindAllWorkInputData();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ExcecuteWork(WorkInputViewModel workInputViewModel)
        {
            var createWorkInputRequest = new CreateWorkInputRequest(
                workInputViewModel.UserId,
                workInputViewModel.InputWorkItem,
                workInputViewModel.InputWorkStatus
                );
            _workInputUseCase.Excecute(createWorkInputRequest);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(
                new ErrorViewModel 
                { 
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier 
                });
        }
    }
}