using Microsoft.AspNetCore.Mvc;
using TESTWebApp.Services.Cookie.Models;
using TESTWebApp.UseCase.Users.Queries;

namespace TESTWebApp.Services.Auth
{
    public class AuthService : Controller, IAuthService
    {
        private readonly HttpContext _httpContext;

        private readonly IUserDataQueryService _userDataQueryService;

        public string UserIdByCookie { get; }

        public UserDataResponse UserData { get; private set; }

        public AuthService(IUserDataQueryService userDataQueryService, HttpContext httpContext)
        {
            _httpContext = httpContext;
            _userDataQueryService = userDataQueryService;
            UserIdByCookie = httpContext.Request.Cookies[WebAuthCookieKey.ATTENDANCE_ID];
        }

        public bool Authorize()
        {
            // セッションが無ければログイン画面にリダイレクトする
            if (string.IsNullOrWhiteSpace(this.UserIdByCookie))
            {
                return false;
            }

            UserDataResponse searchUserData = _userDataQueryService.FindUserById(this.UserIdByCookie);
            if (searchUserData is null)
            {
                // クッキーの削除
                _httpContext.Response.Cookies.Delete(WebAuthCookieKey.ATTENDANCE_ID);
                // ログイン画面へのリダイレクト
                return false;
            }
            UserData = searchUserData;
            return true;
        }
    }
}
