using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BudgetWepApp.Models;

namespace BudgetWepApp.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public ViewResult AccessDenied(){
            return View();
            }

        [HttpGet]
        public IActionResult LogIn(string returnURL = "")
        {
            var model = new LoginViewModel { returnUrl = returnURL };
            return View(model);
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(SignupViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.UserName };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                    model.UserName, model.Password, isPersistent: model.rememberMe, lockoutOnFailure: false);
                var user = await userManager.FindByNameAsync(model.UserName);

                        // Username or password is incorrect.
                        if (user == null)
                        {
                            ModelState.AddModelError("", "user.");
                            // Username is incorrect.
                        }
                        else
                        {
                            ModelState.AddModelError("", model.UserName + " " + model.Password);
                            // Password is incorrect.
                        }
                    
       
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.returnUrl) &&
                        Url.IsLocalUrl(model.returnUrl))
                    {
                        return Redirect(model.returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid username, password, or email.");
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
