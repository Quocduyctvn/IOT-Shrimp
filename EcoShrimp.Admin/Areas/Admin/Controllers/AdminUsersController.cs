using AutoMapper;
using EcoShrimp.Admin.Areas.Admin.Controllers.Base;
using EcoShrimp.Admin.Areas.Admin.ViewModels.Users;
using EcoShrimp.Data;
using EcoShrimp.Data.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList.Extensions;

namespace EcoShrimp.Admin.Areas.Admin.Controllers
{
	public class AdminUsersController : AdminControllerBase
	{
		public AdminUsersController(ApplicationDbContext DbContext, IMapper mapper) : base(DbContext, mapper)
		{
		}

		public IActionResult Index(string keyword, int? page)
		{

			ViewBag.ListUser = _DbContext.AppUsers
										.Include(i => i.appRole)
										.ToList();
			var listUser = _DbContext.AppUsers
									.Include(i => i.appRole)
									.OrderBy(x => x.SortOrder)
									.AsQueryable();
			if (!String.IsNullOrEmpty(keyword))
			{
				var keywordUpper = keyword.ToUpper();
				listUser = listUser.Where(i => i.Name.ToUpper().Contains(keywordUpper) || i.appRole.Name.ToUpper().Contains(keywordUpper) || i.Email.ToUpper().Contains(keywordUpper));
				TempData["searched"] = "searched";
			}

			return View(listUser.ToPagedList(page ?? DEFAULT_PAGE_NUMBER, DEFAULT_PAGE_SIZE));
		}

		public IActionResult AddUser()
		{
			ViewBag.ListRole = _DbContext.AppRoles.ToList();
			return View();
		}

		[HttpPost]
		public IActionResult AddUser(UserVM userVM)
		{

			if (!ModelState.IsValid)
			{
				SetErrorMesg("Vui lòng kiểm tra lại dữ liệu: ");
				return RedirectToAction("AddUser");
			}

			var exit = _DbContext.AppUsers.Any(i => i.Phone == userVM.Phone || i.Name == userVM.Name);
			if (exit)
			{
				SetErrorMesg("Tài khoản đã tồn tại");
				return RedirectToAction("AddUser");
			}

			var maxSortOrder = _DbContext.AppRoles
												.DefaultIfEmpty()
												.Max(x => x == null ? 0 : x.SortOrder);

			var user = new AppUsers();
			user.Name = userVM.Name;
			user.Email = userVM.Email;
			user.Address = userVM.Address;
			user.Pass = BCrypt.Net.BCrypt.HashPassword(userVM.Password);
			user.Phone = userVM.Phone;
			user.CreatedDate = DateTime.Now;
			user.IdRole = userVM.IdRole;
			user.SortOrder = maxSortOrder + 1;

			_DbContext.AppUsers.Add(user);
			_DbContext.SaveChanges();
			SetSuccessMesg("Thêm tài khoản thành công!!");
			return RedirectToAction("Index");
		}

		public IActionResult UpdateUser(int? id)
		{
			ViewBag.ListRole = _DbContext.AppRoles.ToList();
			var user = _DbContext.AppUsers.Find(id);
			if (user == null)
			{
				SetErrorMesg("Đã xảy ra lỗi trong quá trình xử lí");
				return RedirectToAction("Index");
			}
			var userVM = new UserVM();
			userVM.Address = user.Address;
			userVM.Password = user.Pass;
			userVM.Cfm_Password = user.Pass;
			userVM.Email = user.Email;
			userVM.Phone = user.Phone;
			userVM.Name = user.Name;
			userVM.IdRole = user.IdRole;

			return View(userVM);
		}

		[HttpPost]
		public IActionResult UpdateUser(int? id, UserVM userVM)
		{
			ModelState.Remove("Password");
			ModelState.Remove("Cfm_Password");
			if (!ModelState.IsValid)
			{
				SetErrorMesg("Vui lòng kiểm tra lại dữ liệu: ");
				return RedirectToAction("UpdateUser");
			}

			var user = _DbContext.AppUsers.Find(id);
			user.Name = userVM.Name;
			user.Email = userVM.Email;
			user.Address = userVM.Address;
			user.Phone = userVM.Phone;
			user.CreatedDate = DateTime.Now;
			user.IdRole = userVM.IdRole;
			_DbContext.AppUsers.Update(user);
			_DbContext.SaveChanges();
			SetSuccessMesg("Cập nhật thành công!!");

			return RedirectToAction("Index");
		}

		public IActionResult DeleteUser(string id, [FromServices] IWebHostEnvironment envi)
		{
			int Id = int.Parse(id);
			var user = _DbContext.AppUsers.Find(Id);

			if (!string.IsNullOrEmpty(user.Avatar))
			{
				System.IO.File.Delete(Path.Combine(envi.WebRootPath, user.Avatar.TrimStart('/')));
			}
			_DbContext.Remove(user);
			_DbContext.SaveChanges();
			SetSuccessMesg("Xóa tài khoản thành công!!");
			return RedirectToAction("Index");
		}

		public IActionResult Plus(int id)
		{
			var users = _DbContext.AppUsers.OrderBy(x => x.SortOrder).ToList();

			var currentItem = users.FirstOrDefault(x => x.Id == id);

			int currentIndex = users.IndexOf(currentItem);
			if (currentIndex == users.Count - 1)
			{
				// Nếu là phần tử cuối cùng, giữ nguyên
				return RedirectToAction("Index");
			}

			var nextItem = users[currentIndex + 1];

			(currentItem.SortOrder, nextItem.SortOrder) = (nextItem.SortOrder, currentItem.SortOrder);

			_DbContext.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult Subtr(int id)
		{
			var users = _DbContext.AppUsers.OrderBy(x => x.SortOrder).ToList();

			var currentItem = users.FirstOrDefault(x => x.Id == id);
			int currentIndex = users.IndexOf(currentItem);

			if (currentIndex == 0)
			{
				// Giữ nguyên nếu là phần tử đầu tiên
				return RedirectToAction("Index");
			}

			var previousItem = users[currentIndex - 1];

			(currentItem.SortOrder, previousItem.SortOrder) = (previousItem.SortOrder, currentItem.SortOrder);

			_DbContext.SaveChanges();
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			SetSuccessMesg("Đăng xuất thành công!!");
			return RedirectToAction("Index", "Home", new { area = "" });
		}
	}
}
