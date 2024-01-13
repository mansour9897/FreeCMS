using FreeCMS.DomainModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Principal;

namespace FreeCMS.Service.Filters
{
	public class WeboPermissionFilter : Attribute, IAsyncAuthorizationFilter
	{
		private readonly UserManager<ApplicationUser> _userManager;
		public WeboPermissionFilter(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}
		public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
		{
			var user = context.HttpContext.User;
			if (!user.Identity.IsAuthenticated)
			{
				context.Result = new RedirectToPageResult("/Account/Login", new { area = "Identity" });
			}
			else
			{
				var routeData = context.RouteData;
				string areaName = routeData.Values.ContainsKey("area") ? routeData.Values["area"].ToString() : "";
				string controllerName = routeData.Values.ContainsKey("controller") ? routeData.Values["controller"].ToString() : "";
				controllerName += "Controller";
				string actionName = routeData.Values.ContainsKey("action") ? routeData.Values["action"].ToString() : "";
				//create a permission string based on the request in the 'area-controller-action' format
				string requiredPermission = String.Format("{0}-{1}-{2}", areaName, controllerName, actionName);
				if (!await user.HasPermission(_userManager, requiredPermission) && !user.IsSysAdmin(_userManager))
				{
					context.Result = new RedirectToRouteResult(
									   new RouteValueDictionary { { "action", "Index" },
						  { "controller", "Unauthorised" },{"area",""} });
				}
			}
		}
	}
	public static class PricipalExtensions
	{
		public static string GetId(this IPrincipal principal)
		{
			if (principal != null && principal.Identity != null)
			{
				var ci = principal.Identity as ClaimsIdentity;
				string userId = ci != null ? ci.FindFirst(ClaimTypes.NameIdentifier).Value : null;
				//int result = 0;
				//if (int.TryParse(userId, out result))
				//{
				//	return result;
				//}
				return userId;
			}
			return "";
		}
		public static async Task<bool> HasPermission(this IPrincipal principal, UserManager<ApplicationUser> userManager, string requiredPermission)
		{
			bool hasPermission = false;
			if (principal != null && principal.Identity != null && principal.Identity.IsAuthenticated)
			{
				var ci = principal.Identity as ClaimsIdentity;
				string userId = ci != null ? ci.FindFirst(ClaimTypes.NameIdentifier).Value : null;
				if (!string.IsNullOrEmpty(userId))
				{
					//var authenticatedUser = await userManager.FindByIdAsync(userId);
					var authenticatedUser = userManager.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
						.ThenInclude(r => r.RolePermissions).ThenInclude(rp => rp.Permission)
						.Where(u => u.Id == userId).FirstOrDefault();
					hasPermission = authenticatedUser.IsPermissionInUserRoles(requiredPermission);
				}
			}
			return hasPermission;
		}
		public static bool IsSysAdmin(this IPrincipal principal, UserManager<ApplicationUser> userManager)
		{
			bool isAdmin = false;
			if (principal != null && principal.Identity != null && principal.Identity.IsAuthenticated)
			{
				var ci = principal.Identity as ClaimsIdentity;
				string userId = ci != null ? ci.FindFirst(ClaimTypes.NameIdentifier).Value : null;
				if (!string.IsNullOrEmpty(userId))
				{

					var authenticatedUser = userManager.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
						.ThenInclude(r => r.RolePermissions).ThenInclude(rp => rp.Permission)
						.Where(u => u.Id == userId).FirstOrDefault();
					isAdmin = authenticatedUser.IsAdmin();
				}
			}
			return isAdmin;
		}
		public static async Task<bool> CouldAccessToDashboard(this IPrincipal principal, UserManager<ApplicationUser> userManager, string requiredPermission)
		{
			return (principal.HasPermission(userManager, requiredPermission).Result || principal.IsSysAdmin(userManager));
		}
	}

}
