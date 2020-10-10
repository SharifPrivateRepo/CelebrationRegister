using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CelebrationRegister.Core.Services.Interfaces;
using CelebrationRegister.Core.ViewModels.AccountViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace CelebrationRegister.Web.Controllers
{
    public class AccountController : Controller
    {
        #region Injection

        private IUserServices _userServices;

        public AccountController(IUserServices userServices)
        {
            _userServices = userServices;
        }


        #endregion


        #region Login

        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/UserPanel");
            }

            return View();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(LoginViewModel login, string ReturnUrl = "/")
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }


            var employee = await _userServices.LoginEmployeeAsync(login);
            if (employee != null)
            {

                var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,employee.EmployeeId.ToString()),
                        new Claim(ClaimTypes.Name,employee.ProsonnelCode)
                    };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                var properties = new AuthenticationProperties
                {
                    IsPersistent = login.RememberMe
                };

                await HttpContext.SignInAsync(principal, properties);

                ViewBag.IsSuccess = true;

                if (ReturnUrl != "/")
                {
                    return Redirect(ReturnUrl);
                }
                return Redirect("/UserPanel");

            }
            ModelState.AddModelError("Email", "کاربری با مشخصات وارد شده یافت نشد");
            return View(login);
        }

        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        #endregion
    }
}
