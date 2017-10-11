using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

using NorthwindWeb.Core.Models;
using NorthwindWeb.Core.ViewModels;
using System.Security.Claims;
using System.Web;
using NorthwindWeb.Core.Context;
using NorthwindWeb.Core.Models.Interfaces;
using NorthwindWeb.Models.ServerClientCommunication;
using NorthwindWeb.Models.ExceptionHandler;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NorthwindWeb.Core.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NorthwindWeb.Core.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using NorthwindWeb.Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace NorthwindWeb.Controllers
{
    /// <summary>
    /// controller for users and roles
    /// </summary>
    [Authorize]
    public class AccountController : Controller /*, IJsonTableFillServerSide */
    {
        //private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(AccountController));  //Declaring Log4Net to log errors in Event View-er in NorthwindLog Application log.


        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private readonly string _externalCookieScheme;
        private readonly NorthwindDatabase db;
        private readonly ApplicationDbContext identityContext;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<IdentityCookieOptions> identityCookieOptions,
            IEmailSender emailSender,
            ISmsSender smsSender,
            ILoggerFactory loggerFactory,
            NorthwindDatabase northwindContext,
            ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _externalCookieScheme = identityCookieOptions.Value.ExternalCookieAuthenticationScheme;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<AccountController>();
            db = northwindContext;
            identityContext = applicationDbContext;
        }


        /// <summary>
        /// Login result
        /// </summary>
        /// <param name="returnUrl">redirect to this action</param>
        /// <returns>Return view login</returns>
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.Authentication.SignOutAsync(_externalCookieScheme);

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="model">username, passwoard</param>
        /// <param name="returnUrl"> redirect to this action</param>
        /// <returns>Return view for status login</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation(1, "User logged in.");
                    return RedirectToLocal(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning(2, "User account locked out.");
                    return View("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>
        /// send data to verify
        /// </summary>
        /// <param name="provider">Provider</param>
        /// <param name="returnUrl">redirect to this action</param>
        /// <param name="rememberMe"> save passwoard or not</param>
        /// <returns>Return validation code result</returns>
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes.
            // If a user enters incorrect codes for a specified amount of time then the user account
            // will be locked out for a specified amount of time.
            var result = await _signInManager.TwoFactorSignInAsync(model.Provider, model.Code, model.RememberMe, model.RememberBrowser);
            if (result.Succeeded)
            {
                return RedirectToLocal(model.ReturnUrl);
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning(7, "User account locked out.");
                return View("Lockout");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid code.");
                return View(model);
            }
        }

        //
        // GET: /Account/AccessDenied
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        /// <summary>
        /// Verify if date is corect
        /// </summary>
        /// <param name="model">provider, code result, email, return url, save passwoard</param>
        /// <returns>Returns the view according to the validity of the code </returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return View("Error");
            }

            // Generate the token and send it
            var code = await _userManager.GenerateTwoFactorTokenAsync(user, model.SelectedProvider);
            if (string.IsNullOrWhiteSpace(code))
            {
                return View("Error");
            }

            var message = "Your security code is: " + code;
            if (model.SelectedProvider == "Email")
            {
                await _emailSender.SendEmailAsync(await _userManager.GetEmailAsync(user), "Security Code", message);
            }
            else if (model.SelectedProvider == "Phone")
            {
                await _smsSender.SendSmsAsync(await _userManager.GetPhoneNumberAsync(user), message);
            }

            return RedirectToAction(nameof(VerifyCode), new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        /// <summary>
        /// send data to create new user
        /// </summary>
        /// <returns>Return register view</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        /// <summary>
        /// create a new user
        /// </summary>
        /// <param name="model">data for new user</param>
        /// <returns>Returns a redirect to the start page if the model is valid, otherwise it displays the errors</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register([Bind("UserName,Email,Password,ConfirmPassword")]RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var currentUser = await _userManager.FindByNameAsync(user.UserName);
                    await _userManager.AddToRoleAsync(currentUser, "Guest");
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction(returnUrl);
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>
        /// send data to create new user
        /// </summary>
        /// <returns>Return Register Admin view</returns>
        [Authorize(Roles = "Admins")]
        public ActionResult RegisterAdmin()
        {
            return View();
        }

        ///// <summary>
        ///// create a new user
        ///// </summary>
        ///// <param name="model">data for new user</param>
        ///// <returns>Returns a redirect to the index page if the model is valid, otherwise it displays the errors</returns>
        //[HttpPost]
        //[Authorize(Roles = "Admins")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> RegisterAdmin([Bind("UserName,Email,Password,ConfirmPassword,UserImage")]RegisterViewModel model)
        //{
        //    try
        //    {
        //        if (!((model.UserImage == null) || model.UserImage.ContentType.Contains("image")))
        //        {
        //            throw new ArgumentException("Fisierul selectat nu este o imagine");
        //        }
        //        if (ModelState.IsValid)
        //        {
        //            ApplicationUser user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
        //            var result = await UserManager.CreateAsync(user, model.Password);
        //            if (result.Succeeded)
        //            {
        //                var currentUser = UserManager.FindByName(user.UserName);
        //                UserManager.AddToRole(currentUser.Id, "Guest");
        //                //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

        //                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
        //                // Send an email with this link
        //                // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
        //                // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
        //                // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
        //                if (model.UserImage != null)
        //                {
        //                    string path = System.IO.Path.Combine(Server.MapPath($"~/images"), $"{model.UserName}.jpg");
        //                    model.UserImage.SaveAs(path);
        //                }
        //                return RedirectToAction("Index");
        //            }
        //            AddErrors(result);
        //        }

        //        // If we got this far, something failed, redisplay form
        //        return View(model);
        //    }
        //    catch (NullReferenceException e)
        //    {
        //        logger.Error(e.ToString());
        //        throw new NullReferenceException("Imaginea nu a putut fi gasita");
        //    }
        //    catch (ArgumentException e)
        //    {
        //        logger.Error(e.ToString());
        //        throw new ArgumentException("Fisierul ales nu este o imagine");
        //    }
        //    catch (HttpRequestValidationException e)
        //    {
        //        logger.Error(e.ToString());
        //        throw new HttpRequestValidationException("Nu aveti voie sa introduceti html sau script in acest camp.");
        //    }
        //    catch (Exception e)
        //    {
        //        //if something else goes wrong
        //        logger.Error(e.ToString());
        //        throw new Exception("Ceva nu a mers bine, va rugam reincercati. Daca problema persista contactati un administrator.");
        //    }
        //}

        /// <summary>
        /// Confirm email
        /// </summary>
        /// <param name="userId">Curent user id</param>
        /// <param name="code">validation email code</param>
        /// <returns>returns validation of the email address</returns>
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        /// <summary>
        /// announce that you forgot your password
        /// </summary>
        /// <returns>Return Forgot Password view</returns>
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        /// <summary>
        /// Forgot Password
        /// </summary>
        /// <param name="model">Email to send code to reset</param>
        /// <returns>Return validation status for reguest of Forgot Password</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        /// <summary>
        /// confirm password Forgot
        /// </summary>
        /// <returns>Return Forgot Password Confirmation view</returns>
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        /// <summary>
        /// result reset password
        /// </summary>
        /// <param name="code">code you received for reset</param>
        /// <returns>Return if the code is correct or not</returns>
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        /// <summary>
        /// reset password
        /// </summary>
        /// <param name="model">Email, password, confirm password, result</param>
        /// <returns>if there is an error or a mismatch in the model to return to this page, otherwise it redirects you to the confirmation page</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction(nameof(AccountController.ResetPasswordConfirmation), "Account");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(AccountController.ResetPasswordConfirmation), "Account");
            }
            AddErrors(result);
            return View();
        }

        /// <summary>
        /// confirm password reset
        /// </summary>
        /// <returns>Return Reset Password Confirmation view</returns>
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult ExternalLogin(string provider, string returnUrl)
        //{
        //    // Request a redirect to the external login provider.
        //    var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { ReturnUrl = returnUrl });
        //    var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        //    return Challenge(properties, provider);
        //}


        /// <summary>
        /// get sendcode
        /// </summary>
        /// <param name="returnUrl">redirect to this action</param>
        /// <param name="rememberMe">if you want to save your password</param>
        /// <returns>Returns send code view</returns>
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe = false)
        {
            var userId = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await _userManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }


        /// <summary>
        /// result for external logging
        /// </summary>
        /// <param name="returnUrl">redirect to this action</param>
        /// <returns>Return external login result</returns>
        //[AllowAnonymous]
        //public async Task<ActionResult> ExternalLoginCallback(string returnUrl, string remoteError = null)
        //{
        //    if (remoteError != null)
        //    {
        //        ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
        //        return View(nameof(Login));
        //    }
        //    var info = await _signInManager.GetExternalLoginInfoAsync();
        //    if (info == null)
        //    {
        //        return RedirectToAction(nameof(Login));
        //    }

        //    // Sign in the user with this external login provider if the user already has a login.
        //    var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
        //    if (result.Succeeded)
        //    {
        //        _logger.LogInformation(5, "User logged in with {Name} provider.", info.LoginProvider);
        //        return RedirectToLocal(returnUrl);
        //    }
        //    if (result.RequiresTwoFactor)
        //    {
        //        return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl });
        //    }
        //    if (result.IsLockedOut)
        //    {
        //        return View("Lockout");
        //    }
        //    else
        //    {
        //        // If the user does not have an account, then ask the user to create an account.
        //        ViewData["ReturnUrl"] = returnUrl;
        //        ViewData["LoginProvider"] = info.LoginProvider;
        //        var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        //        return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = email });
        //    }
        //}


        /// <summary>
        /// ExternalLoginConfirmation
        /// </summary>
        /// <param name="model">data for external logging</param>
        /// <param name="returnUrl">redirect to this action</param>
        /// <returns>redirects you to the start page if your login was successful otherwise it displays the errors</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        _logger.LogInformation(6, "User created an account using {Name} provider.", info.LoginProvider);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(model);
        }


        /// <summary>
        /// LogOff
        /// </summary>
        /// <returns>Redirect to the start page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> LogOffAsync()
        {
            // check if this is log off sau log out(probabil este asa)
            await _signInManager.SignOutAsync();
            _logger.LogInformation(4, "User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }


        /// <summary>
        /// ExternalLoginFailure
        /// </summary>
        /// <returns>Return External Login Failure view</returns>
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }
        //-------------------------------------------------------------------------manage users account and roles-----------------------------------------
        ///// <summary>
        ///// Index of account
        ///// </summary>
        ///// <returns>Returns Index view</returns>
        //[Authorize(Roles = "Admins")]
        //public ActionResult Index()
        //{

        //    return View();
        //}
        /// <summary>
        /// Index of account
        /// </summary>
        /// <param name="status">Change user status</param>
        /// <returns>Returns Index view</returns>
        [Authorize(Roles = "Admins")]
        public ActionResult Index(string status)
        {
            if (!String.IsNullOrEmpty(status))
            {
                ViewBag.Status = status;

            }
            return View();
        }

        /// <summary>
        /// data to change user
        /// </summary>
        /// <param name="userName">identify the user with this username</param>
        /// <returns>Returns Change User view</returns>
        [Authorize(Roles = "Admins")]
        public async Task<ActionResult> ChangeUser(string userName)
        {
            RegisterViewModel model = new RegisterViewModel();
            try
            {
                ApplicationUser user = null;
                if (!String.IsNullOrEmpty(userName))
                {
                    user = await _userManager.FindByNameAsync(userName);
                }
                else
                {
                    user = await _userManager.FindByNameAsync(ViewBag.UserName);
                }
                model.Email = user.Email;
                model.UserName = user.UserName;
                //model.OldUserName = user.UserName;
                ViewBag.UserName = user.UserName;

            }
            catch (Exception exception)
            {
                //todo add logger
                //logger.Error(exception.ToString());
                throw exception;
            }
            return View(model);
        }
        /// <summary>
        /// Change User
        /// </summary>
        /// <param name="model">User for change</param>
        /// <returns>Returns to index if succes else returns to this page</returns>
        [HttpPost]
        [Authorize(Roles = "Admins")]
        public async Task<ActionResult> ChangeUser([Bind("UserName,Email,Password,ConfirmPassword,UserImage")]RegisterViewModel model)
        {
            try
            {
                //todo check if this is good (contains(image))
                if (!(model.UserName == null))
                {
                    throw new ArgumentException("Fisierul selectat nu este o imagine");
                }

                if (!ModelState.IsValid)
                {
                    ViewBag.UserName = model.UserName;
                    return View(model);
                }

                var user = await _userManager.FindByNameAsync(model.UserName);
                await _userManager.ChangePasswordAsync(user, model.ConfirmPassword, model.Password);
                user.UserName = model.UserName;
                user.Email = model.Email;


                if (await _userManager.IsInRoleAsync(user, "Customers"))
                {
                    var customers = db.Customers.Where(c => c.ContactName == model.UserName).FirstOrDefault();
                    if (customers != null)
                        customers.ContactName = user.UserName;
                    db.Entry(customers).State = EntityState.Modified;
                    db.SaveChanges();

                }

                await _userManager.UpdateAsync(user);
                

                if (model.UserImage != null)
                {
                    string path = System.IO.Path.Combine(Startup._hostingEnvironment + "\\images\\", $"{model.UserName}.jpg");
                    //todo verifica daca sunt necesate \\ de la images\\
                    System.IO.File.Delete(path);

                    foreach (var formFile in model.UserImage)
                    {
                        if (formFile.Length > 0)
                        {
                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await formFile.CopyToAsync(stream);
                            }
                        }
                    }
                }
                else
                {
                    string path = System.IO.Path.Combine(Startup._hostingEnvironment + "\\images\\", $"{model.UserName}.jpg");
                    System.IO.File.Move(System.IO.Path.Combine(path, $"{model.UserName}.jpg"), System.IO.Path.Combine(path, $"{user.UserName}.jpg"));
                }
                return RedirectToAction("Index", new { status = "Schimbarile sau efectuat" });


            }
            //todo in bellow catch log the error
            catch (NullReferenceException)
            {
                //logger.Error(e.ToString());
                throw new NullReferenceException("Imaginea nu a putut fi gasita");
            }
            catch (DbUpdateException e)
            {
                //logger.Error(e.ToString());
                throw new DbUpdateException("Nu s-au putut efectua modificatile", e.InnerException);
            }
            catch (ArgumentException)
            {
                //logger.Error(e.ToString());
                throw new ArgumentException("Fisierul ales nu este o imagine");
            }
            catch (Exception)
            {
                //logger.Error(exception.ToString());
                return RedirectToAction("Index", new { status = "Schimbarile nu sau putut efectua" });
            }



            // If we got this far, something failed, redisplay form
            //ViewBag.PasswordLength = UserManager.PasswordValidator.


        }
        /// <summary>
        /// delete user
        /// </summary>
        /// <param name="userName">deletes the user with this username</param>
        /// <returns>Redirect to index page</returns>
        [Authorize(Roles = "Admins")]
        public async Task<ActionResult> Delete(string userName)
        {
            IdentityResult isDeleted = IdentityResult.Failed() ;

            string curentUser = User.Identity.Name;
            ApplicationUser user = await _userManager.FindByNameAsync(userName);

            if (!String.IsNullOrEmpty(userName))
            {
                if (user.UserName == curentUser) { await LogOffAsync(); }
                isDeleted = await _userManager.DeleteAsync(user);
            }

            if (isDeleted.Succeeded)
            {
                System.IO.File.Delete(System.IO.Path.Combine(Startup._hostingEnvironment + "\\images\\", $"{userName}.jpg"));
            }

            //    return RedirectToAction("Index", "Home");
            //else
            return RedirectToAction("Index");

        }
        /// <summary>
        /// view user to delete
        /// </summary>
        /// <param name="userName">Curent username</param>
        /// <returns>Returns details for user delete</returns>
        [Authorize(Roles = "Admins")]
        public async Task<IActionResult> DeleteUser(string userName)
        {
            //ApplicationDbContext context = new ApplicationDbContext();
            ////var userStore = new UserStore<ApplicationUser>(context);
            //var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            UserInfoViewModel userDelete = new UserInfoViewModel();
            var result = await _userManager.FindByNameAsync(userName);
            if (!String.IsNullOrEmpty(userName) && result != null)
            {
                userDelete.UserName = result.UserName;
                userDelete.Email = result.Email;

            }
            return View(userDelete);
        }
        /// <summary>
        /// View list of role
        /// </summary>
        /// <returns>Returns to RoleIndex view</returns>        
        [Authorize(Roles = "Admins")]
        public ActionResult RolesIndex(RoleManager<IdentityRole> roleManager)
        {
            List<RoleInfoViewModel> roleInfoViewModel = new List<RoleInfoViewModel>();

            foreach (var role in roleManager.Roles)
                roleInfoViewModel.Add(new RoleInfoViewModel()
                {
                    Name = role.Name
                });

            return View("RolesIndex", roleInfoViewModel.AsQueryable());

        }
        /// <summary>
        /// name a new role
        /// </summary>
        /// <returns>Returns create role view</returns>
        [Authorize]
        public ActionResult CreateRole()
        {
            return View();
        }

        #region No Role Creation Dinamicaly
        /// <summary>
        /// create a new role
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <returns>Returns create role error if status is not succes, if status is succes Redirect to the RoleIndex page</returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreateRole(RoleInfoViewModel roleInfo, RoleManager<IdentityRole> roleManager)
        {
            IdentityResult isCreated = null;
            ApplicationDbContext context = new ApplicationDbContext();

            using (context)
            {
                if (! await roleManager.RoleExistsAsync(roleInfo.Name))
                {

                    // first we create Admin rool   
                    var role = new IdentityRole();
                    role.Name = roleInfo.Name;

                    isCreated = await roleManager.CreateAsync(role);

                    if (ViewData.Keys.Contains("RoleExists")) ViewData.Remove("RoleExists");

                    ViewData["Success"] = "Role Created";
                }
                else
                    ViewData["RoleExists"] = "Un rol cu acest nume deja exista!";
            }

            if (isCreated.Succeeded)
                return RedirectToAction("RolesIndex");
            else
                return RedirectToAction("Index", "Home");
        }
        #endregion
        /// <summary>
        /// delete role
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns>Redirect to RoleIndex if succes, else display error message</returns>
        [Authorize]
        public async Task<IActionResult> RoleDelete(string roleName, RoleManager<IdentityRole> roleManager)
        {
            List<SelectListItem> selectItemsUserInRole = new List<SelectListItem>();
            ApplicationDbContext context = new ApplicationDbContext();
            List<ApplicationUser> users = _userManager.Users.ToList();

            foreach (ApplicationUser user in users)
            {

                if (await _userManager.IsInRoleAsync(user, roleName))
                    selectItemsUserInRole.Add(new SelectListItem() { Text = user.UserName, Value = user.UserName, Selected = false });



                //userManager.Dispose();
            }
            if (selectItemsUserInRole.Count() == 0)
            {
                IdentityResult isDeleted = IdentityResult.Failed();

                using (context)
                {

                    if (await roleManager.RoleExistsAsync(roleName))
                    {
                        IdentityRole role = await roleManager.FindByNameAsync(roleName);
                        isDeleted = await roleManager.DeleteAsync(role);
                    }
                }

                if (isDeleted.Succeeded)
                    return RedirectToAction("RolesIndex");
                else
                    return RedirectToAction("Index", "Home");
            }
            else
            {
                string error = "Nu puteti sterge roluri cat timp au utilizatori alocati: ";
                foreach (SelectListItem user in selectItemsUserInRole)
                {
                    error += user.Text + " ";
                }
                //todo add logger
                //logger.Error(error.ToString());
                throw new DeleteException(error);
            }
        }
        /// <summary>
        /// user assingned to role
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns>Return UserInRole view</returns>
        public async Task<IActionResult> UsersInRole(string roleName)
        {
            List<UserInfoViewModel> userInfoViewModel = new List<UserInfoViewModel>();

            foreach (var user in _userManager.Users)
            {

                if (await _userManager.IsInRoleAsync(user, roleName))
                    userInfoViewModel.Add(new UserInfoViewModel()
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        //LastActiveDateTime = user.LastActivityDate,
                        //IsOnline = user.IsOnline,
                        //IsLockedOut = user.IsLockedOut
                    });
            }

            return View(userInfoViewModel.AsQueryable());
        }

        /// <summary>
        /// select and additing user to role
        /// </summary>
        /// <param name="roleInfo">Role name</param>
        /// <returns>Returns RoleMembership view after the selected user has been assigned the role</returns>
        [HttpPost]
        public async Task<IActionResult> RoleMembership(RoleInfoViewModel roleInfo)
        {
            var roleName = Request.Query["name"];
            var userName = Request.Form["UserList"];
            var currentUser = await _userManager.FindByNameAsync(userName);
            if (!string.IsNullOrEmpty(userName) && ! await _userManager.IsInRoleAsync(currentUser, roleName))
                return await AddUsersToRole();

            //Roles.IsUserInRole(userName, roleName)
            //return View(roleInfo);
            //return View(new RoleInfoModel() { Name = roleName });

            return await RoleMembership();
        }
        /// <summary>
        /// user in role
        /// </summary>
        /// <returns>Returns RoleMembership view </returns>
        [HttpGet]
        public async Task<IActionResult> RoleMembership()
        {
            //string roleName = Request["roleName"] != null ? Request["roleName"].ToString() : Roles.GetRolesForUser(User.Identity.Name)[0];
            string roleName = String.IsNullOrEmpty(HttpContext.Request.Query["roleName"]) ? HttpContext.Request.Query["roleName"].ToString() : "Admins";
            //var usersInRole = from C in Roles.GetUsersInRole(roleName).AsQueryable() select C.ToLower();

            //var allUsers = Membership.GetAllUsers();
            List<SelectListItem> selectItemsUserNotInRole = new List<SelectListItem>();
            //List<SelectListItem> selectItemsUserInRole = new List<SelectListItem>();
            List<SelectListItem> selectItemsAllUser = new List<SelectListItem>();

            var context = new ApplicationDbContext();

            List<ApplicationUser> users = _userManager.Users.ToList();

            foreach (ApplicationUser user in users)
            {

                if (! await  _userManager.IsInRoleAsync(user, roleName))
                    selectItemsUserNotInRole.Add(new SelectListItem() { Text = user.UserName, Value = user.UserName, Selected = false });
                //else
                //    selectItemsUserInRole.Add(new SelectListItem() { Text = user.UserName, Value = user.UserName, Selected = false });

                selectItemsAllUser.Add(new SelectListItem() { Text = user.UserName, Value = user.UserName, Selected = false });

                //userManager.Dispose();
            }


            ViewData["UsersNotInRole"] = selectItemsUserNotInRole;
            ViewData["AllUsers"] = selectItemsAllUser;
            //ViewData["UsersInRole"] = selectItemsUserInRole;

            return View(new RoleInfoViewModel() { Name = roleName });
        }
        /// <summary>
        /// add user to role
        /// </summary>
        /// <returns>Redirect to RoleMembership page</returns>        
        [Authorize(Roles = "Admins")]
        public async Task<ActionResult> AddUsersToRole()
        {
            string roleName = (string)Request.Query["roleName"];
            string userNameList = (string)Request.Query["UserList"];


            //Roles.AddUserToRole(userName, roleName);

            var context = new ApplicationDbContext();

            try
            {
                foreach (string userName in new string[] { userNameList })
                {
                    var user = await _userManager.FindByNameAsync(userName);
                    await _userManager.AddToRoleAsync(user, roleName);
                }

                context.SaveChanges();
            }
            catch (Exception exception)
            {
                //todo logger
                //logger.Error(exception.ToString());
                return View();

            }

            return RedirectToAction("RoleMembership", new { name = roleName, roleName = roleName });

            //var usersInRole = from C in Roles.GetUsersInRole(roleName).AsQueryable() select C.ToLower();
            //var allUsers = Membership.GetAllUsers();
            //List<SelectListItem> selectItems = new List<SelectListItem>();
            //foreach (MembershipUser user in allUsers)
            //{
            //    if (!usersInRole.Contains(user.UserName.ToLower()))
            //        selectItems.Add(new SelectListItem() { Text = user.UserName, Value = user.UserName, Selected = false });
            //}

            //ViewData["UsersNotInRole"] = selectItems;
            ////return View();
            //return View("RoleMembership",new RoleInfoModel() { Name = roleName });
        }
        /// <summary>
        /// debunk the user from the role
        /// </summary>
        /// <returns>Debunk user selected from role and redirect to RoleMembership page</returns>
        [HttpGet]
        [Authorize(Roles = "Admins")]
        public async Task<IActionResult> DeleteFromRole()
        {
            var roleName = Request.Query["roleName"];
            var userName = Request.Query["userName"];

            //var account = new AccountController();
            var context = new ApplicationDbContext();
            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            if (await _userManager.IsInRoleAsync(user, roleName))
            {
                await _userManager.RemoveFromRoleAsync(user, roleName);
                ViewBag.ResultMessage = "Role removed from this user successfully !";
            }
            else
            {
                ViewBag.ResultMessage = "This user doesn't belong to selected role.";
            }
            // prepopulat roles for the view dropdown
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            return RedirectToAction("RoleMembership", new { name = roleName, roleName = roleName });
        }

        /// <summary>
        /// Message status
        /// </summary> 
        public enum ManageMessageId
        {    /// <summary>
             /// Add Phone Success
             /// </summary>
            AddPhoneSuccess,
            /// <summary>
            /// Change Password Success
            /// </summary>
            ChangePasswordSuccess,
            /// <summary>
            /// Set Two Factor Success
            /// </summary>
            SetTwoFactorSuccess,
            /// <summary>
            /// Set Password Success
            /// </summary>
            SetPasswordSuccess,
            /// <summary>
            /// Remove Login Success
            /// </summary>
            RemoveLoginSuccess,
            /// <summary>
            /// Remove Phone Success
            /// </summary>
            RemovePhoneSuccess,
            /// <summary>
            /// Error
            /// </summary>
            Error
        }

        //todo decomenteaza tot ce este mai jos intre /**/ si interfata clasei
        /*
        /// <summary>
        /// Change users password
        /// </summary>
        /// <param name="message">Status</param>
        /// <returns>Return view manage</returns>
        [Authorize]
        public async Task<ActionResult> Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Parola dvs. a fost schimbata."
                : message == ManageMessageId.SetPasswordSuccess ? "Parola dvs. a fost setata."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = User.Identity.IsAuthenticated;

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;


            ViewBag.ReturnUrl = Url.Action("Manage");

            return View();

            //return View(new ManageLoginsViewModel
            //{
            //    CurrentLogins = userLogins,
            //    OtherLogins = otherLogins
            //});
        }
        /// <summary>
        ///  Manage users password
        /// </summary>
        /// <param name="model">old and new password</param>
        /// <returns>return status of manage</returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Manage(ChangePasswordViewModel model)
        {
            bool hasLocalAccount = User.Identity.IsAuthenticated;
            ViewBag.HasLocalPassword = hasLocalAccount;
            ViewBag.ReturnUrl = Url.Action("Manage");

            if (hasLocalAccount)
            {
                if (ModelState.IsValid)
                {
                    var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                        if (user != null)
                        {
                            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        }
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                    }
                }
            }
            else
            {
                // User does not have a local password so remove any validation errors caused by a missing
                // OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

            }

            // ViewBag.PasswordLength = MembershipService.MinPasswordLength;

            return View(model);
        }

        /// <summary>
        /// Assigns user to role Customers
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult AssignCustomers()
        {
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null) UserManager.AddToRole(user.Id, "Customers");
            if (UserManager.IsInRole(user.Id, "Guest"))
            {
                UserManager.RemoveFromRole(user.Id, "Guest");
            }
            return RedirectToAction("ConfirmOrder", "ShopCart");
        }


        /// <summary>
        /// Return a list of users to complete table
        /// </summary>
        /// <param name="draw">Draw order. Client send a draw id in request to keep track of asyncron response</param>
        /// <param name="start">Start from this item</param>
        /// <param name="length">Take a list with "lenght" (if exists) objects inside.</param>
        /// <returns>Returns json for datatable with users</returns>        
        public JsonResult JsonTableFill(int draw, int start, int length)
        {
            const int TOTAL_ROWS = 999;


            string search = Request.QueryString["search[value]"] ?? "";
            int sortColumn = -1;
            string sortDirection = "asc";
            if (length == -1)
            {
                length = TOTAL_ROWS;
            }

            // note: we only sort one column at a time
            if (Request.QueryString["order[0][column]"] != null)
            {
                sortColumn = int.Parse(Request.QueryString["order[0][column]"]);
            }
            if (Request.QueryString["order[0][dir]"] != null)
            {
                sortDirection = Request.QueryString["order[0][dir]"];
            }


            var context = new ApplicationDbContext();

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            List<UserInfoViewModel> userInfoViewModel = new List<UserInfoViewModel>();
            foreach (var user in userManager.Users)
                userInfoViewModel.Add(new UserInfoViewModel()
                {
                    UserName = user.UserName,
                    Email = user.Email
                    //LastActiveDateTime = user.LastActivityDate,
                    //IsOnline = user.IsOnline,
                    //IsLockedOut = user.IsLockedOut
                });
            foreach (var user in userInfoViewModel)
            {

                user.LastActiveDateTime = Convert.ToDateTime(String.Format("{0:g}", user.LastActiveDateTime));
                user.LastActiveString = Convert.ToString(user.LastActiveDateTime);
                //user.IsLockedOut = user.IsLockedOut ? true : false;
                user.IsOnline = user.IsOnline ? true : false;
            }

            string searchBool = "bool";
            if ("no".Contains(search)) { searchBool = "false"; }
            else if ("yes".Contains(search)) { searchBool = "true"; }
            userInfoViewModel = userInfoViewModel.Where(u => (u.UserName.ToLower().Contains(search.ToLower()) || u.Email.ToLower().Contains(search.ToLower()) || Convert.ToString(u.LastActiveDateTime).ToLower().Contains(search.ToLower())
            || Convert.ToString(u.IsLockedOut).ToLower().Contains(searchBool.ToLower()) || Convert.ToString(u.IsOnline).ToLower().Contains(searchBool.ToLower()))).ToList();


            //order list
            switch (sortColumn)
            {
                case -1: //sort by first column
                    goto Username;
                case 2: //Username column
                    Username:
                    if (sortDirection == "asc")
                    {
                        userInfoViewModel = userInfoViewModel.OrderBy(x => x.UserName).ToList();
                    }
                    else
                    {
                        userInfoViewModel = userInfoViewModel.OrderByDescending(x => x.UserName).ToList();
                    }
                    break;
                case 3: //Email column
                    if (sortDirection == "asc")
                    {
                        userInfoViewModel = userInfoViewModel.OrderBy(x => x.Email).ToList();
                    }
                    else
                    {
                        userInfoViewModel = userInfoViewModel.OrderByDescending(x => x.Email).ToList();
                    }
                    break;
                case 4: // LastActiveDateTime column
                    if (sortDirection == "asc")
                    {
                        userInfoViewModel = userInfoViewModel.OrderBy(x => x.LastActiveDateTime).ToList();
                    }
                    else
                    {
                        userInfoViewModel = userInfoViewModel.OrderByDescending(x => x.LastActiveDateTime).ToList();
                    }
                    break;
                //case 4:
                //    if (sortDirection == "asc")
                //    {
                //        userInfoViewModel = userInfoViewModel.OrderBy(x => x.IsLockedOut).ToList();
                //    }
                //    else
                //    {
                //        userInfoViewModel = userInfoViewModel.OrderByDescending(x => x.IsLockedOut).ToList();
                //    }
                //    break;
                case 5:// IsOnline column
                    if (sortDirection == "asc")
                    {
                        userInfoViewModel = userInfoViewModel.OrderBy(x => x.IsOnline).ToList();
                    }
                    else
                    {
                        userInfoViewModel = userInfoViewModel.OrderByDescending(x => x.IsOnline).ToList();
                    }
                    break;

            }
            //objet that whill be sent to client
            JsonDataTableUserList dataTableData = new JsonDataTableUserList()
            {
                draw = draw,
                recordsTotal = userManager.Users.Count(),
                data = new List<UserInfoViewModel>(),
                recordsFiltered = userInfoViewModel.Count(), //need to be below data(ref recordsFiltered)
            };
            foreach (var itemUserInfoViewModel in userInfoViewModel.Skip(start).Take(length))
            {
                UserInfoViewModel userInfo = new UserInfoViewModel();
                userInfo.UserName = itemUserInfoViewModel.UserName;
                userInfo.Email = itemUserInfoViewModel.Email;
                userInfo.LastActiveDateTime = itemUserInfoViewModel.LastActiveDateTime;
                userInfo.LastActiveString = itemUserInfoViewModel.LastActiveString;
                //userInfo.IsLockedOut = itemUserInfoViewModel.IsLockedOut;
                userInfo.IsOnline = itemUserInfoViewModel.IsOnline;
                dataTableData.data.Add(userInfo);
            }
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Return a list a roles to complete table
        /// </summary>
        /// <param name="draw">Draw order. Client send a draw id in request to keep track of asyncron response</param>
        /// <param name="start">Start from this item</param>
        /// <param name="length">Take a list with "lenght" (if exists) objects inside.</param>
        /// <returns>Returns json for datatable with role</returns>        
        public JsonResult JsonTableRolesFill(int draw, int start, int length)
        {
            const int TOTAL_ROWS = 999;


            string search = Request.QueryString["search[value]"] ?? "";
            int sortColumn = -1;
            string sortDirection = "asc";
            if (length == -1)
            {
                length = TOTAL_ROWS;
            }

            // note: we only sort one column at a time
            if (Request.QueryString["order[0][column]"] != null)
            {
                sortColumn = int.Parse(Request.QueryString["order[0][column]"]);
            }
            if (Request.QueryString["order[0][dir]"] != null)
            {
                sortDirection = Request.QueryString["order[0][dir]"];
            }


            var context = new ApplicationDbContext();
            //var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            List<RoleInfoViewModel> roleInfoViewModel = new List<RoleInfoViewModel>();

            foreach (var role in roleManager.Roles)
                roleInfoViewModel.Add(new RoleInfoViewModel()
                {
                    Name = role.Name
                });

            roleInfoViewModel = roleInfoViewModel.Where(r => r.Name.ToLower().Contains(search.ToLower())).ToList();


            //order list
            switch (sortColumn)
            {
                case -1: //sort by first column
                    goto FirstColumn;
                case 1: //first column
                    FirstColumn:
                    if (sortDirection == "asc")
                    {
                        roleInfoViewModel = roleInfoViewModel.OrderBy(x => x.Name).ToList();
                    }
                    else
                    {
                        roleInfoViewModel = roleInfoViewModel.OrderByDescending(x => x.Name).ToList();
                    }
                    break;


            }
            //objet that whill be sent to client
            JsonDataTableRoleList dataTableData = new JsonDataTableRoleList()
            {
                draw = draw,
                recordsTotal = roleManager.Roles.Count(),
                data = new List<RoleInfoViewModel>(),

                recordsFiltered = roleInfoViewModel.Count(), //need to be below data(ref recordsFiltered)
            };
            foreach (var role in roleInfoViewModel.Skip(start).Take(length))
            {
                RoleInfoViewModel roleInfo = new RoleInfoViewModel();
                roleInfo.Name = role.Name;
                dataTableData.data.Add(roleInfo);
            }
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Returns a list of users assigned to role
        /// </summary>
        /// <param name="draw">Draw order. Client send a draw id in request to keep track of asyncron response</param>
        /// <param name="start">Start from this item</param>
        /// <param name="length">Take a list with "lenght" (if exists) objects inside.</param>
        /// <returns>Returns json for datatable with users assigned to role</returns>
        public JsonResult JsonTableMembershipFill(int draw, int start, int length)
        {

            const int TOTAL_ROWS = 999;


            string search = Request.QueryString["search[value]"] ?? "";
            int sortColumn = -1;
            string sortDirection = "asc";
            if (length == -1)
            {
                length = TOTAL_ROWS;
            }

            // note: we only sort one column at a time
            if (Request.QueryString["order[0][column]"] != null)
            {
                sortColumn = int.Parse(Request.QueryString["order[0][column]"]);
            }
            if (Request.QueryString["order[0][dir]"] != null)
            {
                sortDirection = Request.QueryString["order[0][dir]"];
            }


            string roleName = HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["roleName"] != null ? HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["roleName"].ToString() : "Admins";



            List<UserInfoViewModel> selectItemsUserInRole = new List<UserInfoViewModel>();


            var context = new ApplicationDbContext();

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            List<ApplicationUser> users = userManager.Users.ToList();

            foreach (ApplicationUser user in users)
            {

                if (userManager.IsInRole(user.Id, roleName))

                    selectItemsUserInRole.Add(new UserInfoViewModel() { UserName = user.UserName });


            }


            var numberUsersInRole = selectItemsUserInRole.Count();
            selectItemsUserInRole = selectItemsUserInRole.Where(r => r.UserName.ToLower().Contains(search.ToLower())).ToList();


            //order list
            switch (sortColumn)
            {
                case -1: //sort by first column
                    goto FirstColumn;
                case 1: //first column
                    FirstColumn:
                    if (sortDirection == "asc")
                    {
                        selectItemsUserInRole = selectItemsUserInRole.OrderBy(x => x.UserName).ToList();
                    }
                    else
                    {
                        selectItemsUserInRole = selectItemsUserInRole.OrderByDescending(x => x.UserName).ToList();
                    }
                    break;


            }
            //objet that whill be sent to client
            JsonDataTableUserList dataTableData = new JsonDataTableUserList()
            {
                draw = draw,
                recordsTotal = numberUsersInRole,
                data = new List<UserInfoViewModel>(),

                recordsFiltered = selectItemsUserInRole.Count(), //need to be below data(ref recordsFiltered)
                roleName = roleName,
            };
            foreach (var userInRole in selectItemsUserInRole.Skip(start).Take(length))
            {
                UserInfoViewModel user = new UserInfoViewModel();
                user.UserName = userInRole.UserName;
                dataTableData.data.Add(user);
            }
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }
        */

        #region Helpers
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        #endregion

    }

    /// <summary>
    /// Return a list of roles.
    /// </summary>
    public static class Extentions
    {
        /// <summary>
        /// List of roles.
        /// </summary>
        /// <param name="identity">User identity</param>
        /// <returns>Returns roles</returns>
        public static List<string> Roles(this ClaimsIdentity identity)
        {
            return identity.Claims
                           .Where(c => c.Type == ClaimTypes.Role)
                           .Select(c => c.Value)
                           .ToList();
        }
    }

}