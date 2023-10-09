using Microsoft.AspNetCore.Mvc;
using TESTWebApp.Services.Cookie.Models;
using TESTWebApp.UseCase.MajorWorkItems.Commands.Create;
using TESTWebApp.UseCase.Users.Queries;
using TESTWebApp.UseCase.MajorWorkItems.Queries;
using TESTWebApp.Models;
using TESTWebApp.UseCase.MajorWorkItems.Commands.Delete;
using TESTWebApp.Models.MajorWorkItem;
using TESTWebApp.UseCase.MajorWorkItems.Commands.Edit;

namespace TESTWebApp.Controllers
{
    public class MajorWorkItemController : Controller
    {
        private readonly IUserDataQueryService _userDataQueryService;
        private readonly IMajorWorkItemQueryService _majorWorkItemQueryService;
        private readonly IMajorWorkItemCreateCommand _majorWorkItemCreateCommand;
        private readonly IMajorWorkItemDeleteCommand _majorWorkItemDeleteCommand;
        private readonly IMajorWorkItemEditCommand _majorWorkItemEditCommand;

        public MajorWorkItemController(
            IUserDataQueryService userDataQueryService,
            IMajorWorkItemQueryService majorWorkItemQueryService,
            IMajorWorkItemCreateCommand majorWorkItemCreateCommand,
            IMajorWorkItemDeleteCommand majorWorkItemDeleteCommand,
            IMajorWorkItemEditCommand majorWorkItemEditCommand)
        {
            _userDataQueryService = userDataQueryService;
            _majorWorkItemQueryService = majorWorkItemQueryService;
            _majorWorkItemCreateCommand = majorWorkItemCreateCommand;
            _majorWorkItemDeleteCommand = majorWorkItemDeleteCommand;
            _majorWorkItemEditCommand = majorWorkItemEditCommand;
        }

        public ActionResult Index()
        {
            // ユーザのログイン情報があるか
            string userIdByCookie = HttpContext.Request.Cookies[WebAuthCookieKey.ATTENDANCE_ID];

            // セッションが無ければログイン画面にリダイレクトする
            if (string.IsNullOrWhiteSpace(userIdByCookie))
            {
                return RedirectToAction("index", "UserLogin");
            }

            UserDataResponse searchUserData = _userDataQueryService.FindUserById(userIdByCookie);
            if (searchUserData is null)
            {
                // クッキーの削除
                HttpContext.Response.Cookies.Delete(WebAuthCookieKey.ATTENDANCE_ID);
                // ログイン画面へのリダイレクト
                return RedirectToAction("index", "UserLogin");
            }

            MajorWorkItemViewModel viewModel = new MajorWorkItemViewModel();
            viewModel.LoginUserName = searchUserData.UserName;
            viewModel.MajorWorkItems = _majorWorkItemQueryService.GetAllMajorWorkItem();
            ViewBag.OperationMessage = TempData["OperationMessage"];
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View("MajorWorkItem", viewModel);
        }

        public ActionResult CreateMajorWorkItemPage()
        {
            // ユーザのログイン情報があるか
            string userIdByCookie = HttpContext.Request.Cookies[WebAuthCookieKey.ATTENDANCE_ID];

            // セッションが無ければログイン画面にリダイレクトする
            if (string.IsNullOrWhiteSpace(userIdByCookie))
            {
                return RedirectToAction("index", "UserLogin");
            }

            UserDataResponse searchUserData = _userDataQueryService.FindUserById(userIdByCookie);
            if (searchUserData is null)
            {
                // クッキーの削除
                HttpContext.Response.Cookies.Delete(WebAuthCookieKey.ATTENDANCE_ID);
                // ログイン画面へのリダイレクト
                return RedirectToAction("index", "UserLogin");
            }

            MajorWorkItemViewModel viewModel = new MajorWorkItemViewModel();
            viewModel.LoginUserName = searchUserData.UserName;
            ViewBag.OperationMessage = TempData["OperationMessage"];
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View("CreateMajorWorkItem", viewModel);
        }


