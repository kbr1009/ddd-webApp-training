using Microsoft.AspNetCore.Mvc;
using TESTWebApp.Models;
using TESTWebApp.Services.Cookie.Models;
using TESTWebApp.UseCase.Users.Queries;

namespace TESTWebApp.Controllers
{
    public class UserLoginController : Controller
    {
        private readonly IUserDataQuery _userDataQuery;

        public UserLoginController(IUserDataQuery userDataQuery)
        {
            _userDataQuery = userDataQuery;
        }

        // GET: UserLoginController
        public ActionResult Index()
        {
            IEnumerable<UserDataResponse> users = _userDataQuery.GetAllUser();
            LoginViewModel model = new LoginViewModel()
            {
                Users = users
            };
            return View("Login", model);
        }

        [HttpPost]
        public ActionResult RedirectToSignUpHandle()
        {
            return RedirectToAction("index", "SignUp");
        }

        // POST: UserLoginController/Create
        [HttpPost]
        public ActionResult ExecuteLogin(LoginViewModel loginViewModel)
        {
            // ログインセッションをセットする
            HttpContext.Response.Cookies.Append(WebAuthCookieKey.ATTENDANCE_ID, loginViewModel.LoginId);
            return RedirectToAction("index", "Home");
        }
    }
}
