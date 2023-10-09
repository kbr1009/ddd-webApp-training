using Microsoft.AspNetCore.Mvc;
using TESTWebApp.Models;
using TESTWebApp.Models.MinorWorkItem;
using TESTWebApp.Services.Cookie.Models;
using TESTWebApp.UseCase.MiddleWorkItems.Commands.Delete;
using TESTWebApp.UseCase.MinorWorkItems.Commands.Create;
using TESTWebApp.UseCase.MinorWorkItems.Commands.Delete;
using TESTWebApp.UseCase.MinorWorkItems.Commands.Edit;
using TESTWebApp.UseCase.MinorWorkItems.Queries;
using TESTWebApp.UseCase.Users.Queries;

namespace TESTWebApp.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class MinorWorkItemController : Controller
    {
        private readonly IUserDataQueryService _userDataQueryService;
        private readonly IMinorWorkItemCreateCommand _minorWorkItemCreateCommand;
        private readonly IMinorWorkItemEditCommand _minorWorkItemEditCommand;
        private readonly IMinorWorkItemDeleteCommand _minorWorkItemDeleteCommand;
        private readonly IMinorWorkItemQueryService _minorWorkItemQueryService;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="userDataQueryService"></param>
        /// <param name="minorWorkItemCreateCommand"></param>
        /// <param name="minorWorkItemDeleteCommand"></param>
        /// <param name="minorWorkItemEditCommand"></param>
        /// <param name="minorWorkItemQueryService"></param>
        public MinorWorkItemController(
            IUserDataQueryService userDataQueryService,
            IMinorWorkItemCreateCommand minorWorkItemCreateCommand,
            IMinorWorkItemEditCommand minorWorkItemEditCommand,
            IMinorWorkItemDeleteCommand minorWorkItemDeleteCommand,
            IMinorWorkItemQueryService minorWorkItemQueryService
            )
        {
            _userDataQueryService = userDataQueryService;
            _minorWorkItemCreateCommand = minorWorkItemCreateCommand;
            _minorWorkItemEditCommand = minorWorkItemEditCommand;
            _minorWorkItemDeleteCommand = minorWorkItemDeleteCommand;
            _minorWorkItemQueryService = minorWorkItemQueryService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rootId"></param>
        /// <param name="rootName"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public ActionResult Index(string rootId, string rootName, string id, string name)
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

            MinorWorkItemViewModel viewModel = new()
            {
                LoginUserName = searchUserData.UserName,
                MajorWorkItemId = rootId,
                MajorWorkItemName = rootName,
                MiddleWorkItemId = id,
                MiddleWorkItemName = name,
                MinorWorkItems = _minorWorkItemQueryService.GetAllMinorWorkItem(id)
            };

            // 画面表示用メッセージ
            ViewBag.OperationMessage = TempData["OperationMessage"];
            ViewBag.ErrorMessage = TempData["ErrorMessage"];

            return View("MinorWorkItem", viewModel);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="rootId"></param>
        /// <param name="rootName"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public ActionResult CreateMinorWorkItemPage(string rootId, string rootName, string id, string name)
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

            // 画面表示用メッセージ
            ViewBag.OperationMessage = TempData["OperationMessage"];
            ViewBag.ErrorMessage = TempData["ErrorMessage"];

            return View(
                "CreateMinorWorkItem",
                new MinorWorkItemViewModel()
                {
                    LoginUserName = searchUserData.UserName,
                    MajorWorkItemId = rootId,
                    MajorWorkItemName = rootName,
                    MiddleWorkItemId = id,
                    MiddleWorkItemName = name
                });
        }


        /// <summary>
        /// 小項目作成
        /// </summary>
        /// <param name="viewModel">小項目ビューモデル</param>
        /// <returns>小項目作成ページ</returns>
        [HttpPost]
        public ActionResult CreateMinorWorkItem(MinorWorkItemViewModel viewModel)
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

            if (viewModel.NewMinorWorkItemName is null)
            {
                TempData["ErrorMessage"] = "登録する作業項目名を入力してください。";
                return RedirectToAction(
                    "CreateMinorWorkItemPage",
                    new
                    {
                        rootId = viewModel.MajorWorkItemId,
                        rootName = viewModel.MajorWorkItemName,
                        id = viewModel.MiddleWorkItemId,
                        name = viewModel.MiddleWorkItemName
                    });
            }

            try
            {
                _minorWorkItemCreateCommand.Execute(new CreateMinorWorkItemRequest(
                    minorWorkItemName: viewModel.NewMinorWorkItemName,
                    majorWorkItemId: viewModel.MajorWorkItemId,
                    middleWorkItemId: viewModel.MiddleWorkItemId,
                    createdBy: searchUserData.Id));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(
                    "CreateMinorWorkItemPage",
                    new
                    {
                        rootId = viewModel.MajorWorkItemId,
                        rootName = viewModel.MajorWorkItemName,
                        id = viewModel.MiddleWorkItemId,
                        name = viewModel.MiddleWorkItemName
                    });
            }

            TempData["OperationMessage"] = $"{viewModel.NewMinorWorkItemName} の登録が完了しました。";
            return RedirectToAction(
                "CreateMinorWorkItemPage",
                new
                {
                    rootId = viewModel.MajorWorkItemId,
                    rootName = viewModel.MajorWorkItemName,
                    id = viewModel.MiddleWorkItemId,
                    name = viewModel.MiddleWorkItemName
                });
        }


        /// <summary>小項目編集ページ</summary>
        /// <param name="id">小項目ID</param>
        /// <param name="majorWorkItemId">関連大項目ID</param>
        /// <param name="majorWorkItemName">関連大項目名</param>
        /// <param name="middleWorkItemId">関連中項目ID</param>
        /// <param name="middleWorkItemName">関連中項目名</param>
        /// <returns>中項目一覧ページ</returns>
        public ActionResult EditMinorWorkItemPage(
            string id,
            string majorWorkItemId,
            string majorWorkItemName,
            string middleWorkItemId,
            string middleWorkItemName)
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

            // ビューモデル生成
            MinorWorkItemEditViewModel viewModel = new()
            {
                LoginUserName = searchUserData.UserName,
                CurrentMinorWorkItem = _minorWorkItemQueryService.GetMinorWorkItem(id),
                MajorWorkItemId = majorWorkItemId,
                MajorWorkItemName = majorWorkItemName,
                MiddleWorkItemId = middleWorkItemId,
                MiddleWorkItemName = middleWorkItemName
            };

            // 編集画面に表示するメッセージ
            ViewBag.OperationMessage = TempData["OperationMessage"];
            ViewBag.ErrorMessage = TempData["ErrorMessage"];

            return View("EditMinorWorkItem", viewModel);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditMinorWorkItem(MinorWorkItemEditViewModel viewModel)
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

            if (viewModel.NewMinorWorkItemName is null)
            {
                TempData["ErrorMessage"] = "登録する作業項目名を入力してください。";
                return RedirectToAction(
                    "EditMinorWorkItemPage",
                    new
                    {
                        id = viewModel.EditMinorWorkItemId,
                        majorWorkItemId = viewModel.MajorWorkItemId,
                        majorWorkItemName = viewModel.MajorWorkItemName,
                        middleWorkItemId = viewModel.MiddleWorkItemId,
                        middleWorkItemName = viewModel.MiddleWorkItemName,
                    });
            }

            try
            {
                _minorWorkItemEditCommand.Execute(new EditMinorWorkItemRequest(
                    minorWorkItemId: viewModel.EditMinorWorkItemId,
                    minorWorkItemName: viewModel.NewMinorWorkItemName,
                    modifedBy: userIdByCookie,
                    isDeleted: viewModel.IsDeleted
                    ));
            }
            catch (Exception ex) 
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(
                    "EditMinorWorkItemPage",
                    new
                    {
                        id = viewModel.EditMinorWorkItemId,
                        majorWorkItemId = viewModel.MajorWorkItemId,
                        majorWorkItemName = viewModel.MajorWorkItemName,
                        middleWorkItemId = viewModel.MiddleWorkItemId,
                        middleWorkItemName = viewModel.MiddleWorkItemName,
                    });
            }

            TempData["OperationMessage"] = "変更が完了しました。";
            return RedirectToAction(
                "Index",
                new
                {
                    rootId = viewModel.MajorWorkItemId,
                    rootName = viewModel.MajorWorkItemName,
                    id = viewModel.MiddleWorkItemId,
                    name = viewModel.MiddleWorkItemName
                });
        }


        [HttpPost]
        public ActionResult DeleteMinorWorkItem(
            string majorWorkItemId,
            string majorWorkItemName,
            string middleWorkItemId,
            string middleWorkItemName,
            string delWorkItemId,
            string delWorkItemName)
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
                _minorWorkItemDeleteCommand.Execute(new DeleteMinorWorkItemRequest(delWorkItemId, userIdByCookie));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(
                    "EditMinorWorkItemPage",
                    new
                    {
                        id = delWorkItemId,
                        majorWorkItemId = majorWorkItemId,
                        majorWorkItemName = majorWorkItemName,
                        middleWorkItemId = middleWorkItemId,
                        middleWorkItemName = middleWorkItemName,
                    });
            }

            TempData["OperationMessage"] = $"{delWorkItemName} を完全削除しました。";
            return RedirectToAction(
                "Index",
                new
                {
                    rootId = majorWorkItemId,
                    rootName = majorWorkItemName,
                    id = middleWorkItemId,
                    name = middleWorkItemName
                });
        }

    }
}
