using Microsoft.AspNetCore.Http;
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

        // GET: SignUpController
        public ActionResult Index()
        {
            SignUpViewModel signUpViewModel = new SignUpViewModel();
            ViewBag.OperationMessage = TempData["OperationMessage"];
            return View("SignUp", signUpViewModel);
        }

        // POST: SignUpController/Create
        [HttpPost]
        public ActionResult ExecuteSiginUp(SignUpViewModel viewModel)
        {
            if (string.IsNullOrWhiteSpace(viewModel.NewUserName))
            {
                viewModel.ErrMsg = "登録する作業者名を入力してください。";
                viewModel.HasErr = true;
                return View("SignUp", viewModel);
            }

            try
            {
                var createUserModel = new CreateUserRequest(viewModel.NewUserName, "xxxxx-xxxxx-xxxxx-xxxxx");
                _userCreateCommand.Execute(createUserModel);
            }
            catch (Exception ex)
            {
                viewModel.ErrMsg = ex.Message;
                viewModel.HasErr = true;
                return View("SignUp", viewModel);
            }

            TempData["OperationMessage"] = $"{viewModel.NewUserName} の登録が完了しました。";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RedirectToHomeHandle()
        {
            return RedirectToAction("index", "Home");
        }
    }
}
