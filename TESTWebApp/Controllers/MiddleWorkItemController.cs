using Microsoft.AspNetCore.Mvc;
using TESTWebApp.Models;
using TESTWebApp.Models.MiddleWorkItem;
using TESTWebApp.Services.Cookie.Models;
using TESTWebApp.UseCase.MiddleWorkItems.Commands.Create;
using TESTWebApp.UseCase.MiddleWorkItems.Commands.Delete;
using TESTWebApp.UseCase.MiddleWorkItems.Commands.Edit;
using TESTWebApp.UseCase.MiddleWorkItems.Queries;
using TESTWebApp.UseCase.Users.Queries;

namespace TESTWebApp.Controllers
{
    /// <summary>
    /// 中項目コントローラー
    /// </summary>
    public class MiddleWorkItemController : Controller
    {
        private readonly IUserDataQueryService _userDataQueryService;
        private readonly IMiddleWorkItemQueryService _middleWorkItemQueryService;
        private readonly IMiddleWorkItemCreateCommand _middleWorkItemCreateCommand;
        private readonly IMiddleWorkItemDeleteCommand _middleWorkItemDeleteCommand;
        private readonly IMiddleWorkItemEditCommand _middleWorkItemEditCommand;

        public MiddleWorkItemController(
            IUserDataQueryService userDataQueryService,
            IMiddleWorkItemQueryService middleWorkItemQueryService,
            IMiddleWorkItemCreateCommand middleWorkItemCreateCommand,
            IMiddleWorkItemDeleteCommand middleWorkItemDeleteCommand,
            IMiddleWorkItemEditCommand middleWorkItemEditCommand) 
        {
            _userDataQueryService = userDataQueryService;
            _middleWorkItemQueryService = middleWorkItemQueryService;
            _middleWorkItemCreateCommand = middleWorkItemCreateCommand;
            _middleWorkItemDeleteCommand = middleWorkItemDeleteCommand;
            _middleWorkItemEditCommand = middleWorkItemEditCommand;
        }


        ///<summary>中項目一覧ページ</summary>
        /// <param name="id">MajorItemId</param>
        /// <param name="name">MajorItemName</param>
        public ActionResult Index(string id, string name)
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

            // ビューモデル
            MiddleWorkItemViewModel viewModel = new()
            {
                LoginUserName = searchUserData.UserName,
                MajorWorkItemId = id,
                MajorWorkItemName = name,
                MiddleWorkItems = _middleWorkItemQueryService.GetAllMiddleWorkItem(id)
            };

            // 画面表示用メッセージ
            ViewBag.OperationMessage = TempData["OperationMessage"];
            ViewBag.ErrorMessage = TempData["ErrorMessage"];

            return View("MiddleWorkItem", viewModel);
        }


        /// <summary>
        /// 中項目作成ページ
        /// </summary>
        /// <param name="id">MajorItemId</param>
        /// <param name="name">MajorItemName</param>
        public ActionResult CreateMiddleWorkItemPage(string id, string name)
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

            // ビューモデル
            MiddleWorkItemViewModel viewModel = new()
            {
                LoginUserName = searchUserData.UserName,
                MajorWorkItemId = id,
                MajorWorkItemName = name
            };

            // 画面表示用メッセージ
            ViewBag.OperationMessage = TempData["OperationMessage"];
            ViewBag.ErrorMessage = TempData["ErrorMessage"];

