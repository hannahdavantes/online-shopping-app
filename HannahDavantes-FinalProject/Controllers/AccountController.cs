using HannahDavantes_FinalProject.Data.Utilities;
using HannahDavantes_FinalProject.Data.ViewModel;
using HannahDavantes_FinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


namespace HannahDavantes_FinalProject.Controllers {
    /// <summary>
    /// This class represents the controller for Account related actions of the application
    /// </summary>
    public class AccountController : Controller {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly DbContextUtility _context;

        /// <summary>
        /// Constructor that injects the User Manager, SignIn Manager and DbContext services
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="context"></param>
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, DbContextUtility context) {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        /// <summary>
        /// This method will return the page that shows the list of users of the application and their details
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public IActionResult Users() {
            var users = _context.Users.ToList();

            return View(users);
        }

        /// <summary>
        /// This method will return the page that shows the Sign In Page where users can sign in
        /// </summary>
        /// <returns></returns>
        public IActionResult Login() {
            var signInVM = new SignInViewModel();
            return View(signInVM);
        }

        /// <summary>
        /// This method will send the request to the User Manager service to get if the user exists.
        /// If the user exists, the Products will be shown
        /// If the user doesn't exist, the user will be redirected to the login page
        /// </summary>
        /// <param name="signInVM"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(SignInViewModel signInVM) {
            if (!ModelState.IsValid) {
                return View(signInVM);
            } else {
                //Check if user exist on the database based on the email provided
                var user = await _userManager.FindByEmailAsync(signInVM.EmailAddress);
                if (user != null) {
                    //Check if the password match the email that was provided
                    var validPassword = await _userManager.CheckPasswordAsync(user, signInVM.Password);
                    if (validPassword) {
                        //This will sign in the user into the application
                        var result = await _signInManager.PasswordSignInAsync(user, signInVM.Password, false, false);
                        if (result.Succeeded) {
                            return RedirectToAction("Index", "Products");
                        } else {
                            TempData["ErrorMessage"] = "Something went wrong";
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

        /// <summary>
        /// This method will return the page where the user can register their account
        /// </summary>
        /// <returns></returns>
        public IActionResult Register() {
            var registerVM = new RegisterViewModel();
            return View(registerVM);
        }

        /// <summary>
        /// This method will send a request to the database and create the account if user's email has not been used yet
        /// </summary>
        /// <param name="registerViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel) {
            //Input validation
            if (!ModelState.IsValid) {
                return View(registerViewModel);
            } else {
                //Check if the user email has already been used - if yes then he will be redirected to the sign in page
                var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
                if (user != null) {
                    TempData["ErrorMessage"] = "Email Address is already taken";
                    return View(registerViewModel);
                } else {
                    var newUser = new User() {
                        FirstName = registerViewModel.FirstName,
                        LastName = registerViewModel.LastName,
                        Email = registerViewModel.EmailAddress,
                        UserName = registerViewModel.FirstName
                    };
                    //This will add user to the database
                    var result = await _userManager.CreateAsync(newUser, registerViewModel.Password);
                    if (result.Succeeded) {
                        await _userManager.AddToRoleAsync(newUser, Roles.USER);
                        TempData["SuccessMessage"] = "You can now login!";
                        return RedirectToAction(nameof(SignIn));
                    } else {
                        Debug.WriteLine(result.Errors);
                        TempData["ErrorMessage"] = "Something went wrong. Please try again.";
                        return View(registerViewModel);
                    }
                }
            }
        }

        /// <summary>
        /// This method sign out the user from the application
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SignOut() {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// This method will show the page when a specific page cannot be accessed by the user
        /// </summary>
        /// <returns></returns>
        public IActionResult AccessDenied() {
            return View();
        }
    }
}
