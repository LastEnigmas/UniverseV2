using CoreA.DTOs.MainDTOs;
using CoreA.Generator;
using CoreA.Security;
using CoreA.Sender;
using CoreA.Services.MainService;
using Data.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static CoreA.Generator.ViewToString;
using Microsoft.AspNetCore.Authorization;
using System.Drawing;

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

        #region Test_Section

        [Authorize]
        public IActionResult Hello() => View();

        #endregion

        #region SignIn
        public IActionResult SignIn() => View();

        [HttpPost]
        public IActionResult SignIn(SignInViewModel signIn )
        {
            User user = _mainService.FindUserByUsernameOrEmail(signIn);
            if(user != null)
            {
                if (user.IsActive)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name , user.Username),
                        new Claim(ClaimTypes.Email, user.Email),
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties()
                    {
                        IsPersistent = true ,
                    };

                    HttpContext.SignInAsync(principal, properties);

                    ViewBag.IsSignIn = true;
                    return View();
                }
                else
                {
                    ModelState.AddModelError("UsernameOrEmail", "Please Active Your Account First");
                }
            }
            else
            {
                ModelState.AddModelError("UsernameOrEmail", "Username Or Email Or Password Invalid");
                return View(signIn);
            }

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

        #region Register

        public IActionResult Register(string id)
        {
            RegisterViewModel register = _mainService.RegisterUser(id);
            if(register != null)
            {
                ViewBag.IsRegister = true;
                return View(register);
            }
            else
            {
                return NotFound();
            }
        }

        #endregion

        #region Forgot_Pass


        [Route("ForgotPassword")]
        public IActionResult ForgotPassword() => View();

        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword(ForgotPasswordViewModel forgot )
        {
            ViewBag.IsSend = _mainService.ForgotPasswordTask(forgot);
            return View();
        }

        #endregion

        #region SignOut

        [Authorize]
        [Route("SignOut")]
        public IActionResult SignOut_User()
        {
            SignOutViewModel signOut = new SignOutViewModel();
            signOut.AreYouSure = false;
            return View(signOut);
        }

        #endregion

        #region Reset Pasword

        public IActionResult ResetPassword(string id )
        {
            return View( new ResetPasswordViewModel()
            {
                ActiveCode = id ,
            });
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel resetPassword)
        {

            User user = _mainService.GetUserByActiveCode(resetPassword.ActiveCode);
            if(user != null)
            {
                string HashPassword = HashPasswordC.EncodePasswordMd5(resetPassword.Password);
                user.Password = HashPassword;
                user.ActiveCode = CreateActiveCode.GenerateCode();

                _mainService.Update(user);
                return RedirectToAction("SignIn");
            }
            else
            {
                return NotFound();
            }
            return View();
        }

        #endregion
    }
}
