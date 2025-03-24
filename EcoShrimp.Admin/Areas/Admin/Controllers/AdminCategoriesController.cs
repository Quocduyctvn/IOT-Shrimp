using AutoMapper;
using EcoShrimp.Admin.Areas.Admin.Controllers.Base;
using EcoShrimp.Admin.Areas.Admin.ViewModels.Categories;
using EcoShrimp.Data;
using EcoShrimp.Data.Entities;
using EcoShrimp.Share.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList.Extensions;

namespace EcoShrimp.Admin.Areas.Admin.Controllers
{
	public class AdminCategoriesController : AdminControllerBase
	{
		protected new List<SelectListItem> status;
		public AdminCategoriesController(ApplicationDbContext DbContext, IMapper mapper) : base(DbContext, mapper)
		{
			this.status = new List<SelectListItem>
					{
						new SelectListItem { Value = ((int)Status.Active).ToString(), Text = "Hoạt động" },
						new SelectListItem { Value = ((int)Status.Inactive).ToString(), Text = "Tạm ngưng" }
					};
		}

		public IActionResult Index(string keyword, int page = 1, int size = DEFAULT_PAGE_SIZE)
		{
			var cates = _DbContext.AppCategories.Where(x => x.Status != Status.Deleted)
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

		[HttpPost]
		public IActionResult Create(CateVM model)
		{
			if (!ModelState.IsValid)
			{
				SetErrorMesg("Dữ liệu không hợp lệ");
				return RedirectToAction("Index");
			}
			var checkCate = _DbContext.AppCategories.FirstOrDefault(x => x.Name == model.Name);
			if (checkCate != null)
			{
				SetErrorMesg("Tên danh mục đã tồn tại!!");
				return RedirectToAction("Index");
			}
			var maxSortOrder = _DbContext.AppCategories
												.DefaultIfEmpty()
												.Max(x => x == null ? 0 : x.SortOrder);

			var newCate = _mapper.Map<AppCategories>(model);
			newCate.CreatedDate = DateTime.Now;
			newCate.SortOrder = maxSortOrder + 1;
			_DbContext.Add(newCate);
			_DbContext.SaveChanges();
			SetSuccessMesg("Thêm mới thành công.");
			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult Update(CateVM model)
		{
			var cate = _DbContext.AppCategories.Find(model.Id);
			if (cate == null)
			{
				SetErrorMesg("Danh mục không tồn tại");
				return RedirectToAction("Index");
			}
			if (!ModelState.IsValid)
			{
				SetErrorMesg("Dữ liệu không hợp lệ");
				return RedirectToAction("Index");
			}
			var checkCate = _DbContext.AppCategories.FirstOrDefault(x => x.Name == model.Name && x.Id != model.Id);
			if (checkCate != null)
			{
				SetErrorMesg("Tên danh mục đã tồn tại!!");
				return RedirectToAction("Index");
			}
			_mapper.Map(model, cate);
			cate.UpdatedDate = DateTime.Now;
			_DbContext.SaveChanges();
			SetSuccessMesg("Cập nhật thành công.");
			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult Delete(int id)
		{
			var cate = _DbContext.AppCategories.Include(x => x.appProducts).FirstOrDefault(x => x.Id == id);
			if (cate == null)
			{
				SetErrorMesg("Danh mục không tồn tại");
				return RedirectToAction("Index");
			}
			return View(cate);
		}

		[HttpPost]
		public IActionResult DeleteConfirm(int id)
		{
			var cate = _DbContext.AppCategories.FirstOrDefault(x => x.Id == id);
			if (cate == null)
			{
				SetErrorMesg("Danh mục không tồn tại");
				return RedirectToAction("Index");
			}
			cate.Status = Status.Deleted;
			_DbContext.SaveChanges();
			SetSuccessMesg("Xóa thành công.");
			return RedirectToAction("Index");
		}

		public IActionResult Plus(int id)
		{
			var cates = _DbContext.AppCategories.OrderBy(x => x.SortOrder).ToList();

			var currentItem = cates.FirstOrDefault(x => x.Id == id);

			int currentIndex = cates.IndexOf(currentItem);
			if (currentIndex == cates.Count - 1)
			{
				// Nếu là phần tử cuối cùng, giữ nguyên
				return RedirectToAction("Index");
			}

			var nextItem = cates[currentIndex + 1];

			(currentItem.SortOrder, nextItem.SortOrder) = (nextItem.SortOrder, currentItem.SortOrder);

			_DbContext.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult Subtr(int id)
		{
			var cates = _DbContext.AppCategories.OrderBy(x => x.SortOrder).ToList();

			var currentItem = cates.FirstOrDefault(x => x.Id == id);
			int currentIndex = cates.IndexOf(currentItem);

			if (currentIndex == 0)
			{
				// Giữ nguyên nếu là phần tử đầu tiên
				return RedirectToAction("Index");
			}

			var previousItem = cates[currentIndex - 1];

			(currentItem.SortOrder, previousItem.SortOrder) = (previousItem.SortOrder, currentItem.SortOrder);

			_DbContext.SaveChanges();
			return RedirectToAction("Index");
		}

	}
}
