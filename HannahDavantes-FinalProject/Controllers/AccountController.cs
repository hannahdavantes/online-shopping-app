using HannahDavantes_FinalProject.Data.Utilities;
using HannahDavantes_FinalProject.Data.ViewModel;
using HannahDavantes_FinalProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HannahDavantes_FinalProject.Controllers {
    public class AccountController : Controller {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly DbContextUtility _context;


        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, DbContextUtility context) {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult SignIn() {
            var signInVM = new SignInViewModel();
            return View(signInVM);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel signInVM) {
            if (!ModelState.IsValid) {
                return View(signInVM);
            } else {
                var user = await _userManager.FindByEmailAsync(signInVM.EmailAddress);
                if (user != null) {
                    var validPassword = await _userManager.CheckPasswordAsync(user, signInVM.Password);
                    if (validPassword) {
                        var result = await _signInManager.PasswordSignInAsync(user, signInVM.Password, false, false);
                        if (result.Succeeded) {
                            return RedirectToAction("Index", "Products");
                        } else {
                            TempData["ErrorMessage"] = "Invalid Email/Password";
                            return View(signInVM);
                        }
                    } else {
                        TempData["ErrorMessage"] = "Invalid Email/Password";
                        return View(signInVM);
                    }
                } else {
                    TempData["ErrorMessage"] = "Invalid Email/Password";
                    return View(signInVM);
                }
            }
        }
    }
}
