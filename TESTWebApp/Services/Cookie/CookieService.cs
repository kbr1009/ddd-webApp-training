using Microsoft.AspNetCore.Mvc;
using TESTWebApp.Models;

namespace TESTWebApp.Services.Cookie
{
    public class CookieService : ICookieService
    {
        private readonly HttpContext _context;
        public CookieService(HttpContext httpContext) 
        { 
            _context = httpContext;
        }
        public string GetCookieItem(string key)
        {
             return _context.Request.Cookies[key];
        }

        public void SetCookieItem(string key, string value)
        {
            _context.Response.Cookies.Append(key, value);
        }

        public void DeleteCookieItem(string key)
        {
            _context.Response.Cookies.Delete(key);
        }
    }
}
