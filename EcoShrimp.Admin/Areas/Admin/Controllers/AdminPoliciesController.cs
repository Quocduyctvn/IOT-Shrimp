using AutoMapper;
using EcoShrimp.Admin.Areas.Admin.Controllers.Base;
using EcoShrimp.Admin.Areas.Admin.ViewModels.Policies;
using EcoShrimp.Data;
using EcoShrimp.Data.Entities;
using EcoShrimp.Share.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList.Extensions;

namespace EcoShrimp.Admin.Areas.Admin.Controllers
{
	public class AdminPoliciesController : AdminControllerBase
	{
		protected new List<SelectListItem> status;
		public AdminPoliciesController(ApplicationDbContext DbContext, IMapper mapper) : base(DbContext, mapper)
		{
			this.status = new List<SelectListItem>
					{
						new SelectListItem { Value = ((int)Status.Active).ToString(), Text = "Hoạt động" },
						new SelectListItem { Value = ((int)Status.Inactive).ToString(), Text = "Tạm ngưng" }
					};
		}

		public IActionResult Index(string keyword, int page = 1, int size = DEFAULT_PAGE_SIZE)
		{
			var news = _DbContext.AppPolicies.Where(x => x.Status != Status.Deleted)
										.OrderBy(x => x.SortOrder).AsQueryable();
			if (keyword != null)
			{
				keyword = keyword.Trim().ToUpper();
				news = news.Where(x => x.Name.ToUpper().Contains(keyword) || x.Summary.ToUpper().Contains(keyword));
				TempData["searched"] = "searched";
			}

			return View(news.ToPagedList(page, size));
		}

		public IActionResult Create()
		{
			TempData["status"] = status;
			return View();
		}

		[HttpPost]
		public IActionResult Create(PoliciesVM model, [FromServices] IWebHostEnvironment envi)
		{
			if (!ModelState.IsValid)
			{
				SetErrorMesg("Vui lòng kiểm tra dữ liệu đầu vào!!");
				return View(model);
			}

			string adminRootPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "EcoShrimp.Admin", "wwwroot");
			string clientRootPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "EcoShrimp.Client", "wwwroot");

			var maxSortOrder = _DbContext.AppPolicies
												.DefaultIfEmpty()
												.Max(x => x == null ? 0 : x.SortOrder);

			var policy = new AppPolicies();
			policy.Name = model.Name;
			policy.Summary = model.Summary;
			policy.Content = model.Content;
			policy.Status = model.Status;
			policy.Path = UploadFileToBothProjects(model.FormFile, adminRootPath, clientRootPath);
			policy.SortOrder = maxSortOrder + 1;
			policy.CreatedDate = DateTime.Now;

