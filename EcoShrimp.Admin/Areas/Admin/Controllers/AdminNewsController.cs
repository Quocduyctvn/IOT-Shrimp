using AutoMapper;
using EcoShrimp.Admin.Areas.Admin.Controllers.Base;
using EcoShrimp.Admin.Areas.Admin.ViewModels.News;
using EcoShrimp.Data;
using EcoShrimp.Data.Entities;
using EcoShrimp.Share.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList.Extensions;

namespace EcoShrimp.Admin.Areas.Admin.Controllers
{
	public class AdminNewsController : AdminControllerBase
	{
		protected new List<SelectListItem> status;
		public AdminNewsController(ApplicationDbContext DbContext, IMapper mapper) : base(DbContext, mapper)
		{
			this.status = new List<SelectListItem>
					{
						new SelectListItem { Value = ((int)Status.Active).ToString(), Text = "Hoạt động" },
						new SelectListItem { Value = ((int)Status.Inactive).ToString(), Text = "Tạm ngưng" }
					};
		}

		public IActionResult Index(string keyword, int page = 1, int size = DEFAULT_PAGE_SIZE)
		{
			var news = _DbContext.AppNews.Include(x => x.appCateNew).Where(x => x.Status != Status.Deleted)
										.OrderBy(x => x.SortOrder).AsQueryable();
			if (keyword != null)
			{
				keyword = keyword.Trim().ToUpper();
				news = news.Where(x => x.Title.ToUpper().Contains(keyword) || x.Summary.ToUpper().Contains(keyword));
				TempData["searched"] = "searched";
			}

			return View(news.ToPagedList(page, size));
		}

		[HttpGet]
		public IActionResult Create()
		{
			ViewBag.cateNews = new SelectList(
									_DbContext.AppCateNews.Where(x => x.Status != Status.Deleted),
									"Id",
									"Name"
								);
			TempData["status"] = status;
			return View();
		}

		[HttpPost]
		public IActionResult Create(NewsVM model)
		{
			if (!ModelState.IsValid)
			{
				SetErrorMesg("Vui lòng kiểm tra lại dữ liệu đầu vào!!");
				return View(model);
			}

			bool isExists = _DbContext.AppNews.Any(x => x.Title == model.Title);
			if (isExists)
			{
				SetErrorMesg("Tên bài viết đã tồn tại!!");
				return View(model);
			}

			string adminRootPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "EcoShrimp.Admin", "wwwroot");
			string clientRootPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "EcoShrimp.Client", "wwwroot");

			var maxSortOrder = _DbContext.AppNews
												.DefaultIfEmpty()
												.Max(x => x == null ? 0 : x.SortOrder);
			var news = new AppNews();
			news.Title = model.Title;
			news.Summary = model.Summary;
			news.Content = model.Content;
			news.IdCateNew = model.IdCateNews;
			news.Status = model.Status;
			news.Path = UploadFileToBothProjects(model.FormFile, adminRootPath, clientRootPath);
			news.CreatedDate = DateTime.Now;
			news.SortOrder = maxSortOrder + 1;

			_DbContext.AppNews.Add(news);
			_DbContext.SaveChanges();
			SetSuccessMesg("Thêm bài viết thành công!!");
			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult Update(int id)
		{
			var news = _DbContext.AppNews.Include(x => x.appCateNew).FirstOrDefault(x => x.Id == id);
			if (news == null)
			{
				SetErrorMesg("Đã xảy ra lỗi trong quá trình xử lí!!");
				return RedirectToAction("Index");
			}
			var newsVM = new NewsVM();
			newsVM.Id = id;
			newsVM.Title = news.Title;
			newsVM.Summary = news.Summary;
			newsVM.Content = news.Content;
			newsVM.IdCateNews = news.IdCateNew;
			newsVM.Status = news.Status;
			newsVM.Path = news.Path;

			ViewBag.cateNews = new SelectList(
						_DbContext.AppCateNews.Where(x => x.Status != Status.Deleted),
						"Id",
						"Name"
					);
			TempData["status"] = status;

			return View(newsVM);
		}

