using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using FreeCMS.Service.System.Abstraction;
using FreeCMS.DomainModels.Identity;
using FreeCMS.Areas.Core.ViewModels;
using FreeCMS.Service.System.Implemented;
using FreeCMS.Extensions.Attributes;

namespace FreeCMS.Areas.Core.Controllers
{
    [Area("Core")]
    [Route("Core/[controller]/[action]")]
    [Authorize]
    [ControllerInfo("مدیریت نقش","سیستم")]
    public class RoleController : Controller
    {
        #region variables ...
        private readonly FreeCmsRoleManager _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPermissionService _permissionService;
        #endregion

        #region constructors ...
        public RoleController(UserManager<ApplicationUser> userManager, FreeCmsRoleManager roleManager, IPermissionService permissionService)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
            this._permissionService = permissionService;
        }
        #endregion

        #region actions ...
        [ActionInfo("فهرست نقش ها", "مشاهده فهرست نقش ها")]
        public IActionResult Index(int? page)
        {
            var roles = _roleManager.Roles;
            int pageSize = 5;
            int pageNumber = page ?? 1;
            return View(roles.ToPagedList(pageNumber, pageSize));
        }
        [ActionInfo("افزودن نقش", "ایجاد نقش جدید")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AddRoleVm model)
        {
            if (ModelState.IsValid)
            {
                var newRole = new Role { Name = model.Name, Description = model.Description, IsAdmin = model.IsAdmin };
                var result = _roleManager.CreateAsync(newRole).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Role", new { area = "Core" });
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        [ActionInfo("مشاهده جزییات نقش", "مشاهده مجوزها، کاربران")]
        public IActionResult Details(string id)
        {
            var role = _roleManager.FindByIdAsync(id.ToString()).Result;
            if (role == null)
            {
                return NotFound();
            }
            return View(new RoleDetailsVm(role));
        }
        
        [ActionInfo("ویرایش نقش", "ویرایش نام، مجوزها و کاربران")]
        public IActionResult Edit(string id)
        {
            ViewBag.PermissionId = new SelectList(this.GetPermissions(), "Id", "FullDisplayName");
            var role = _roleManager.FindByIdAsync(id.ToString()).Result;
            if (role == null)
            {
                return NotFound();
            }
            return View(new EditRoleVm(role));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditRoleVm model)
        {
            if (ModelState.IsValid)
            {
                var role = _roleManager.FindByIdAsync(model.Id.ToString()).Result;
                role.Name = model.Name;
                role.IsAdmin = model.IsAdmin;
                role.Description = model.Description;
                var result = _roleManager.UpdateAsync(role).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Role", new { area = "Core" });
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        [ActionInfo("حذف نقش", "حذف نقش به همراه همه مجوزها")]
        public IActionResult Delete(string id)
        {
            var role = _roleManager.FindByIdAsync(id.ToString()).Result;
            if (role == null)
            {
                return NotFound();
            }
            return View(new RoleDetailsVm(role));
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(string id)
        {
            var role = _roleManager.FindByIdAsync(id.ToString()).Result;
            if (role == null)
            {
                return NotFound();
            }
            var result = _roleManager.DeleteAsync(role).Result;
            return RedirectToAction("Index", "Role", new { area = "Core" });
        }
        public PartialViewResult AddPermission2RoleReturnPartialView(string id, int permissionId)
        {
            _roleManager.AddPermissionToRole(id, permissionId);
            //var resultRole = new EditRoleVm(_roleManager.FindByIdAsync(id.ToString()).Result);
            var resultRole = new EditRoleVm(_roleManager.Roles.Include(r => r.RolePermissions).ThenInclude(rp => rp.Permission)
               .Where(r => r.Id == id).FirstOrDefault());
            return PartialView("_EditableRolePermissionsList", resultRole);
        }
        public PartialViewResult AddAllPermissions2RoleReturnPartialView(string id)
        {
            var permissionIds = _permissionService.GetPermissions().Select(p => p.Id).ToList();
            _roleManager.AddPermissionsToRole(id, permissionIds);
            return PartialView("_EditableRolePermissionsList", new EditRoleVm(_roleManager.FindByIdAsync(id.ToString()).Result));
        }
        public PartialViewResult DeletePermissionFromRoleReturnPartialView(string id, int permissionId)
        {
            _roleManager.RemovePermissionFromRole(id, permissionId);
            return PartialView("_EditableRolePermissionsList", new EditRoleVm(_roleManager.FindByIdAsync(id.ToString()).Result));
        }
        #endregion

        #region methods ...
        public List<Permission> GetPermissions() => _permissionService.GetPermissions();
        #endregion
    }

}