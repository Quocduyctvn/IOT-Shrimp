using AutoMapper;
using EcoShrimp.Admin.Areas.Admin.Controllers.Base;
using EcoShrimp.Admin.Areas.Admin.ViewModels.Roles;
using EcoShrimp.Data;
using EcoShrimp.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList.Extensions;

namespace EcoShrimp.Admin.Areas.Admin.Controllers
{
	public class AdminRolesController : AdminControllerBase
	{
		public AdminRolesController(ApplicationDbContext DbContext, IMapper mapper) : base(DbContext, mapper)
		{
		}


		public IActionResult Index(string keyword, int? page)
		{
			ViewBag.ListRole = _DbContext.AppRoles
										.ToList();
			var listRole = _DbContext.AppRoles
										.Include(i => i.appUsers)
										.OrderBy(x => x.SortOrder)
										.AsQueryable();
			if (!String.IsNullOrEmpty(keyword))
			{
				var keywordUpper = keyword.ToUpper();
				listRole = listRole.Where(i => i.Name.ToUpper().Contains(keywordUpper) || i.Desc.ToUpper().Contains(keywordUpper));
				TempData["searched"] = "searched";
			}

			return View(listRole.ToPagedList(page ?? DEFAULT_PAGE_NUMBER, DEFAULT_PAGE_SIZE));
		}

		public IActionResult AddRole()
		{
			return View();
		}

		[HttpPost]
		public IActionResult AddRole(AddRoleVM model)
		{
			if (!ModelState.IsValid)
			{
				SetErrorMesg("Vui lòng nhập đúng yêu cầu");
				return View(model);
			}
			if (model.IdPermission == null)
			{
				SetErrorMesg("Xảy ra lỗi trong quá trình xử lí");
				return View(model);
			}
			var arrIdPermission = model.IdPermission.Split(',');

			var role = new AppRoles();
			var maxSortOrder = _DbContext.AppRoles
												.DefaultIfEmpty()
												.Max(x => x == null ? 0 : x.SortOrder);

			role.Name = model.Name;
			role.Desc = model.Desc;
			role.CreatedDate = DateTime.Now;
			role.SortOrder = maxSortOrder + 1;
			role.appRolePermissions = new List<AppRolePermission>();

			foreach (var item in arrIdPermission)
			{
				var idPer = Convert.ToInt32(item);
				role.appRolePermissions.Add(new AppRolePermission
				{
					IdPermission = idPer
				});
			}

			_DbContext.AppRoles.Add(role);
			_DbContext.SaveChanges();
			SetSuccessMesg("Thêm vai trò thành công");
			return RedirectToAction("Index");
		}


		[HttpGet]
		public async Task<IActionResult> UpdateRole(int? id)
		{
			var data = await _DbContext.AppRoles
							.Include(r => r.appRolePermissions)
							.FirstOrDefaultAsync(r => r.Id == id.Value);
			if (data == null)
			{
				return NotFound();
			}
			var model = new UpdateRoleVM
			{
				Id = data.Id,
				Name = data.Name,
				Desc = data.Desc,
				IdPermission = string.Join(',', data.appRolePermissions.Select(rp => rp.IdPermission)),
			};

			return View(model);
		}
		[HttpPost]
		public IActionResult UpdateRole(UpdateRoleVM model)
		{
			if (String.IsNullOrEmpty(model.IdPermission))
			{
				SetErrorMesg("Vai trò ít nhất 1 hành động");
				return RedirectToAction("UpdateRole");
			}


			var role = _DbContext.AppRoles.Find(model.Id);
			// kiểm tra có XÓA bớt hay không 
			string[] deletedIdPermission = new string[200];
			if (model.DeletedIdPermission != null)
			{
				if (model.DeletedIdPermission.Contains(","))  // kiểm tra xem có nhiều hơn 1 hay không
				{
					deletedIdPermission = model.DeletedIdPermission.Split(',');
				}
				else
				{
					deletedIdPermission[0] = model.DeletedIdPermission;
				}
			}

			if (deletedIdPermission.Length > 0)
			{
				foreach (var item in deletedIdPermission)
				{
					if (item != null)
					{
						var idPer = Convert.ToInt32(item);
						var rolePer = _DbContext.AppRolePermission
										.Where(i => i.IdRole == role.Id && i.IdPermission == idPer)
										.FirstOrDefault();
						_DbContext.AppRolePermission.Remove(rolePer);
						_DbContext.SaveChanges();
					}
				}
			}

			// kiểm tra có thêm mới hay không 
			string[] addedIdPermission = new string[200];
			if (model.AddedIdPermission != null)
			{
				if (model.AddedIdPermission.Contains(",")) // kiểm tra xem có nhiều hơn 1 hay không 
				{
					addedIdPermission = model.AddedIdPermission.Split(',');
				}
				else
				{
					addedIdPermission[0] = model.AddedIdPermission;
				}
			}

			if (addedIdPermission.Length > 0)
			{
				role.appRolePermissions = new List<AppRolePermission>();
				foreach (var item in addedIdPermission)
				{
					if (item != null)
					{
						var idPer = Convert.ToInt32(item);
						role.appRolePermissions.Add(new AppRolePermission
						{
							IdPermission = idPer
						});
					}
				}
			}
			role.Name = model.Name;
			role.Desc = model.Desc;

			_DbContext.AppRoles.Update(role);
			_DbContext.SaveChanges();
			SetSuccessMesg("Cập nhật vai trò thành công");
			return RedirectToAction("Index");
		}