        [HttpPost]
        public ActionResult CreateMajorWorkItem(MajorWorkItemViewModel viewModel)
        {
            // ユーザのログイン情報があるか
            string userIdByCookie = HttpContext.Request.Cookies[WebAuthCookieKey.ATTENDANCE_ID];
            if (string.IsNullOrWhiteSpace(userIdByCookie))
            {
                return RedirectToAction("index", "UserLogin");
            }

            UserDataResponse searchUserData = _userDataQueryService.FindUserById(userIdByCookie);
            if (searchUserData is null)
            {
                // クッキーの削除
                HttpContext.Response.Cookies.Delete(WebAuthCookieKey.ATTENDANCE_ID);
                // ログイン画面へのリダイレクト
                return RedirectToAction("index", "UserLogin");
            }

            if (viewModel.NewMajorWorkItemName is null)
            {
                TempData["ErrorMessage"] = "登録する作業項目名を入力してください。";
                return RedirectToAction("CreateMajorWorkItemPage");
            }

            try
            {
                _majorWorkItemCreateCommand.Execute(
                    new CreateMajorWorkItemRequest(
                    viewModel.NewMajorWorkItemName, userIdByCookie));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("CreateMajorWorkItemPage");
            }

            TempData["OperationMessage"] = $"{viewModel.NewMajorWorkItemName} の登録が完了しました。";
            return RedirectToAction("CreateMajorWorkItemPage");
        }


        public ActionResult EditMajorWorkItemPage(string id)
        {
            // ユーザのログイン情報があるか
            string userIdByCookie = HttpContext.Request.Cookies[WebAuthCookieKey.ATTENDANCE_ID];

            // セッションが無ければログイン画面にリダイレクトする
            if (string.IsNullOrWhiteSpace(userIdByCookie))
            {
                return RedirectToAction("index", "UserLogin");
            }

            UserDataResponse searchUserData = _userDataQueryService.FindUserById(userIdByCookie);
            if (searchUserData is null)
            {
                // クッキーの削除
                HttpContext.Response.Cookies.Delete(WebAuthCookieKey.ATTENDANCE_ID);
                // ログイン画面へのリダイレクト
                return RedirectToAction("index", "UserLogin");
            }

            MajorWorkItemEditViewModel viewModel = new MajorWorkItemEditViewModel();
            viewModel.LoginUserName = searchUserData.UserName;
            viewModel.CurrentMajorWorkItem = _majorWorkItemQueryService.GetMajorWorkItem(id);
            ViewBag.OperationMessage = TempData["OperationMessage"];
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View("EditMajorWorkItem", viewModel);
        }

        [HttpPost]
        public ActionResult EditMajorWorkItem(MajorWorkItemEditViewModel viewModel)
        {
            // ユーザのログイン情報があるか
            string userIdByCookie = HttpContext.Request.Cookies[WebAuthCookieKey.ATTENDANCE_ID];
            if (string.IsNullOrWhiteSpace(userIdByCookie))
            {
                return RedirectToAction("index", "UserLogin");
            }

            UserDataResponse searchUserData = _userDataQueryService.FindUserById(userIdByCookie);
            if (searchUserData is null)
            {
                // クッキーの削除
                HttpContext.Response.Cookies.Delete(WebAuthCookieKey.ATTENDANCE_ID);
                // ログイン画面へのリダイレクト
                return RedirectToAction("index", "UserLogin");
            }

            if (viewModel.NewMajorWorkItemName is null)
            {
                TempData["ErrorMessage"] = "登録する作業項目名を入力してください。";
                return RedirectToAction("EditMajorWorkItemPage", new { id = viewModel.EditMajorWorkItemId });
            }

            try
            {
                _majorWorkItemEditCommand.Execute(new EditMajorWorkItemRequest(
                    majorWorkItemId: viewModel.EditMajorWorkItemId,
                    majorWorkItemName: viewModel.NewMajorWorkItemName,
                    modifiedBy: userIdByCookie,
                    isDeleted: viewModel.IsDeleted));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("EditMajorWorkItemPage", new { id = viewModel.EditMajorWorkItemId });
            }

            TempData["OperationMessage"] = "変更が完了しました。";
            //return RedirectToAction("EditMajorWorkItemPage", new { id = viewModel.EditMajorWorkItemId });
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteMajorWorkItem(string delWorkItemId, string delWorkItemName)
        {
            // ユーザのログイン情報があるか
            string userIdByCookie = HttpContext.Request.Cookies[WebAuthCookieKey.ATTENDANCE_ID];
            if (string.IsNullOrWhiteSpace(userIdByCookie))
            {
                return RedirectToAction("index", "UserLogin");
            }

            UserDataResponse searchUserData = _userDataQueryService.FindUserById(userIdByCookie);
            if (searchUserData is null)
            {
                // クッキーの削除
                HttpContext.Response.Cookies.Delete(WebAuthCookieKey.ATTENDANCE_ID);
                // ログイン画面へのリダイレクト
                return RedirectToAction("index", "UserLogin");
            }

            try
            {
                _majorWorkItemDeleteCommand.Execute(
                    new DeleteMajorWorkItemRequest(
                        delWorkItemId, userIdByCookie));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("EditMajorWorkItemPage", new { id = delWorkItemId });
            }

            TempData["OperationMessage"] = $"{delWorkItemName} を完全削除しました。";
            return RedirectToAction("Index");
        }
    }
}
