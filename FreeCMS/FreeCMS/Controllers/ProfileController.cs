using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using FreeCMS.DomainModels.Identity;
using FreeCMS.ViewModels.Profile;
using FreeCMS.Service.Filters;

namespace FreeCMS.Controllers
{
    [Authorize]
    public class ProfileController:Controller
    {
        #region variables ...
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly IFileChecker _imageChecker;
        private readonly IEmailSender _emailSender;
        #endregion

        #region constructors ...
        public ProfileController(IHttpContextAccessor httpContextAccessor,IEmailSender emailSender, UserManager<ApplicationUser> userManager)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._userManager = userManager;
            //_imageChecker = FileCheckerFactory.GetDefaultImageChecker();
            this._emailSender = emailSender;
        }
        #endregion

        #region actions ...
        public IActionResult Index()
        {
            var userId = _httpContextAccessor.HttpContext.User.GetId();
            if(userId == null)
            {
                return NotFound();
            }
            var user = _userManager.FindByIdAsync(userId.ToString()).Result;
            var userInfo = new UserInfoVm(user);
            return View(userInfo);
        }
        public IActionResult Edit()
        {
            var userId = _httpContextAccessor.HttpContext.User.GetId();
            if(userId == null)
            {
                return NotFound();
            }
            var user = _userManager.FindByIdAsync(userId.ToString()).Result;
            var userInfo = new EditUserInfoVm(user);
            return View(userInfo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditUserInfoVm model)
        {
            if(ModelState.IsValid)
            {
                var userId = _httpContextAccessor.HttpContext.User.GetId();
                if(userId == null)
                {
                    return NotFound();
                }
                var user = _userManager.FindByIdAsync(userId.ToString()).Result;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                var result = _userManager.UpdateAsync(user).Result;
                if(result.Succeeded)
                {
                    return RedirectToAction("Index","Profile",new{area=""});
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(ChangePasswordVm model)
        {
            if(ModelState.IsValid)
            {
                var userId = _httpContextAccessor.HttpContext.User.GetId();
                if(userId == null)
                {
                    return NotFound();
                }
                var user = _userManager.FindByIdAsync(userId.ToString()).Result;
                var result = _userManager.ChangePasswordAsync(user,model.CurrentPassword,model.NewPassword).Result;
                if(result.Succeeded)
                {
                    return RedirectToAction("Index","Profile",new{area=""});
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        public IActionResult ChangeImage()
        {
            ViewBag.ProfileImageSize = 500;
            return View();
        }
        
        public IActionResult ChangeEmail()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeEmail(ChangeEmailVm model)
        {
            if(ModelState.IsValid)
            {
                var userId = _httpContextAccessor.HttpContext.User.GetId();
                if(userId == null)
                {
                    return NotFound();
                }
                var user = _userManager.FindByIdAsync(userId.ToString()).Result;
                var code =  _userManager.GenerateChangeEmailTokenAsync(user,model.Email).Result;
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ChangeEmailConfirmation",
                    pageHandler: null,
                    values: new { area = "Identity", email =  model.Email , code = code ,id=user.Id},
                    protocol: Request.Scheme);
                /*callbackUrl = Url.PageLink("/ChangeEmailConfirmation",pageHandler:null,
                values:new { area = "Identity", email =  model.Email , code = code ,id=user.Id},
                protocol: Request.Protocol, host: Request.Host.Host, "Account");*/
                _emailSender.SendEmailAsync(model.Email, "تغییر ایمیل",
                    $"لطفا ایمیل تان را با کلیک <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>اینجا</a> تایید کنید.");
                return RedirectToAction("Index","Profile",new {area=""});
            }
            return View(model);
        }
        #endregion
        
        #region methods ...
        private byte[] ConvertImageFileToByte(IFormFile file)
        {
            byte[] result;
            using(Stream fileStream = file.OpenReadStream())
            {
                MemoryStream memoryStream = new MemoryStream();
                fileStream.CopyTo(memoryStream);
                result = memoryStream.ToArray();
            }
            
            
            return result;
        }
        #endregion
    }
    
}