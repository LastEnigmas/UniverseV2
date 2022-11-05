using CoreA.DTOs.MainDTOs;
using CoreA.Services.MainService;
using Microsoft.AspNetCore.Mvc;

namespace UniverseV2.Areas.Main.Controllers
{
    [Area("Main")]
    public class MainHomeController : Controller
    {
        private readonly IMainService _mainService;
        public MainHomeController( IMainService mainService )
        {
            _mainService = mainService;
        }


        #region SignIn
        public IActionResult SignIn() => View();

        [HttpPost]
        public IActionResult SignIn(SignInViewModel signIn )
        {
            return View();
        }

        #endregion

        #region SignUp

        [Route("SignUp")]
        public IActionResult SignUp() => View();

        [HttpPost]
        [Route("SignUp")]
        public IActionResult SignUp(SignUpViewModel signUp )
        {
            if (!ModelState.IsValid)
            {
                return View(signUp);
            }

            return View(signUp);
        }

        #endregion
    }
}
