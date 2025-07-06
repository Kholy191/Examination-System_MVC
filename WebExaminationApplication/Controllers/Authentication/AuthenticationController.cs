using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using ServicesAbstraction;
using ServicesAbstraction.CoreServices;
using Shared.Dtos.Authentication;
using WebExaminationApplication.ViewModels.Authentication;

namespace WebExaminationApplication.Controllers.Authentication
{
    public class AuthenticationController : Controller
    {
        readonly IAuthenticationService _services;
        public AuthenticationController(IAuthenticationService services)
        {
            _services = services;
        }

        #region Register
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(RegisterViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var UserDto = new RegisterDto()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
                UserType = user.UserType,
            };
            var Result = await _services.SignUpAsync(UserDto);
            if (Result == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", Result);
            }
            return View(user);

        }
        #endregion

        #region SignIn
        [AllowAnonymous]
        [HttpGet]
        public IActionResult SignIn()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                    return RedirectToAction("AdminViewModel");
                else if (User.IsInRole("Student"))
                    return RedirectToAction("StudentViewPanel");
                else if (User.IsInRole("Instructor"))
                    return RedirectToAction("InstructorViewPanel");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            var userDto = new LoginDto()
            {
                Email = user.Email,
                Password = user.Password
            };
            var Result = await _services.SignInAsync(userDto);
            if (Result == null)
            {
                if (User.IsInRole("Admin"))
                    return RedirectToAction("AdminViewModel");
                else if (User.IsInRole("Student"))
                    return RedirectToAction("StudentViewPanel");
                else if (User.IsInRole("Instructor"))
                    return RedirectToAction("InstructorViewPanel");
            }
            else
            {
                ModelState.AddModelError("", Result);
            }
            return View(user);
        }
        #endregion

        #region SignOut
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await _services.SignOutAsync();
            return RedirectToAction("SignIn");
        }

        #endregion

        #region Homes
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminViewModel()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Student")]
        public IActionResult StudentViewPanel()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Instructor")]
        public IActionResult InstructorViewPanel()
        {
            return View();
        }
        #endregion
    }
}