		public IActionResult DeleteRole(int id)
		{
			var role = _DbContext.AppRoles
								.Include(i => i.appUsers)
								.FirstOrDefault(i => i.Id == id);
			if (role == null)
			{
				SetErrorMesg("Đã xảy ra lỗi trong quá trình xử lí");
				return RedirectToAction("Index");
			}
			var listUser = _DbContext.AppUsers
					.Where(i => i.IdRole == role.Id)
					.ToList();
			DeleteRoleVM deleteRoloVM = new DeleteRoleVM();
			deleteRoloVM.Name = role.Name;
			deleteRoloVM.Desc = role.Desc;
			deleteRoloVM.CreateDate = (DateTime)role.CreatedDate;
			deleteRoloVM.IdRole = id;
			deleteRoloVM.appUsers = new List<RoleDeleteVM_User>();

			foreach (var item in listUser)
			{
				var user = new RoleDeleteVM_User();
				user.IdUser = item.Id;
				user.Name = item.Name;
				deleteRoloVM.appUsers.Add(user);
			}
			ViewBag.ListRole = _DbContext.AppRoles.ToList();

			return View(deleteRoloVM);
		}

		[HttpPost]
		public IActionResult DeleteRole(DeleteRoleVM model, int id)
		{
			var role = _DbContext.AppRoles.Include(x => x.appUsers)
								.FirstOrDefault(i => i.Id == id);
			if (role == null)
			{
				SetErrorMesg("Đã xảy ra lỗi trong quá trình xử lí");
				return Redirect(Request.Headers["Referer"].ToString() ?? "/");
			}


			// Cập nhật Role 
			var listUser = _DbContext.AppUsers
					.Where(i => i.IdRole == role.Id)
					.ToList();
			if (listUser.Count > 0 && listUser != null)
			{
				if (model.IdNewRole == role.Id || model.IdNewRole <= 0)
				{
					SetErrorMesg("Vui lòng chọn 'Vai trò' để thay thế!!");
					return Redirect(Request.Headers["Referer"].ToString() ?? "/");
				}

				foreach (var item in listUser)
				{
					item.IdRole = model.IdNewRole;
					_DbContext.AppUsers.Update(item);
					_DbContext.SaveChanges();
				}
			}

			// Xóa item Role ở bảng RolePermission  
			var RolePer = _DbContext.AppRolePermission
												.Where(i => i.IdRole == id)
												.ToList();
			if (RolePer.Count > 0 && RolePer != null)
			{
				foreach (var item in RolePer)
				{
					_DbContext.AppRolePermission.Remove(item);
					_DbContext.SaveChanges();
				}
			}
			// xóa Role 
			_DbContext.AppRoles.Remove(role);
			_DbContext.SaveChanges();
			SetSuccessMesg("Xóa vai trò thành công");
			return RedirectToAction("Index");
		}

		public IActionResult Plus(int id)
		{
			var roles = _DbContext.AppRoles.OrderBy(x => x.SortOrder).ToList();

			var currentItem = roles.FirstOrDefault(x => x.Id == id);

			int currentIndex = roles.IndexOf(currentItem);
			if (currentIndex == roles.Count - 1)
			{
				// Nếu là phần tử cuối cùng, giữ nguyên
				return RedirectToAction("Index");
			}

			var nextItem = roles[currentIndex + 1];

			(currentItem.SortOrder, nextItem.SortOrder) = (nextItem.SortOrder, currentItem.SortOrder);

			_DbContext.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult Subtr(int id)
		{
			var roles = _DbContext.AppRoles.OrderBy(x => x.SortOrder).ToList();

			var currentItem = roles.FirstOrDefault(x => x.Id == id);
			int currentIndex = roles.IndexOf(currentItem);

			if (currentIndex == 0)
			{
				// Giữ nguyên nếu là phần tử đầu tiên
				return RedirectToAction("Index");
			}

			var previousItem = roles[currentIndex - 1];

			(currentItem.SortOrder, previousItem.SortOrder) = (previousItem.SortOrder, currentItem.SortOrder);

			_DbContext.SaveChanges();
			return RedirectToAction("Index");
		}

	}
}
