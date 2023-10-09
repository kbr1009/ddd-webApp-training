using Microsoft.AspNetCore.Mvc;
using TESTWebApp.Models;
using TESTWebApp.UseCase.Users.Commands.Create;

namespace TESTWebApp.Controllers
{
    public class SignUpController : Controller
    {
        private readonly IUserCreateCommand _userCreateCommand;

        public SignUpController(IUserCreateCommand userCreateCommand) 
        { 
            _userCreateCommand = userCreateCommand;
        }

        public ActionResult Index()
        {
            SignUpViewModel signUpViewModel = new SignUpViewModel();
            ViewBag.OperationMessage = TempData["OperationMessage"];
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View("SignUp", signUpViewModel);
        }

        [HttpPost]
        public ActionResult ExecuteSiginUp(SignUpViewModel viewModel)
        {
            if (viewModel.NewUserName is null)
            {
                TempData["ErrorMessage"] = "登録する作業者名を入力してください。";
                return RedirectToAction("Index");
            }

            try
            {
                var createUserModel = new CreateUserRequest(viewModel.NewUserName, "xxxxx-xxxxx-xxxxx-xxxxx");
                _userCreateCommand.Execute(createUserModel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }

            TempData["OperationMessage"] = $"{viewModel.NewUserName} の登録が完了しました。";
            return RedirectToAction("Index");
        }
    }
}
