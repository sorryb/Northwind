using System;
using System.Globalization;
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

namespace NorthwindWeb.Controllers
{
    [Authorize]
    public class AccountController : Controller, IJsonTableFill
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

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

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
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

        //
        // GET: /Account/VerifyCode
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

        //
        // POST: /Account/VerifyCode
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

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
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

        //
        // GET: /Account/ConfirmEmail
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

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
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

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
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

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
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

        //
        // POST: /Account/SendCode
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

        //
        // GET: /Account/ExternalLoginCallback
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

        //
        // POST: /Account/ExternalLoginConfirmation
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

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }
        //-------------------------------------------------------------------------manage users account and roles-----------------------------------------

        [Authorize(Roles = "Admins")]
        public ActionResult Index()
        {
            var context = new ApplicationDbContext();
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            List<UserInfoViewModel> userInfoViewModel = new List<UserInfoViewModel>();
            foreach (var user in userManager.Users)
                userInfoViewModel.Add(new UserInfoViewModel()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    //LastActiveDateTime = user.LastActivityDate,
                    //IsOnline = user.IsOnline,
                    //IsLockedOut = user.IsLockedOut
                });
            return View(userInfoViewModel.AsQueryable());
        }

        // **************************************
        // URL: /Account/ChangeUser
        // **************************************
        [Authorize]
        public async Task<ActionResult> ChangeUser(string userName)
        {
            RegisterViewModel model = new RegisterViewModel();

            var user = await UserManager.FindByNameAsync(userName);

            model.Email = user.Email;
            model.UserName = user.UserName;

            ViewBag.UserName = user.UserName;

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ChangeUser(RegisterViewModel model)
        {
            IdentityResult isChanged = new IdentityResult("Nu s-a putut modifica!");

            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.UserName);


                isChanged = UserManager.Update(user);

                if (isChanged.Succeeded)
                    return RedirectToAction("Index", "Home");
                else
                    return RedirectToAction("Index");

            }

            // If we got this far, something failed, redisplay form
            //ViewBag.PasswordLength = UserManager.PasswordValidator.

            return View(model);
        }

        [Authorize(Roles = "Admins")]
        public async Task<ActionResult> Delete(string userName)
        {
            IdentityResult isDeleted = new IdentityResult("Nu s-a putut sterge!");

            var user = await UserManager.FindByNameAsync(userName);

            if (!String.IsNullOrEmpty(userName))
                isDeleted = UserManager.Delete(user);


            if (isDeleted.Succeeded)
                return RedirectToAction("Index", "Home");
            else
                return RedirectToAction("Index");

        }

        [Authorize(Roles = "Admins")]
        public ActionResult DeleteUser(string userName)
        {
            var context = new ApplicationDbContext();
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            UserInfoViewModel userDelete = new UserInfoViewModel();
            if (!String.IsNullOrEmpty(userName))
            {
                userDelete.UserName=userManager.Users.First().UserName;
                userDelete.Email = userManager.Users.First().Email;
               
            }
            return View(userDelete);
        }

        [Authorize(Roles = "Admins")]
        public ActionResult RolesIndex()
        {

            var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            List<RoleInfoViewModel> roleInfoViewModel = new List<RoleInfoViewModel>();

            foreach (var role in roleManager.Roles)
                roleInfoViewModel.Add(new RoleInfoViewModel()
                {
                    Name = role.Name
                });

            return View("RolesIndex", roleInfoViewModel.AsQueryable());

        }

        [Authorize]
        public ActionResult CreateRole()
        {
            return View();
        }

        #region No Role Creation Dinamicaly
        //[Authorize]
        //[HttpPost]
        //public ActionResult CreateRole(RoleInfoViewModel roleInfo)
        //{
        //    IdentityResult isCreated = null;
        //    ApplicationDbContext context = new ApplicationDbContext();

        //    using (context)
        //    {
        //        var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

        //        if (!roleManager.RoleExists(roleInfo.Name))
        //        {

        //            // first we create Admin rool   
        //            var role = new IdentityRole();
        //            role.Name = roleInfo.Name;

        //            isCreated = roleManager.Create(role);

        //            if (ViewData.Keys.Contains("RoleExists")) ViewData.Remove("RoleExists");

        //            ViewData["Success"] = "Role Created";
        //        }
        //        else
        //            ViewData["RoleExists"] = "Un rol cu acest nume deja exista!";
        //    }

        //    if (isCreated.Succeeded)
        //        return RedirectToAction("RolesIndex");
        //    else
        //        return RedirectToAction("Index", "Home");
        //} 
        #endregion

        //[Authorize]
        //public ActionResult RoleDelete(string roleName)
        //{
        //    IdentityResult isDeleted = new IdentityResult("Nu s-a putut sterge!");
        //    ApplicationDbContext context = new ApplicationDbContext();

        //    using (context)
        //    {
        //        var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

        //        if (roleManager.RoleExists(roleName))
        //        {
        //            IdentityRole role = roleManager.FindByName(roleName);
        //            isDeleted = roleManager.Delete(role);
        //        }
        //    }

        //    if (isDeleted.Succeeded)
        //        return RedirectToAction("RolesIndex");
        //    else
        //        return RedirectToAction("Index", "Home");
        //}


        public ActionResult UsersInRole(string roleName)
        {
            var context = new ApplicationDbContext();
            var userStore = new UserStore<ApplicationUser>(context);
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

        [HttpPost]
        public ActionResult RoleMembership(RoleInfoViewModel roleInfo)
        {
            //var roleName = Request["name"];
            //var userName = Request.Form["UserList"];
            //if (!string.IsNullOrEmpty(userName) && !Roles.IsUserInRole(userName, roleName))
            //    Roles.AddUsersToRole(new string[] { userName }, roleName);


            ////return View(roleInfo);
            ////return View(new RoleInfoModel() { Name = roleName });
            return RoleMembership();
        }

        //[Authorize(Roles = "Admins")]
        [HttpGet]
        public ActionResult RoleMembership()
        {
            //string roleName = Request["roleName"] != null ? Request["roleName"].ToString() : Roles.GetRolesForUser(User.Identity.Name)[0];
            string roleName = Request["roleName"] != null ? Request["roleName"].ToString() : "Admins";
            //var usersInRole = from C in Roles.GetUsersInRole(roleName).AsQueryable() select C.ToLower();

            //var allUsers = Membership.GetAllUsers();
            List<SelectListItem> selectItemsUserNotInRole = new List<SelectListItem>();
            List<SelectListItem> selectItemsUserInRole = new List<SelectListItem>();
            List<SelectListItem> selectItemsAllUser = new List<SelectListItem>();

            var context = new ApplicationDbContext();

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            List<ApplicationUser> users = userManager.Users.ToList();

            foreach (ApplicationUser user in users)
            {

                if (!userManager.IsInRole(user.Id, roleName))
                    selectItemsUserNotInRole.Add(new SelectListItem() { Text = user.UserName, Value = user.UserName, Selected = false });
                else
                    selectItemsUserInRole.Add(new SelectListItem() { Text = user.UserName, Value = user.UserName, Selected = false });

                selectItemsAllUser.Add(new SelectListItem() { Text = user.UserName, Value = user.UserName, Selected = false });

                //userManager.Dispose();
            }


            ViewData["UsersNotInRole"] = selectItemsUserNotInRole;
            ViewData["AllUsers"] = selectItemsAllUser;
            ViewData["UsersInRole"] = selectItemsUserInRole;

            return View(new RoleInfoViewModel() { Name = roleName });
        }

        [Authorize(Roles = "Admins")]
        public ActionResult AddUsersToRole()
        {
            string roleName = (string)Request["roleName"];
            string userNameList = (string)Request["UserList"];


            //Roles.AddUserToRole(userName, roleName);

            var context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            try
            {
                foreach (string userName in new string[] { userNameList })
                {
                    var user = UserManager.FindByName(userName);
                    UserManager.AddToRole(user.Id, roleName);
                }

                context.SaveChanges();
            }
            catch
            {
                throw;
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

        [HttpGet]
        [Authorize(Roles = "Admins")]
        public ActionResult DeleteFromRole()
        {
            var roleName = Request["roleName"];
            var userName = Request["userName"];

            var account = new AccountController();
            var context = new ApplicationDbContext();

            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            if (account.UserManager.IsInRole(user.Id, roleName))
            {
                account.UserManager.RemoveFromRole(user.Id, roleName);
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


        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        //
        // GET: /Account/Manage
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
        public JsonResult JsonTableFill(string search = "")
        {
            var context = new ApplicationDbContext();
            var userStore = new UserStore<ApplicationUser>(context);
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
                user.LastActiveString = user.LastActiveDateTime.ToUniversalTime().ToString();
                user.IsLockedOut = user.IsLockedOut ? true : false;
                user.IsOnline = user.IsOnline ? true : false;
            }


            /*Select what wee need in table*/
            return Json(
                userInfoViewModel.AsQueryable()
                
                , JsonRequestBehavior.AllowGet);
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
        #endregion
    }

    /// <summary>
    /// Return a list of roles.
    /// </summary>
    public static class Extentions
    {
        public static List<string> Roles(this ClaimsIdentity identity)
        {
            return identity.Claims
                           .Where(c => c.Type == ClaimTypes.Role)
                           .Select(c => c.Value)
                           .ToList();
        }
    }

}