            return View("CreateMiddleWorkItem", viewModel);
        }


        /// <summary>
        /// 中項目作成
        /// </summary>
        /// <param name="viewModel">中項目ビューモデル</param>
        /// <returns>中項目作成ページ</returns>
        [HttpPost]
        public ActionResult CreateMiddleWorkItem(MiddleWorkItemViewModel viewModel)
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

            if (viewModel.NewMiddleWorkItemName is null)
            {
                TempData["ErrorMessage"] = "登録する作業項目名を入力してください。";
                return RedirectToAction(
                    "CreateMiddleWorkItemPage",
                    new 
                    { 
                        id = viewModel.MajorWorkItemId,
                        name = viewModel.MajorWorkItemName 
                    });
            }

            try
            {
                var request = new CreateMiddleWorkItemRequest(
                    middleWorkItemName: viewModel.NewMiddleWorkItemName,
                    foreignKey: viewModel.MajorWorkItemId,
                    createdBy: userIdByCookie);
                _middleWorkItemCreateCommand.Execute(request);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(
                    "CreateMiddleWorkItemPage",
                    new 
                    { 
                        id = viewModel.MajorWorkItemId,
                        name = viewModel.MajorWorkItemName 
                    });
            }

            TempData["OperationMessage"] = $"{viewModel.NewMiddleWorkItemName} の登録が完了しました。";
            return RedirectToAction(
                "CreateMiddleWorkItemPage",
                new 
                { 
                    id = viewModel.MajorWorkItemId,
                    name = viewModel.MajorWorkItemName 
                });
        }


        /// <summary>中項目編集ページ</summary>
        /// <param name="id">MiddleItemId</param>
        /// <param name="fk">MajorItemId</param>
        /// <param name="name">MajorItemName</param>
        /// <returns>中項目一覧ページ</returns>
        public ActionResult EditMiddleWorkItemPage(string id, string fk, string name)
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

            MiddleWorkItemEditViewModel viewModel = new()
            {
                LoginUserName = searchUserData.UserName,
                CurrentMiddleWorkItem = _middleWorkItemQueryService.GetMiddleWorkItem(id),
                MajorWorkItemId = fk,
                MajorWorkItemName = name
            };

            ViewBag.OperationMessage = TempData["OperationMessage"];
            ViewBag.ErrorMessage = TempData["ErrorMessage"];

            return View("EditMiddleWorkItem", viewModel);
        }


        /// <summary>
        /// 中項目編集
        /// </summary>
        /// <param name="viewModel">中項目ビューモデル</param>
        /// <returns>中項目一覧ページ</returns>
        [HttpPost]
        public ActionResult EditMiddleWorkItem(MiddleWorkItemEditViewModel viewModel)
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

            if (viewModel.NewMiddleWorkItemName is null)
            {
                TempData["ErrorMessage"] = "登録する作業項目名を入力してください。";
                return RedirectToAction(
                    "EditMiddleWorkItemPage",
                    new
                    {
                        id = viewModel.EditMiddleWorkItemId,
                        fk = viewModel.MajorWorkItemId,
                        viewModel.MajorWorkItemName
                    });
            }

            try
            {
                _middleWorkItemEditCommand.Execute(new EditMiddleWorkItemRequest(
                    middleWorkItemId: viewModel.EditMiddleWorkItemId,
                    middleWorkItemName: viewModel.NewMiddleWorkItemName,
                    modifedBy: userIdByCookie,
                    isDeleted: viewModel.IsDeleted));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(
                    "EditMiddleWorkItemPage", 
                    new 
                    { 
                        id = viewModel.EditMiddleWorkItemId,
                        fk = viewModel.MajorWorkItemId,
                        viewModel.MajorWorkItemName 
                    });
            }

            TempData["OperationMessage"] = "変更が完了しました。";
            return RedirectToAction(
                "Index",
                new 
                { 
                    id = viewModel.MajorWorkItemId,
                    name = viewModel.MajorWorkItemName 
                });
        }


        /// <summary>
        /// 中項目削除
        /// </summary>
        /// <param name="majorWorkItemId">親項目ID</param>
        /// <param name="majorWorkItemName">親項目名</param>
        /// <param name="delWorkItemId">削除中項目ID</param>
        /// <param name="delWorkItemName">削除中項目名</param>
        /// <returns>中項目一覧ページ</returns>
        [HttpPost]
        public ActionResult DeleteMiddleWorkItem(
            string majorWorkItemId,
            string majorWorkItemName,
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
                _middleWorkItemDeleteCommand.Execute(new DeleteMiddleWorkItemRequest(delWorkItemId, userIdByCookie));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(
                    "EditMiddleWorkItemPage",
                    new { id = delWorkItemId, fk = majorWorkItemId, name = majorWorkItemName });
            }

            TempData["OperationMessage"] = $"{delWorkItemName} を完全削除しました。";
            return RedirectToAction(
                "Index",
                new { id = majorWorkItemId, name = majorWorkItemName });
        }
    }
}
