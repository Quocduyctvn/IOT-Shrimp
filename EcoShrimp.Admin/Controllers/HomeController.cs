using EcoShrimp.Admin.Controllers.Base;
using EcoShrimp.Admin.Models;
using EcoShrimp.Data;
using EcoShrimp.Share.Const;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace EcoShrimp.Admin.Controllers
{
	public class HomeController : AdControllerBase
	{
		public HomeController(ApplicationDbContext DbContext) : base(DbContext)
		{
		}

		public IActionResult Index(string returnUrl = null)
		{
			if (returnUrl != null)
			{
				SetErrorMesg("Truy cập không hợp lệ!!");
			}
			return View();
		}

		[HttpPost]
		public IActionResult Login(LoginVM model)
		{
			if (!ModelState.IsValid)
			{
				SetErrorMesg("Vui lòng kiểm tra dữ liệu đầu vào!!");
				return RedirectToAction("Index", model);
			}

			var user = _DbContext.AppUsers
									.Include(x => x.appRole)
									.ThenInclude(x => x.appRolePermissions)
									.FirstOrDefault(x => x.Phone == model.Phone);
			if (user == null)
			{
				SetErrorMesg("Thông tin đăng nhập không hợp lệ!!");
				return RedirectToAction("Index", model);
			}

			var checkPass = BCrypt.Net.BCrypt.Verify(model?.Pass, user.Pass);
			if (!checkPass)
			{
				SetErrorMesg("Thông tin đăng nhập không hợp lệ!!!");
				return RedirectToAction("Index", model);
			}

			var per = string.Join(',', user.appRole.appRolePermissions.Select(rp => rp.IdPermission));
			var claims = new List<Claim>
							{
								new Claim(ClaimTypes.MobilePhone, user.Phone),
								new Claim(ClaimTypes.Name, user.Name),
								new Claim(("ID"), user.Id.ToString()),
								new Claim(ClaimTypes.Role , "Admin"),
								new Claim(AppClaimTypes.Permissions,  per),
							};
			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
			HttpContext.SignInAsync(claimsPrincipal);
			return RedirectToAction("Index", "AdminHome", new { area = "Admin" });
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
