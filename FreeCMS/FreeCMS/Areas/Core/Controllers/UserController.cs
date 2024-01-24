using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Identity.UI.Services;
using X.PagedList;
using FreeCMS.DomainModels.Identity;
using Microsoft.AspNetCore.Identity;
using FreeCMS.Service.Filters;
using Microsoft.AspNetCore.Authorization;

using FreeCMS.Areas.Core.ViewModels;
using FreeCMS.Extensions.Attributes;
using FreeCMS.Attributes;

namespace Webo.Core.Areas.Core.Controllers
{
    [Area("Core")]
    [Route("Core/[controller]/[action]")]
    [FreeCmsAuthorize]
    [ControllerInfo("مدیریت کاربران", "سیستم")]
    public class UserController:Controller
    {
        #region variables ...
        private readonly ILogger<UserController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<Role> _roleManager;
        #endregion

        #region constructors ...
        public UserController(IHttpContextAccessor httpContextAccessor,ILogger<UserController> logger,
            IEmailSender emailSender,UserManager<ApplicationUser> userManager,RoleManager<Role> roleManager)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _emailSender = emailSender;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        #endregion

        #region actions ...
        
        [ActionInfo("فهرست کاربران","مشاهده اطلاعات تمام کاربران ")]
        public IActionResult Index(int? page)
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.GetId();
            var users = _userManager.Users.Where(u => u.Id != currentUserId).OrderBy(u => u.UserName).ToList();
           
            int pageSize = 5;
            int pageNumber = page ?? 1;
            return View(users.ToPagedList(pageNumber,pageSize));
        }

        [ActionInfo("افزودن کاربر","افزودن کاربر جدید")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AddUserVm model)
        {
            if(ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName , Email = model.Email,
                    FirstName = model.FirstName, LastName = model.LastName, PhoneNumber = model.MobileNumber,
                    PhoneNumberConfirmed = true,Active = true};
                var result =  _userManager.CreateAsync(user, model.Password).Result;
                if(result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code =  _userManager.GenerateEmailConfirmationTokenAsync(user).Result;
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    _emailSender.SendEmailAsync(model.Email, "ایمیل تان را تایید کنید.",
                        $"لطفا حساب کاربری تان را با کلیک <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>اینجا</a> تایید کنید.");

                    return RedirectToAction("Index","User",new {area="Core"});
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [ActionInfo("اطلاعات کاربر","مشاهده اطلاعات کاربر")]
        public IActionResult Details(string id)
        {
            var user = _userManager.FindByIdAsync(id.ToString()).Result;
            if(user == null)
            {
                return  NotFound();
            }
            return View(new UserDetails(user));
        }

        [ActionInfo("ویرایش کاربر","ویرایش اطلاعات کاربر")]
        public IActionResult Edit(string id)
        {
            ViewBag.RoleId = new SelectList(this.GetRoles(), "Id", "Name");
            ViewBag.UserId = id;
            string newPassword = "P@ssw0rd";
            ViewBag.NewPassword = newPassword;
            var user = _userManager.FindByIdAsync(id).Result;
            if(user == null)
            {
                return  NotFound();
            }
            
            var model = new EditUserVm(user);
            model.UserRoles = _userManager.GetRolesAsync(user).Result;

			return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditUserVm model)
        {
            ViewBag.RoleId = new SelectList(this.GetRoles(), "Id", "Name");
            ViewBag.UserId = model.Id;
            string newPassword = "P@ssw0rd";
            ViewBag.NewPassword = newPassword;
            if(ModelState.IsValid)
            {
                var user = _userManager.FindByIdAsync(model.Id.ToString()).Result;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Active = model.Active;
                var updateResult = _userManager.UpdateAsync(user).Result;
                if(updateResult.Succeeded)
                {
                    return RedirectToAction("Details","User",new{area="Core",id=model.Id});
                }
                foreach (var error in updateResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        [ActionInfo("حذف کاربر","حذف کاربر")]
        public IActionResult Delete(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            if(user == null)
            {
                return  NotFound();
            }
            return View(new UserDetails(user));
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            if(user != null)
            {
                var result =  _userManager.DeleteAsync(user).Result;
                if(result.Succeeded)
                {
                    return RedirectToAction("Index","User",new{area="Core"});
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            } 
            return RedirectToAction("Index","User",new{area="Core"});
        }
        public IActionResult ChangeImage(int id)
        {
            ViewBag.ProfileImageSize = 500;
            ViewBag.UserId = id;
            return View();
        }
        
        public IActionResult ResetUserPassword(string id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var user = _userManager.FindByIdAsync(id.ToString()).Result;
            string newPassword = "P@ssw0rd";
            ViewBag.NewPassword = newPassword;
            var code = _userManager.GeneratePasswordResetTokenAsync(user).Result;
            var changePasswordResult = _userManager.ResetPasswordAsync(user, code, newPassword);

            if (changePasswordResult.Result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "رمز کاربر با موفقیت تغییر کرد.");
                return RedirectToAction("Edit", "User", new { area = "Core", id = id });
            }

            foreach (var error in changePasswordResult.Result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return RedirectToAction("Edit","User",new {area="Core",id=id});
        }
        public PartialViewResult AddUserRoleReturnPartialView(string id, string userId)
        {
            var role = _roleManager.FindByIdAsync(id).Result;
            var user = _userManager.FindByIdAsync(userId.ToString()).Result;
            
            if(role != null && user != null)
            {
                var result = _userManager.AddToRoleAsync(user, role.Name).Result;
                
            }
			var model = new EditUserVm(user);
			model.UserRoles = _userManager.GetRolesAsync(user).Result;
			//var resultUser = _userManager.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).Where(u => u.Id == id).FirstOrDefault();
			return PartialView("_EditableUserRoles", model);
        }
        public PartialViewResult DeleteUserRoleReturnPartialView(string roleName, string userId)
        {
            var role = _roleManager.FindByNameAsync(roleName).Result;//.FindByIdAsync(id.ToString()).Result;
            var user = _userManager.FindByIdAsync(userId.ToString()).Result;
            if(role != null && user != null)
            {
                var result = _userManager.RemoveFromRoleAsync(user, role.Name).Result;
                
            }

            var model = new EditUserVm(_userManager.FindByIdAsync(userId).Result);
            if (model is not null)
                model.UserRoles = _userManager.GetRolesAsync(user).Result;
			return PartialView("_EditableUserRoles",model);
        }
        #endregion
        
        #region methods ...
        private List<Role> GetRoles() => _roleManager.Roles.OrderBy(r => r.Name).ToList();
        
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