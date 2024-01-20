using FreeCMS.DomainModels.Identity;
using System.ComponentModel.DataAnnotations;

namespace FreeCMS.Areas.Core.ViewModels
{
    public class EditRoleVm
    {
        public EditRoleVm(){}
        public EditRoleVm(Role role)
        {
            this.Id = role.Id;
            this.Name = role.Name;
            this.Description = role.Description ?? "";
            this.IsAdmin = role.IsAdmin;
            this.RolePermissions = role.RolePermissions;
            this.UserRoles = role.UserRoles;
        }
        [Display(Name="کد")]
        public string Id { get; set; }
        [Required(ErrorMessage="وارد کردن نام الزامی است.")]
        [Display(Name="نام")]
        public string Name { get; set; }
        [Display(Name="توضیحات")]
        public string Description { get; set; }
        [Display(Name="مدیر است؟")]
        public bool IsAdmin { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        
    }
    
}