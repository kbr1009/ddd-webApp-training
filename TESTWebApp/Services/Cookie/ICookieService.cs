namespace TESTWebApp.Services.Cookie
{
    public interface ICookieService
    {
        string GetCookieItem(string key);
        void SetCookieItem(string key, string value);
        void DeleteCookieItem(string key);
    }
}
