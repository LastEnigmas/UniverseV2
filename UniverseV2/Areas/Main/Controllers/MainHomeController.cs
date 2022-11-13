using CoreA.DTOs.MainDTOs;
using CoreA.Generator;
using CoreA.Security;
using CoreA.Sender;
using CoreA.Services.MainService;
using Data.Model;
using Microsoft.AspNetCore.Mvc;
using static CoreA.Generator.ViewToString;

namespace UniverseV2.Areas.Main.Controllers
{
    [Area("Main")]
    public class MainHomeController : Controller
    {
        private readonly IMainService _mainService;
        private readonly IViewRenderService _render;
        public MainHomeController( IMainService mainService  , IViewRenderService render)
        {
            _mainService = mainService;
            _render = render;
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

            //Validation Email & Username
            if (_mainService.IsEmail(FixText.FixTexts(signUp.Email)))
            {
                ModelState.AddModelError("Email", "Is Exist");
                return View(signUp);
            }
            if (_mainService.IsUsername(FixText.FixTexts(signUp.Username)))
            {
                ModelState.AddModelError("Username", "Is Exist");
                return View(signUp);
            }

            User user = new User()
            {
                Email = FixText.FixTexts(signUp.Email),
                Username = FixText.FixTexts(signUp.Username),
                Password = HashPasswordC.EncodePasswordMd5(signUp.Password),
                IsActive = false,
                ActiveCode = CreateActiveCode.GenerateCode(),
                Picture = "",
                PictureTitle = "helloWorld.jpg",
            };



            _mainService.Add(user);
            string Body = _render.RenderToStringAsync("registerEmail", user);
            EmailSenders.Send(user.Email, "Register", Body);
            ViewBag.IsSignUp = true;
            return View();
        }

        #endregion
    }
}
