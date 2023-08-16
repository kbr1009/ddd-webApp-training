using Microsoft.AspNetCore.Mvc.Rendering;
using TESTWebApp.UseCase.Users.Queries;

namespace TESTWebApp.Models
{
    public class LoginViewModel
    {
        public IEnumerable<UserDataResponse> Users { get; set; } = new List<UserDataResponse>();
        public IEnumerable<SelectListItem> SelectListItems
        {
            get
            {
                foreach (var i in Users)
                {
                    yield return new SelectListItem { Text = i.UserName, Value = i.Id };
                }
            }
        }

        public string LoginId { get; set; } = string.Empty;
    }
}
