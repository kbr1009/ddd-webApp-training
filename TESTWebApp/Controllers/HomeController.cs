using Microsoft.AspNetCore.Mvc;
using TESTWebApp.UseCase.WorkInputs.Queries;
using TESTWebApp.Models;
using TESTWebApp.UseCase.Users.Queries;
using TESTWebApp.Services.Cookie.Models;
using TESTWebApp.UseCase.WorkInputs.Commands.Create;
using TESTWebApp.UseCase.MajorWorkItems.Queries;
using TESTWebApp.UseCase.MiddleWorkItems.Queries;
using TESTWebApp.UseCase.MinorWorkItems.Queries;

namespace TESTWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWorkInputDataQueryService _workInputDataQuery;
        private readonly IWorkInputCreateCommand _workInputCreateCommand;
        private readonly IMajorWorkItemQueryService _majorWorkItemQueryService;
        private readonly IMiddleWorkItemQueryService _middleWorkItemQueryService;
        private readonly IMinorWorkItemQueryService _minorWorkItemQueryService;
        private readonly IUserDataQueryService _userDataQuery;

        public HomeController(
            IWorkInputDataQueryService workInputDataQuery,
            IWorkInputCreateCommand workInputCreateCommand,
            IMajorWorkItemQueryService majorWorkItemQueryService,
            IMiddleWorkItemQueryService middleWorkItemQueryService,
            IMinorWorkItemQueryService minorWorkItemQueryService,
            IUserDataQueryService userDataQuery
            )
        {
            _workInputDataQuery = workInputDataQuery;
            _workInputCreateCommand = workInputCreateCommand;
            _majorWorkItemQueryService = majorWorkItemQueryService;
            _middleWorkItemQueryService = middleWorkItemQueryService;
            _minorWorkItemQueryService = minorWorkItemQueryService;
            _userDataQuery = userDataQuery;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            // ユーザのログイン情報があるか
            string userIdByCookie = HttpContext.Request.Cookies[WebAuthCookieKey.ATTENDANCE_ID];

            // セッションが無ければログイン画面にリダイレクトする
            if (string.IsNullOrWhiteSpace(userIdByCookie))
            {
                return RedirectToAction("index", "UserLogin");
            }

            UserDataResponse searchUserData = _userDataQuery.FindUserById(userIdByCookie);
            if (searchUserData is null)
            {
                // クッキーの削除
                HttpContext.Response.Cookies.Delete(WebAuthCookieKey.ATTENDANCE_ID);
                // ログイン画面へのリダイレクト
                return RedirectToAction("index", "UserLogin");
            }

            // エラー等
            ViewBag.ErrorMessage = TempData["ErrorMessage"];

            return View(new WorkInputViewModel()
            {
                WorkInputDatas = _workInputDataQuery.FindWorkInputDataForToday(userIdByCookie, DateTime.Now),
                MajorWorkItems = _majorWorkItemQueryService.GetMajorWorkItemsNotDel(),
                userData = searchUserData
            });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="workInputViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ExcecuteWork(WorkInputViewModel workInputViewModel)
        {
            string userIdByCookie = HttpContext.Request.Cookies[WebAuthCookieKey.ATTENDANCE_ID];
            if (string.IsNullOrWhiteSpace(userIdByCookie))
            {
                return RedirectToAction("index", "UserLogin");
            }
            string session = HttpContext.Request.Cookies["webSession"];
            if (string.IsNullOrWhiteSpace(userIdByCookie))
            {
                return RedirectToAction("index", "UserLogin");
            }

            try
            {
                _workInputCreateCommand.Excecute(new CreateWorkInputRequest(
                    userId: userIdByCookie,
                    webSessionId: session,
                    workItem: workInputViewModel.MajorWorkItemId ?? string.Empty,
                    middleWorkItemId: workInputViewModel.MiddleWorkItemId ?? string.Empty,
                    minorWorkItemId: workInputViewModel.MinorWorkItemId ?? string.Empty,
                    workStatus: workInputViewModel.InputWorkStatus));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"実行に失敗しました。{ex.Message}";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Logout()
        {
            // クッキーの削除
            HttpContext.Response.Cookies.Delete(WebAuthCookieKey.ATTENDANCE_ID);
            // ログアウト画面への遷移
            return RedirectToAction("index", "UserLogin");
        }
    }
}