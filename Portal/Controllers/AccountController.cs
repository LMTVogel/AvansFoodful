using Microsoft.AspNetCore.Mvc;
using Portal.Models;
using Core.DomainServices.Repos.Intf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Portal.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IStudentRepo _studentRepo;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IStudentRepo studentRepo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _studentRepo = studentRepo;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginVM.Email);

                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    if ((await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false)).Succeeded)
                    {
                        return RedirectToAction("Index", "Home", new { response = "login" });
                    }
                }
            }

            ModelState.AddModelError("", "Incorrecte email of wachtwoord");
            return View();
        }

        [Authorize(Roles = "Student")]
        public IActionResult Reservations(string email)
        {
            if (email == null)
            {
                return View();
            }
            return View(_studentRepo.GetStudentReservations(email));
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { response = "logout" });
        }
    }
}