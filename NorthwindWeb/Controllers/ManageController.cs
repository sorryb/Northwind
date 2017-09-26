using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using NorthwindWeb.Models;
using NorthwindWeb.ViewModels;

namespace NorthwindWeb.Controllers
{
    /// <summary>
    /// Contains all the methods used to change the user's information.
    /// </summary>
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(ManageController));  //Declaring Log4Net to log errors in Event View-er in NorthwindLog Application log.
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ManageController()
        {

        }

        /// <summary>
        /// Initialises userManager and signInManager.
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        
        /// <summary>
        /// Getter/Setter
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
        /// Getter/Setter
        /// </summary>
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

        /// <summary>
        /// Displays a page with links to change various account information. (password, phone number)
        /// </summary>
        /// <param name="message">A message to show.</param>
        /// <returns>Manage index view</returns>
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Parola va fost schimbata"
                : message == ManageMessageId.SetPasswordSuccess ? "Parola va fost setata"
                : message == ManageMessageId.ChangeEmailSuccess ? "Adresa de email va fost schimbata"
                : message == ManageMessageId.SetEmailSuccess ? "Adresa de email va fost setata"
                : message == ManageMessageId.ChangeProfilePhotoSuccess ? "Poza de profil va fost schimbata"
                : message == ManageMessageId.SetProfilePhotoSuccess ? "Poza de profil va fost setata"
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "A aparut o eroare"
                : message == ManageMessageId.AddPhoneSuccess ? "Numarul dumneavoastra de telefon a fost schimbat"
                : message == ManageMessageId.RemovePhoneSuccess ? "Numarul dumneavoastra de telefon a fost setat"
                : "";

            var userId = User.Identity.GetUserId();
            ViewBag.Username = User.Identity.GetUserName();
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };
            return View(model);
        }

        /// <summary>
        /// Removes an external login from this user.
        /// </summary>
        /// <param name="loginProvider">The provider that sent the external login link.</param>
        /// <param name="providerKey"></param>
        /// <returns>Manage ManageLogins view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        /// <summary>
        /// Displays a page containing a form neccessary to add a phone number to an existing user.
        /// </summary>
        /// <returns>Manage AddPhoneNumber view</returns>
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        
        /// <summary>
        /// Displays a page containing a form neccessary to add a phone number to an existing user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Generate the token and send it
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };
                await UserManager.SmsService.SendAsync(message);
            }
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        /// <summary>
        /// Enables two factor authentication
        /// </summary>
        /// <returns>Manage index view.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        /// <summary>
        /// Disables two factor authentication
        /// </summary>
        /// <returns>Manage index view.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        /// <summary>
        /// Sends an sms containing a security code to the phonenumber
        /// </summary>
        /// <param name="phoneNumber">The number that will receive the sms.</param>
        /// <returns>Manage VerifyPhoneNumber view.</returns>
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        /// <summary>
        /// Verifies if the security code matches the number it was sent to.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Manage index view.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");
            return View(model);
        }

        /// <summary>
        /// Removes the current user's phone number.
        /// </summary>
        /// <returns>Manage index view.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        /// <summary>
        /// Displays a page containing a form neccessary to change the password for the curent user.
        /// </summary>
        /// <returns>Manage ChangePassword view</returns>
        public ActionResult ChangePassword()
        {
            return View();
        }

        /// <summary>
        /// Changes the password of the current user if the old password was entered correctly.
        /// </summary>
        /// <param name="model">The class containing the neccessary information.</param>
        /// <returns>Manage index view.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }
        /// <summary>
        /// Displays a page containing a form neccessary to change the email for the curent user.
        /// </summary>
        /// <returns>Manage ChangeEmail view</returns>
        public ActionResult ChangeEmail()
        {
            return View();
        }

        /// <summary>
        /// Changes the email of the current user if the password was entered correctly.
        /// </summary>
        /// <param name="model">The class containing the neccessary information.</param>
        /// <returns>Manage index view.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeEmail(ChangeEmailViewModel model)
        {
            IdentityResult isChanged = new IdentityResult("Nu s-a putut modifica!");
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var verifyUser = await UserManager.FindAsync(User.Identity.Name, model.Password);
            
            if (verifyUser!=null)
            {
                verifyUser.Email = model.Email;
                isChanged = UserManager.Update(verifyUser);
                if (isChanged.Succeeded)
                {
                    return RedirectToAction("Index", new { Message = ManageMessageId.ChangeEmailSuccess });
                }
                AddErrors(isChanged);
            }
            else
            {
            ModelState.AddModelError("Parola actuala", "Parola nu este corecta");
            }
           
            return View(model);
        }
        /// <summary>
        /// Displays a page containing a form neccessary to change the profile photo for the curent user.
        /// </summary>
        /// <returns>Manage ChangeEmail view</returns>
        public ActionResult ChangeImage()
        {
            return View();
        }

        /// <summary>
        /// Changes the profile photo of the current user if the password was entered correctly.
        /// </summary>
        /// <param name="model">The class containing the neccessary information.</param>
        /// <returns>Manage index view.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeImage(ChangeProfileViewModel model)
        {
            try
            {
                if (!((model.UserImage == null) || model.UserImage.ContentType.Contains("image")))
                {
                    throw new ArgumentException("Fisierul selectat nu este o imagine");
                }
                IdentityResult isChanged = new IdentityResult("Nu s-a putut modifica!");
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var verifyUser = await UserManager.FindAsync(User.Identity.Name, model.Password);

                if (verifyUser != null)
                {
                    System.IO.File.Delete(System.IO.Path.Combine(Server.MapPath($"~/images"), $"{verifyUser.UserName}.jpg"));
                    if (model.UserImage != null)
                    {
                        string path = System.IO.Path.Combine(Server.MapPath($"~/images"), $"{verifyUser.UserName}.jpg");
                        model.UserImage.SaveAs(path);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.ChangeProfilePhotoSuccess });

                }
                ModelState.AddModelError("Parola actuala", "Parola nu este corecta");

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
            catch (Exception e)
            {
                //if something else goes wrong
                logger.Error(e.ToString());
                throw new Exception("Ceva nu a mers bine, va rugam reincercati. Daca problema persista contactati un administrator.");
            }
        }

        /// <summary>
        /// Displays a page containing a form neccessary to set a new password.
        /// </summary>
        /// <returns>Manage setPassword view.</returns>
        public ActionResult SetPassword()
        {
            return View();
        }

        /// <summary>
        /// Sets the password for the current user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Manage index view.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>
        /// Manages local and external logins.
        /// </summary>
        /// <param name="message"></param>
        /// <returns>Manage ManageLogins view</returns>
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        /// <summary>
        /// Requests an external login link from a provider.
        /// </summary>
        /// <param name="provider">The provider that will receive the request.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        /// <summary>
        /// Logs in the user using the external login link.
        /// </summary>
        /// <returns>Manage ManageLogins view.</returns>
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
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

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        //public object Index()
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// Contains the possible messages that a Manage page can display.
        /// </summary>
        public enum ManageMessageId
        {
            /// <summary>
            /// 
            /// </summary>
            AddPhoneSuccess,
            /// <summary>
            /// 
            /// </summary>
            ChangePasswordSuccess,
            /// <summary>
            /// 
            /// </summary>
            ChangeEmailSuccess,
            /// <summary>
            /// 
            /// </summary>
            ChangeProfilePhotoSuccess,
            /// <summary>
            /// 
            /// </summary>
            SetTwoFactorSuccess,
            /// <summary>
            /// 
            /// </summary>
            SetPasswordSuccess,
            /// <summary>
            /// 
            /// </summary>
            SetEmailSuccess,
            /// <summary>
            /// 
            /// </summary>
            SetProfilePhotoSuccess,
            /// <summary>
            /// 
            /// </summary>
            RemoveLoginSuccess,
            /// <summary>
            /// 
            /// </summary>
            RemovePhoneSuccess,
            /// <summary>
            /// 
            /// </summary>
            Error
        }

#endregion
    }
}