using AutoMapper;
using EcoShrimp.Admin.Areas.Admin.Controllers.Base;
using EcoShrimp.Admin.Areas.Admin.ViewModels.CateNews;
using EcoShrimp.Data;
using EcoShrimp.Data.Entities;
using EcoShrimp.Share.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList.Extensions;

namespace EcoShrimp.Admin.Areas.Admin.Controllers
{
	public class AdminCateNewsController : AdminControllerBase
	{
		protected new List<SelectListItem> status;
		public AdminCateNewsController(ApplicationDbContext DbContext, IMapper mapper) : base(DbContext, mapper)
		{
			this.status = new List<SelectListItem>
					{
						new SelectListItem { Value = ((int)Status.Active).ToString(), Text = "Hoạt động" },
						new SelectListItem { Value = ((int)Status.Inactive).ToString(), Text = "Tạm ngưng" }
					};
		}

		public IActionResult Index(string keyword, int page = 1, int size = DEFAULT_PAGE_SIZE)
		{
			var cates = _DbContext.AppCateNews.Where(x => x.Status != Status.Deleted)
										.OrderBy(x => x.SortOrder).AsQueryable();
			if (keyword != null)
			{
				keyword = keyword.Trim().ToUpper();
				cates = cates.Where(x => x.Name.ToUpper().Contains(keyword) || x.Desc.ToUpper().Contains(keyword));
				TempData["searched"] = "searched";
			}

			TempData["status"] = status;
			return View(cates.ToPagedList(page, size));
		}

		public IActionResult Create(CateNewsVM model)
		{
			if (!ModelState.IsValid)
			{
				SetErrorMesg("Vui lòng kiểm tra dữ liệu đầu vào!!");
				return RedirectToAction("Index");
			}


			bool isExists = _DbContext.AppCateNews.Any(x => x.Name == model.Name);
			if (isExists)
			{
				SetErrorMesg("Tên danh mục đã tồn tại");
				return View(model);
			}

			var maxSortOrder = _DbContext.AppCateNews
												.DefaultIfEmpty()
												.Max(x => x == null ? 0 : x.SortOrder);

			var cateNew = new AppCateNews();
			cateNew.Name = model.Name;
			cateNew.Desc = model.Desc;
			cateNew.Status = model.Status;
			cateNew.CreatedDate = DateTime.Now;
			cateNew.SortOrder = maxSortOrder + 1;

			_DbContext.Add(cateNew);
			_DbContext.SaveChanges();
			SetSuccessMesg("Thêm loại tin tức thành công!!");
			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult Update(CateNewsVM model)
		{
			if (!ModelState.IsValid)
			{
				SetErrorMesg("Vui lòng kiểm tra dữ liệu đầu vào!!");
				return RedirectToAction("Index");
			}

			var cateNew = _DbContext.AppCateNews.FirstOrDefault(x => x.Id == model.Id);
			if (cateNew == null)
			{
				SetErrorMesg("Đã xảy ra lỗi trong quá trình xử lí!!");
				return RedirectToAction("Index");
			}

			cateNew.Name = model.Name;
			cateNew.Desc = model.Desc;
			cateNew.Status = model.Status;
			cateNew.UpdatedDate = DateTime.Now;

			_DbContext.Update(cateNew);
			_DbContext.SaveChanges();
			SetSuccessMesg("Cập nhật thành công!!");
			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult Delete(int id)
		{
			var cateNew = _DbContext.AppCateNews.Include(x => x.appNews).FirstOrDefault(x => x.Id == id);
			if (cateNew == null)
			{
				SetErrorMesg("Đã xảy ra lỗi trong quá trình xử lí!!");
				return RedirectToAction("Index");
			}

			return View(cateNew);
		}

		[HttpPost]
		public IActionResult DeleteConfirm(int id)
		{
			var cateNew = _DbContext.AppCateNews.Include(x => x.appNews).FirstOrDefault(x => x.Id == id);
			if (cateNew == null)
			{
				SetErrorMesg("Đã xảy ra lỗi trong quá trình xử lí!!");
				return RedirectToAction("Index");
			}

			cateNew.Status = Status.Deleted;
			cateNew.DeletedDate = DateTime.Now;
			_DbContext.Update(cateNew);
			_DbContext.SaveChanges();
			SetErrorMesg("Xóa loại tin thành công!!");
			return RedirectToAction("Index");
		}


		public IActionResult Plus(int id)
		{
			var cateNews = _DbContext.AppCateNews.OrderBy(x => x.SortOrder).ToList();

			var currentItem = cateNews.FirstOrDefault(x => x.Id == id);

			int currentIndex = cateNews.IndexOf(currentItem);
			if (currentIndex == cateNews.Count - 1)
			{
				// Nếu là phần tử cuối cùng, giữ nguyên
				return RedirectToAction("Index");
			}

			var nextItem = cateNews[currentIndex + 1];

			(currentItem.SortOrder, nextItem.SortOrder) = (nextItem.SortOrder, currentItem.SortOrder);

			_DbContext.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult Subtr(int id)
		{
			var cateNews = _DbContext.AppCateNews.OrderBy(x => x.SortOrder).ToList();

			var currentItem = cateNews.FirstOrDefault(x => x.Id == id);
			int currentIndex = cateNews.IndexOf(currentItem);

			if (currentIndex == 0)
			{
				// Giữ nguyên nếu là phần tử đầu tiên
				return RedirectToAction("Index");
			}

			var previousItem = cateNews[currentIndex - 1];

			(currentItem.SortOrder, previousItem.SortOrder) = (previousItem.SortOrder, currentItem.SortOrder);

			_DbContext.SaveChanges();
			return RedirectToAction("Index");
		}

	}
}
