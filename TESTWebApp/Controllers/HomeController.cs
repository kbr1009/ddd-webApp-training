using Microsoft.AspNetCore.Mvc;
using TESTWebApp.UseCase.WorkInputs.Queries;
using TESTWebApp.Models;
using TESTWebApp.UseCase.Users.Queries;
using TESTWebApp.Services.Cookie.Models;
using TESTWebApp.UseCase.WorkInputs.Commands.Create;

namespace TESTWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWorkInputDataQuery _workInputDataQuery;
        private readonly IWorkInputCreateCommand _workInputCreateCommand;
        private readonly IUserDataQuery _userDataQuery;

        public HomeController(
            IWorkInputDataQuery workInputDataQuery,
            IWorkInputCreateCommand workInputCreateCommand,
            IUserDataQuery userDataQuery
            )
        {
            _workInputDataQuery = workInputDataQuery;
            _workInputCreateCommand = workInputCreateCommand;
            _userDataQuery = userDataQuery;
        }

        public IActionResult Index()
        {
            // ユーザのログイン情報があるか
            string cookie = HttpContext.Request.Cookies[WebAuthCookieKey.ATTENDANCE_ID];

            // セッションが無ければログイン画面にリダイレクトする
            if (string.IsNullOrWhiteSpace(cookie))
            {
                return RedirectToAction("index", "UserLogin");
            }

            UserDataResponse searchUserData = _userDataQuery.FindUserById(cookie);
            if (searchUserData is null)
            {
                // クッキーの削除
                HttpContext.Response.Cookies.Delete(WebAuthCookieKey.ATTENDANCE_ID);
                // ログイン画面へのリダイレクト
                return RedirectToAction("index", "UserLogin");
            }

            WorkInputViewModel viewModel = new WorkInputViewModel();
            viewModel.WorkInputDatas = _workInputDataQuery.FindAllWorkInputData(cookie);
            viewModel.userData = searchUserData;
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ExcecuteWork(WorkInputViewModel workInputViewModel)
        {
            string cookie = HttpContext.Request.Cookies[WebAuthCookieKey.ATTENDANCE_ID];
            if (string.IsNullOrWhiteSpace(cookie))
            {
                return RedirectToAction("index", "UserLogin");
            }

            var createWorkInputRequest = new CreateWorkInputRequest(
                cookie,
                workInputViewModel.InputWorkItem,
                workInputViewModel.InputWorkStatus
                );
            _workInputCreateCommand.Excecute(createWorkInputRequest);

            return RedirectToAction("Index");
        }

        public IActionResult Logout()
        {
            // クッキーの削除
            HttpContext.Response.Cookies.Delete(WebAuthCookieKey.ATTENDANCE_ID);
            // ログアウト画面への遷移
            return RedirectToAction("index", "UserLogin");
        }
    }
}