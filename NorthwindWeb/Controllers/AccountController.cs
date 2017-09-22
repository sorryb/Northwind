using System;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

using NorthwindWeb.Models;
using NorthwindWeb.ViewModels;
using System.Security.Claims;
using System.Web;
using NorthwindWeb.Context;
using NorthwindWeb.Models.Interfaces;
using NorthwindWeb.Models.ServerClientCommunication;
using NorthwindWeb.Models.ExceptionHandler;
using System.Data.Entity;

namespace NorthwindWeb.Controllers
{
    /// <summary>
    /// controller for users and roles
    /// </summary>
    [Authorize]
    public class AccountController : Controller, IJsonTableFillServerSide
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(AccountController));  //Declaring Log4Net to log errors in Event View-er in NorthwindLog Application log.
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        /// <summary>
        ///  constructor with managed empty
        /// </summary>
        public AccountController()
        {
        }

        /// <summary>
        /// constructor with managed set
        /// </summary>
        /// <param name="userManager">User manager</param>
        /// <param name="signInManager">sign in manager</param>
        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        /// <summary>
        ///  sign in manager
        /// </summary>
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        /// <summary>
        /// User manager
        /// </summary>
        public ApplicationUserManager UserManager
        {
            get
            {
                //var manager = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<UserManager<ApplicationUser>>();
                return _userManager ?? System.Web.HttpContext.Current.Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        /// <summary>
        /// Login result
        /// </summary>
        /// <param name="returnUrl">redirect to this action</param>
        /// <returns>Return view login</returns>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
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
        [ValidateInput(false)]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            model.UserName = HttpUtility.HtmlEncode(model.UserName);
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
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
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        /// <summary>
        /// Verify if date is corect
        /// </summary>
        /// <param name="model">provider, code result, email, return url, save passwoard</param>
        /// <returns>Returns the view according to the validity of the code </returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        /// <summary>
        /// send data to create new user
        /// </summary>
        /// <returns>Return register view</returns>
        [AllowAnonymous]
        public ActionResult Register()
        {
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
        public async Task<ActionResult> Register([Bind(Include = "UserName,Email,Password,ConfirmPassword")]RegisterViewModel model)
        {
            model.UserName = HttpUtility.HtmlEncode(model.UserName);
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var currentUser = UserManager.FindByName(user.UserName);
                    UserManager.AddToRole(currentUser.Id, "Guest");
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
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

        /// <summary>
        /// create a new user
        /// </summary>
        /// <param name="model">data for new user</param>
        /// <returns>Returns a redirect to the index page if the model is valid, otherwise it displays the errors</returns>
        [HttpPost]
        [Authorize(Roles = "Admins")]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> RegisterAdmin([Bind(Include = "UserName,Email,Password,ConfirmPassword,UserImage")]RegisterViewModel model)
        {
            try
            {
                model.UserName = HttpUtility.HtmlEncode(model.UserName);
                if (!((model.UserImage == null) || model.UserImage.ContentType.Contains("image")))
                {
                    throw new ArgumentException("Fisierul selectat nu este o imagine");
                }
                if (ModelState.IsValid)
                {
                    ApplicationUser user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        var currentUser = UserManager.FindByName(user.UserName);
                        UserManager.AddToRole(currentUser.Id, "Guest");
                        //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                        if (model.UserImage != null)
                        {
                            string path = System.IO.Path.Combine(Server.MapPath($"~/images"), $"{model.UserName}.jpg");
                            model.UserImage.SaveAs(path);
                        }
                        return RedirectToAction("Index");
                    }
                    AddErrors(result);
                }

                // If we got this far, something failed, redisplay form
                return View(model);
            }
            catch (NullReferenceException e)
            {
                logger.Error(e.ToString());
                throw new NullReferenceException("Imaginea nu a putut fi gasita");
            }
            catch (ArgumentException e)
            {
                logger.Error(e.ToString());
                throw new ArgumentException("Fisierul ales nu este o imagine");
            }
            catch (HttpRequestValidationException e)
            {
                logger.Error(e.ToString());
                throw new HttpRequestValidationException("Nu aveti voie sa introduceti html sau script in acest camp.");
            }
            catch (Exception e)
            {
                //if something else goes wrong
                logger.Error(e.ToString());
                throw new Exception("Ceva nu a mers bine, va rugam reincercati. Daca problema persista contactati un administrator.");
            }
        }

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
            var result = await UserManager.ConfirmEmailAsync(userId, code);
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
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
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
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
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

        ////
        //// POST: /Account/ExternalLogin
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult ExternalLogin(string provider, string returnUrl)
        //{
        //    // Request a redirect to the external login provider
        //    return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        //}


        /// <summary>
        /// get sendcode
        /// </summary>
        /// <param name="returnUrl">redirect to this action</param>
        /// <param name="rememberMe">if you want to save your password</param>
        /// <returns>Returns send code view</returns>
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        /// <summary>
        /// code with the data of a new user
        /// </summary>
        /// <param name="model">data of a new user</param>
        /// <returns>returns an error or Verify code</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }
        /// <summary>
        /// result for external logging
        /// </summary>
        /// <param name="returnUrl">redirect to this action</param>
        /// <returns>Return external login result</returns>
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }
        /// <summary>
        /// ExternalLoginConfirmation
        /// </summary>
        /// <param name="model">data for external logging</param>
        /// <param name="returnUrl">redirect to this action</param>
        /// <returns>redirects you to the start page if your login was successful otherwise it displays the errors</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }
        /// <summary>
        /// LogOff
        /// </summary>
        /// <returns>Redirect to the start page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult LogOff()
        {
            if (System.Web.HttpContext.Current != null)
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
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
                    user = await UserManager.FindByNameAsync(userName);
                }
                else
                {
                    user = await UserManager.FindByNameAsync(ViewBag.UserName);
                }
                model.Email = user.Email;
                model.UserName = user.UserName;
                //model.OldUserName = user.UserName;
                ViewBag.UserName = user.UserName;

            }
            catch (Exception exception)
            {
                logger.Error(exception.ToString());

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
        public async Task<ActionResult> ChangeUser([Bind(Include = "UserName,Email,Password,ConfirmPassword,UserImage")]RegisterViewModel model)
        {
            try
            {
                if (!((model.UserImage == null) || model.UserImage.ContentType.Contains("image")))
                {
                    throw new ArgumentException("Fisierul selectat nu este o imagine");
                }
                IdentityResult isChanged = new IdentityResult("Nu s-a putut modifica!");
                string userName = Request["Name"];
                if (!ModelState.IsValid)
                {
                    ViewBag.UserName = userName;
                    return View(model);
                }
                var user = await UserManager.FindByNameAsync(userName);
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.PasswordHash = UserManager.PasswordHasher.HashPassword(model.Password);
                if (UserManager.IsInRole(user.Id, "Customers"))
                {
                    NorthwindDatabase db = new NorthwindDatabase();
                    var customers = db.Customers.Where(c => c.ContactName == userName).FirstOrDefault();
                    if (customers != null)
                        customers.ContactName = user.UserName;
                    db.Entry(customers).State = EntityState.Modified;
                    db.Dispose();

                }

                isChanged = UserManager.Update(user);
                if (model.UserImage != null)
                {
                    System.IO.File.Delete(System.IO.Path.Combine(Server.MapPath($"~/images"), $"{userName}.jpg"));
                    string path = System.IO.Path.Combine(Server.MapPath($"~/images"), $"{model.UserName}.jpg");
                    model.UserImage.SaveAs(path);
                }
                else
                {
                    System.IO.File.Move(System.IO.Path.Combine(Server.MapPath($"~/images"), $"{userName}.jpg"), System.IO.Path.Combine(Server.MapPath($"~/images"), $"{user.UserName}.jpg"));
                }
                return RedirectToAction("Index", new { status = "Schimbarile sau efectuat" });


            }
            catch (NullReferenceException e)
            {
                logger.Error(e.ToString());
                throw new NullReferenceException("Imaginea nu a putut fi gasita");
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                logger.Error(e.ToString());
                throw new System.Data.Entity.Infrastructure.DbUpdateException("Nu s-au putut efectua modificatile");
            }
            catch (ArgumentException e)
            {
                logger.Error(e.ToString());
                throw new ArgumentException("Fisierul ales nu este o imagine");
            }
            catch (Exception exception)
            {
                logger.Error(exception.ToString());

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
            IdentityResult isDeleted = new IdentityResult("Nu s-a putut sterge!");

            string curentUser = User.Identity.GetUserName();
            ApplicationUser user = await UserManager.FindByNameAsync(userName);

            if (!String.IsNullOrEmpty(userName))
            {
                if (user.UserName == curentUser) { LogOff(); }
                isDeleted = UserManager.Delete(user);
            }

            if (isDeleted.Succeeded)

                System.IO.File.Delete(System.IO.Path.Combine(Server.MapPath($"~/images"), $"{userName}.jpg"));

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
        public ActionResult DeleteUser(string userName)
        {
            //ApplicationDbContext context = new ApplicationDbContext();
            ////var userStore = new UserStore<ApplicationUser>(context);
            //var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            UserInfoViewModel userDelete = new UserInfoViewModel();

            if (!String.IsNullOrEmpty(userName) && UserManager.FindByName(userName) != null)
            {
                userDelete.UserName = UserManager.FindByName(userName).UserName;
                userDelete.Email = UserManager.FindByName(userName).Email;

            }
            return View(userDelete);
        }
        /// <summary>
        /// View list of role
        /// </summary>
        /// <returns>Returns to RoleIndex view</returns>        
        [Authorize(Roles = "Admins")]
        public ActionResult RolesIndex()
        {

            //var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
            var roleStore = new RoleStore<IdentityRole>(System.Web.HttpContext.Current.GetOwinContext().Get<ApplicationDbContext>());
            var roleManager = new RoleManager<IdentityRole>(roleStore);

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
        public ActionResult CreateRole(RoleInfoViewModel roleInfo)
        {
            IdentityResult isCreated = null;
            ApplicationDbContext context = new ApplicationDbContext();

            using (context)
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                if (!roleManager.RoleExists(roleInfo.Name))
                {

                    // first we create Admin rool   
                    var role = new IdentityRole();
                    role.Name = roleInfo.Name;

                    isCreated = roleManager.Create(role);

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
        public ActionResult RoleDelete(string roleName)
        {
            List<SelectListItem> selectItemsUserInRole = new List<SelectListItem>();
            ApplicationDbContext context = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            List<ApplicationUser> users = userManager.Users.ToList();

            foreach (ApplicationUser user in users)
            {

                if (userManager.IsInRole(user.Id, roleName))
                    selectItemsUserInRole.Add(new SelectListItem() { Text = user.UserName, Value = user.UserName, Selected = false });



                //userManager.Dispose();
            }
            if (selectItemsUserInRole.Count() == 0)
            {
                IdentityResult isDeleted = new IdentityResult("Nu s-a putut sterge!");


                using (context)
                {
                    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                    if (roleManager.RoleExists(roleName))
                    {
                        IdentityRole role = roleManager.FindByName(roleName);
                        isDeleted = roleManager.Delete(role);
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
                logger.Error(error.ToString());
                throw new DeleteException(error);
            }
        }
        /// <summary>
        /// user assingned to role
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns>Return UserInRole view</returns>
        public ActionResult UsersInRole(string roleName)
        {
            var context = new ApplicationDbContext();
            //var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            List<UserInfoViewModel> userInfoViewModel = new List<UserInfoViewModel>();

            foreach (var user in userManager.Users)
            {

                if (userManager.IsInRole(user.Id, roleName))
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
        public ActionResult RoleMembership(RoleInfoViewModel roleInfo)
        {
            var roleName = Request["name"];
            var userName = Request.Form["UserList"];
            var userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var currentUser = userManager.FindByName(userName);
            if (!string.IsNullOrEmpty(userName) && !userManager.IsInRole(currentUser.Id, roleName))
                return AddUsersToRole();

            //Roles.IsUserInRole(userName, roleName)
            //return View(roleInfo);
            //return View(new RoleInfoModel() { Name = roleName });

            return RoleMembership();
        }
        /// <summary>
        /// user in role
        /// </summary>
        /// <returns>Returns RoleMembership view </returns>
        [HttpGet]
        public ActionResult RoleMembership()
        {
            //string roleName = Request["roleName"] != null ? Request["roleName"].ToString() : Roles.GetRolesForUser(User.Identity.Name)[0];
            string roleName = Request["roleName"] != null ? Request["roleName"].ToString() : "Admins";
            //var usersInRole = from C in Roles.GetUsersInRole(roleName).AsQueryable() select C.ToLower();

            //var allUsers = Membership.GetAllUsers();
            List<SelectListItem> selectItemsUserNotInRole = new List<SelectListItem>();
            //List<SelectListItem> selectItemsUserInRole = new List<SelectListItem>();
            List<SelectListItem> selectItemsAllUser = new List<SelectListItem>();

            var context = new ApplicationDbContext();

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            List<ApplicationUser> users = userManager.Users.ToList();

            foreach (ApplicationUser user in users)
            {

                if (!userManager.IsInRole(user.Id, roleName))
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
        public ActionResult AddUsersToRole()
        {
            string roleName = (string)Request["roleName"];
            string userNameList = (string)Request["UserList"];


            //Roles.AddUserToRole(userName, roleName);

            var context = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            try
            {
                foreach (string userName in new string[] { userNameList })
                {
                    var user = userManager.FindByName(userName);
                    userManager.AddToRole(user.Id, roleName);
                }

                context.SaveChanges();
            }
            catch (Exception exception)
            {
                logger.Error(exception.ToString());
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
        public ActionResult DeleteFromRole()
        {
            var roleName = Request["roleName"];
            var userName = Request["userName"];

            //var account = new AccountController();
            var context = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            if (userManager.IsInRole(user.Id, roleName))
            {
                userManager.RemoveFromRole(user.Id, roleName);
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

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
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

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        /// <summary>
        ///Dispose connection
        /// </summary>
        #endregion
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }
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