		[HttpPost]
		public IActionResult Update(NewsVM model, [FromServices] IWebHostEnvironment envi)
		{
			ModelState.Remove("FormFile");
			if (!ModelState.IsValid)
			{
				SetErrorMesg("Vui lòng kiểm tra lại dữ liệu đầu vào!!");
				return View(model);
			}

			bool isExists = _DbContext.AppNews.Where(x => x.Id != model.Id).Any(x => x.Title == model.Title);
			if (isExists)
			{
				SetErrorMesg("Tên bài viết đã tồn tại");
				return View(model);
			}

			string adminRootPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "EcoShrimp.Admin", "wwwroot");
			string clientRootPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "EcoShrimp.Client", "wwwroot");

			var news = _DbContext.AppNews.FirstOrDefault(x => x.Id == model.Id);

			news.Title = model.Title;
			news.Summary = model.Summary;
			news.Content = model.Content;
			news.IdCateNew = model.IdCateNews;
			news.Status = model.Status;
			if (model.FormFile != null)
			{
				string filePath1 = Path.Combine(adminRootPath, news.Path);
				if (System.IO.File.Exists(filePath1))
				{
					System.IO.File.Delete(filePath1);
				}
				string filePath2 = Path.Combine(clientRootPath, news.Path);
				if (System.IO.File.Exists(filePath2))
				{
					System.IO.File.Delete(filePath2);
				}
				news.Path = UploadFileToBothProjects(model.FormFile, adminRootPath, clientRootPath);
			}
			news.UpdatedDate = DateTime.Now;

			_DbContext.Update(news);
			_DbContext.SaveChanges();
			SetSuccessMesg("Cập nhật thành công!!");
			return RedirectToAction("Index");
		}

		public IActionResult Delete(int id)
		{
			var news = _DbContext.AppNews.FirstOrDefault(x => x.Id == id);
			if (news == null)
			{
				SetErrorMesg("Đã xảy ra lỗi trong quá trình xử lí!!");
				return RedirectToAction("Index");
			}

			news.Status = Status.Deleted;
			news.DeletedDate = DateTime.Now;
			_DbContext.Update(news);
			_DbContext.SaveChanges();
			SetSuccessMesg("Xóa bài viết thành công!!");
			return RedirectToAction("Index");
		}


		private string UploadFileToBothProjects(IFormFile file, string adminWebRoot, string clientWebRoot)
		{
			var fName = Path.GetFileNameWithoutExtension(file.FileName)
						+ DateTime.Now.Ticks
						+ Path.GetExtension(file.FileName);

			var relativePath = "/images/news/" + fName; // Đường dẫn như cũ

			// Lưu vào thư mục của Admin
			var adminPath = Path.Combine(adminWebRoot, "images", "news", fName);
			Directory.CreateDirectory(Path.GetDirectoryName(adminPath));
			using (var stream = new FileStream(adminPath, FileMode.Create))
			{
				file.CopyTo(stream);
			}

			// Lưu vào thư mục của Client
			var clientPath = Path.Combine(clientWebRoot, "images", "news", fName);
			Directory.CreateDirectory(Path.GetDirectoryName(clientPath));
			using (var stream = new FileStream(clientPath, FileMode.Create))
			{
				file.CopyTo(stream);
			}

			return relativePath; // Trả về đường dẫn tương đối
		}

		public IActionResult Plus(int id)
		{
			var news = _DbContext.AppNews.OrderBy(x => x.SortOrder).ToList();

			var currentItem = news.FirstOrDefault(x => x.Id == id);

			int currentIndex = news.IndexOf(currentItem);
			if (currentIndex == news.Count - 1)
			{
				// Nếu là phần tử cuối cùng, giữ nguyên
				return RedirectToAction("Index");
			}

			var nextItem = news[currentIndex + 1];

			(currentItem.SortOrder, nextItem.SortOrder) = (nextItem.SortOrder, currentItem.SortOrder);

			_DbContext.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult Subtr(int id)
		{
			var news = _DbContext.AppNews.OrderBy(x => x.SortOrder).ToList();

			var currentItem = news.FirstOrDefault(x => x.Id == id);
			int currentIndex = news.IndexOf(currentItem);

			if (currentIndex == 0)
			{
				// Giữ nguyên nếu là phần tử đầu tiên
				return RedirectToAction("Index");
			}

			var previousItem = news[currentIndex - 1];

			(currentItem.SortOrder, previousItem.SortOrder) = (previousItem.SortOrder, currentItem.SortOrder);

			_DbContext.SaveChanges();
			return RedirectToAction("Index");
		}

	}
}
