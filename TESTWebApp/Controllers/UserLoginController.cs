using Microsoft.AspNetCore.Mvc;
using TESTWebApp.Models;
using TESTWebApp.Services.Cookie.Models;
using TESTWebApp.UseCase.Users.Commands.CreateSession;
using TESTWebApp.UseCase.Users.Queries;

namespace TESTWebApp.Controllers
{
    public class UserLoginController : Controller
    {
        private readonly IUserDataQueryService _userDataQuery;
        private readonly IWebSessionCreateCommand _sessionCreateCommand;

        public UserLoginController(
            IUserDataQueryService userDataQuery,
            IWebSessionCreateCommand webSessionCreateCommand)
        {
            _userDataQuery = userDataQuery;
            _sessionCreateCommand = webSessionCreateCommand;
        }

        public ActionResult Index()
        {
            IEnumerable<UserDataResponse> users = _userDataQuery.GetAllUser();
            LoginViewModel model = new LoginViewModel() { Users = users };
            return View("Login", model);
        }

        [HttpPost]
        public ActionResult ExecuteLogin(LoginViewModel loginViewModel)
        {
            // ログインセッションをセットする
            HttpContext.Response.Cookies.Append(WebAuthCookieKey.ATTENDANCE_ID, loginViewModel.LoginId);
            string webSessionId = _sessionCreateCommand.Execute(new CreateWebSessionRequest(loginViewModel.LoginId));
            HttpContext.Response.Cookies.Append("webSession", webSessionId);
            return RedirectToAction("index", "Home");
        }
    }
}