			_DbContext.Add(policy);
			_DbContext.SaveChanges();
			SetSuccessMesg("Thêm mới Chính sách thành công!!");
			return RedirectToAction("Index");
		}


		[HttpGet]
		public IActionResult Update(int id)
		{
			if (id <= 0)
			{
				SetErrorMesg("Truy cập không hợp lệ!!");
				RedirectToAction("Index");
			}
			var policies = _DbContext.AppPolicies.FirstOrDefault(x => x.Id == id);
			if (policies == null)
			{
				SetErrorMesg("Đã xảy ra lỗi trong quá trình xử lí!!");
				RedirectToAction("Index");
			}

			TempData["status"] = status;

			var policiesVM = new PoliciesVM();
			policiesVM.Name = policies.Name;
			policiesVM.Summary = policies.Summary;
			policiesVM.Content = policies.Content;
			policiesVM.Status = policies.Status;
			policiesVM.Path = policies.Path;

			return View(policiesVM);
		}

		[HttpPost]
		public IActionResult Update(PoliciesVM model, int id)
		{
			ModelState.Remove("FormFile");
			if (!ModelState.IsValid)
			{
				SetErrorMesg("Vui lòng kiểm tra dữ liệu đầu vào!!");
				TempData["status"] = status;
				return View(model);
			}
			var policy = _DbContext.AppPolicies.FirstOrDefault(x => x.Id == id);
			if (policy == null)
			{
				SetErrorMesg("Đã xảy ra lỗi trong quá trình xử lí!!");
				return View(model);
			}

			string adminRootPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "EcoShrimp.Admin", "wwwroot");
			string clientRootPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "EcoShrimp.Client", "wwwroot");

			policy.Name = model.Name;
			policy.Summary = model.Summary;
			policy.Content = model.Content;
			policy.Status = model.Status;
			policy.UpdatedDate = DateTime.Now;
			if (model.FormFile != null)
			{
				policy.Path = UploadFileToBothProjects(model.FormFile, adminRootPath, clientRootPath);
			}

			_DbContext.Update(policy);
			_DbContext.SaveChanges();
			SetSuccessMesg("Cập nhật thành công!!");
			return RedirectToAction("Index");
		}

		public IActionResult Delete(int id)
		{
			var policies = _DbContext.AppPolicies.FirstOrDefault(x => x.Id == id);
			if (policies == null)
			{
				SetErrorMesg("Đã xảy ra lỗi trong quá trình xử lí!!");
				return RedirectToAction("Index");
			}

			policies.Status = Status.Deleted;
			policies.DeletedDate = DateTime.Now;
			_DbContext.Update(policies);
			_DbContext.SaveChanges();
			SetSuccessMesg("Xóa chính sách thành công!!");
			return RedirectToAction("Index");
		}


		public IActionResult Plus(int id)
		{
			var policies = _DbContext.AppPolicies.OrderBy(x => x.SortOrder).ToList();

			var currentItem = policies.FirstOrDefault(x => x.Id == id);

			int currentIndex = policies.IndexOf(currentItem);
			if (currentIndex == policies.Count - 1)
			{
				// Nếu là phần tử cuối cùng, giữ nguyên
				return RedirectToAction("Index");
			}

			var nextItem = policies[currentIndex + 1];

			(currentItem.SortOrder, nextItem.SortOrder) = (nextItem.SortOrder, currentItem.SortOrder);

			_DbContext.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult Subtr(int id)
		{
			var policies = _DbContext.AppPolicies.OrderBy(x => x.SortOrder).ToList();

			var currentItem = policies.FirstOrDefault(x => x.Id == id);
			int currentIndex = policies.IndexOf(currentItem);

			if (currentIndex == 0)
			{
				// Giữ nguyên nếu là phần tử đầu tiên
				return RedirectToAction("Index");
			}

			var previousItem = policies[currentIndex - 1];

			(currentItem.SortOrder, previousItem.SortOrder) = (previousItem.SortOrder, currentItem.SortOrder);

			_DbContext.SaveChanges();
			return RedirectToAction("Index");
		}

		private string UploadFileToBothProjects(IFormFile file, string adminWebRoot, string clientWebRoot)
		{
			var fName = Path.GetFileNameWithoutExtension(file.FileName)
						+ DateTime.Now.Ticks
						+ Path.GetExtension(file.FileName);

			var relativePath = "/images/policies/" + fName; // Đường dẫn như cũ

			// Lưu vào thư mục của Admin
			var adminPath = Path.Combine(adminWebRoot, "images", "policies", fName);
			Directory.CreateDirectory(Path.GetDirectoryName(adminPath));
			using (var stream = new FileStream(adminPath, FileMode.Create))
			{
				file.CopyTo(stream);
			}

			// Lưu vào thư mục của Client
			var clientPath = Path.Combine(clientWebRoot, "images", "policies", fName);
			Directory.CreateDirectory(Path.GetDirectoryName(clientPath));
			using (var stream = new FileStream(clientPath, FileMode.Create))
			{
				file.CopyTo(stream);
			}

			return relativePath; // Trả về đường dẫn tương đối
		}
	}
}
