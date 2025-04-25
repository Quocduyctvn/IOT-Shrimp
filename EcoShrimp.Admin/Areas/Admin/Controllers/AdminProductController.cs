using AutoMapper;
using EcoShrimp.Admin.Areas.Admin.Controllers.Base;
using EcoShrimp.Admin.Areas.Admin.ViewModels.Products;
using EcoShrimp.Data;
using EcoShrimp.Data.Entities;
using EcoShrimp.Share.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList.Extensions;

namespace EcoShrimp.Admin.Areas.Admin.Controllers
{
	public class AdminProductController : AdminControllerBase
	{
		protected new List<SelectListItem> status;
		public AdminProductController(ApplicationDbContext DbContext, IMapper mapper) : base(DbContext, mapper)
		{
			this.status = new List<SelectListItem>
					{
						new SelectListItem { Value = ((int)Status.Active).ToString(), Text = "Hoạt động" },
						new SelectListItem { Value = ((int)Status.Inactive).ToString(), Text = "Tạm ngưng" }
					}; ;
		}

		public IActionResult Index(string keyword, int page = 1, int size = DEFAULT_PAGE_SIZE)
		{
			var products = _DbContext.AppProducts.Include(x => x.appProInstances)
												.Include(x => x.appImges)
												.Include(x => x.appCategory).Where(x => x.Status != Status.Deleted)
												.OrderBy(x => x.SortOrder).AsQueryable();
			if (keyword != null)
			{
				keyword = keyword.Trim().ToUpper();
				products = products.Where(x => x.Name.ToUpper().Contains(keyword) || x.Desc.ToUpper().Contains(keyword));
				TempData["searched"] = "searched";
			}

			return View(products.ToPagedList(page, size));
		}


		[HttpGet]
		public IActionResult Create()
		{
			ViewBag.status = status;
			ViewBag.cate = _DbContext.AppCategories.Where(x => x.Status != Status.Deleted)
														.Select(x => new SelectListItem
														{
															Value = x.Id.ToString(),
															Text = x.Name
														}).ToList();
			return View();
		}

		[HttpPost]
		public IActionResult Create(ProductVM model, [FromServices] IWebHostEnvironment envi)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.status = status;
				ViewBag.cate = _DbContext.AppCategories.Where(x => x.Status != Status.Deleted)
														.Select(x => new SelectListItem
														{
															Value = x.Id.ToString(),
															Text = x.Name
														}).ToList();
				if (model.FormFile.Count == 0 || model.FormFile.Count > 10)
				{
					SetErrorMesg("Vui lòng chọn ít nhất 1 ảnh & tối đa là 10");
					return View(model);
				}
				var errorMessages = ModelState.Values
						.SelectMany(v => v.Errors)
						.Select(e => e.ErrorMessage)
						.ToList();

				// Ghép tất cả lỗi lại thành một chuỗi, mỗi lỗi trên một dòng
				SetErrorMesg(string.Join("<br>", errorMessages));

				return View(model);

			}

			bool isProductExists = _DbContext.AppProducts.Any(x => x.Name == model.Name);
			if (isProductExists)
			{
				SetErrorMesg("Sản phẩm đã tồn tại");
				return View(model);
			}

			var maxSortOrder = _DbContext.AppProducts
												.DefaultIfEmpty()
												.Max(x => x == null ? 0 : x.SortOrder);
			var product = new AppProducts
			{
				Code = "PD0",
				Name = model.Name,
				Status = (Status)int.Parse(model.Status),
				OriginalPrice = double.TryParse(model.OriginalPrice, out var originalPrice) ? originalPrice : 0,
				SalePrice = double.TryParse(model.SalePrice, out var salePrice) ? salePrice : 0,
				TotalQuantity = int.TryParse(model.Quantity, out var quantity) ? quantity : 0,
				StockQuantity = quantity,
				Desc = model.Desc,
				Summary = model.Summary,
				IdCategory = (int)model.IdCate,
				SortOrder = maxSortOrder + 1,
				CreatedDate = DateTime.Now,
				appImges = new List<AppImges>(), // Tránh lỗi null
				appProInstances = new List<AppProInstances>() // Tránh lỗi null
			};

			_DbContext.AppProducts.Add(product);
			_DbContext.SaveChanges();

			product.Code = $"PD0{product.Id}";
			_DbContext.Update(product);
			_DbContext.SaveChanges();

			// ✅ Xử lý upload ảnh
			if (model.FormFile != null && model.FormFile.Any())
			{
				foreach (var formFile in model.FormFile.Where(f => f != null))
				{
					string adminRootPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "EcoShrimp.Admin", "wwwroot");
					string clientRootPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "EcoShrimp.Client", "wwwroot");

					var file = UploadFileToBothProjects(formFile, adminRootPath, clientRootPath);
					if (!string.IsNullOrEmpty(file)) // Kiểm tra file hợp lệ
					{
						product.appImges.Add(new AppImges { Path = file });
					}
				}
				_DbContext.SaveChanges(); // Lưu ảnh vào DB
			}

			// ✅ Xử lý tạo AppProInstances nếu số lượng > 0
			if (quantity > 0)
			{
				Random random = new Random();
				for (int i = 0; i < quantity; i++)
				{
					// Lấy số thứ tự hiện tại (SequentialNumber)
					var max = _DbContext.AppProInstances
												 .Where(x => x.IdProduct == product.Id)
												 .DefaultIfEmpty()
												 .Max(x => x == null ? 0 : x.SortOrder);

					var sequentialNumber = max + 1;
					var randomCode = $"{(char)random.Next(65, 90)}{random.Next(0, 9)}"; // VD: A7, X2
					var serialNumber = $"{product.Code}-{DateTime.Now:yyyyMMdd}-{sequentialNumber:D4}-{randomCode}";

					int port;
					do
					{
						port = random.Next(5000, 65535); // Chọn ngẫu nhiên trong khoảng 5000 - 65535
					} while (_DbContext.AppProInstances.Any(x => x.Port == port));

					var instance = new AppProInstances
					{
						IdProduct = product.Id, // Liên kết với product
						SeriNumber = serialNumber,
						Name = model.Name + $" - {sequentialNumber:D4}",
						Status = Status.Active,
						SortOrder = sequentialNumber,
						Port = port,
						CreatedDate = DateTime.Now
					};

					_DbContext.AppProInstances.Add(instance);
					_DbContext.SaveChanges(); // Lưu từng instance để cập nhật số lượng
				}
			}

			SetSuccessMesg("Thêm sản phẩm thành công");
			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult Update(int id)
		{
			var product = _DbContext.AppProducts.Include(x => x.appImges)
												.Include(x => x.appProInstances)
												.FirstOrDefault(x => x.Id == id);
			TempData["Cate"] = _DbContext.AppCategories.Where(x => x.Status != Status.Deleted).ToList();
			TempData["Status"] = status;
			if (product == null)
			{
				SetErrorMesg("Sản phẩm không tồn tại");
				return RedirectToAction("Index");
			}
			var ProductVM = new ProductVM();
			ProductVM.Id = product.Id;
			ProductVM.Name = product.Name;
			ProductVM.Status = ((int)product.Status).ToString();
			ProductVM.OriginalPrice = product.OriginalPrice.ToString();
			ProductVM.SalePrice = product.SalePrice.ToString();
			ProductVM.Quantity = product.TotalQuantity.ToString();
			ProductVM.Desc = product.Desc;
			ProductVM.Summary = product.Summary;
			ProductVM.IdCate = product.IdCategory;
			ProductVM.FileString = product.appImges.Select(x => x.Path).ToList();


			return View(ProductVM);
		}

		[HttpPost]
		public IActionResult Update(ProductVM model, [FromServices] IWebHostEnvironment envi)
		{
			ModelState.Remove("FormFile");
			if (!ModelState.IsValid)
			{
				TempData["Cate"] = _DbContext.AppCategories.Where(x => x.Status != Status.Deleted).ToList();
				TempData["Status"] = status;
				var errorMessages = ModelState.Values
						.SelectMany(v => v.Errors)
						.Select(e => e.ErrorMessage)
						.ToList();

				// Ghép tất cả lỗi lại thành một chuỗi, mỗi lỗi trên một dòng
				SetErrorMesg(string.Join("<br>", errorMessages));

				return View(model);
			}

			var product = _DbContext.AppProducts.Include(x => x.appImges)
												.Include(x => x.appProInstances)
												.FirstOrDefault(x => x.Id == model.Id);
			if (product == null)
			{
				SetErrorMesg("Sản phẩm không tồn tại");
				return RedirectToAction("Index");
			}

			product.Name = model.Name;
			product.Status = (Status)int.Parse(model.Status);
			product.OriginalPrice = double.TryParse(model.OriginalPrice, out var originalPrice) ? originalPrice : 0;
			product.SalePrice = double.TryParse(model.SalePrice, out var salePrice) ? salePrice : 0;
			product.Desc = model.Desc;
			product.Summary = model.Summary;
			product.IdCategory = (int)model.IdCate;
			product.UpdatedDate = DateTime.Now;

			if (model.FormFile?.Any() == true)
			{
				// Xóa ảnh cũ nếu có
				if (product.appImges.Any())
				{
					foreach (var img in product.appImges)
					{
						string filePath = Path.Combine(envi.WebRootPath, img.Path);
						if (System.IO.File.Exists(filePath))
						{
							System.IO.File.Delete(filePath);
						}
					}
					_DbContext.RemoveRange(product.appImges);
				}

				// Thêm ảnh mới
				foreach (var formFile in model.FormFile.Where(f => f != null))
				{
					string adminRootPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "EcoShrimp.Admin", "wwwroot");
					string clientRootPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "EcoShrimp.Client", "wwwroot");

					var file = UploadFileToBothProjects(formFile, adminRootPath, clientRootPath);
					if (!string.IsNullOrEmpty(file)) // Kiểm tra file hợp lệ
					{
						product.appImges.Add(new AppImges { Path = file });
					}
				}
			}
			_DbContext.Update(product);
			_DbContext.SaveChanges();
			SetSuccessMesg("Cập nhật sản phẩm thành công");
			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult UpdateQuantity(QuantityVM model)
		{
			var product = _DbContext.AppProducts.Find(model.Id);
			if (product == null)
			{
				SetErrorMesg("Sản phẩm không tồn tại");
				return RedirectToAction("Index");
			}

			product.StockQuantity += model.Quantity;
			product.TotalQuantity += model.Quantity;

			var appProInstances = new List<AppProInstances>();

			Random random = new Random();
			var max = _DbContext.AppProInstances
								.Where(x => x.IdProduct == product.Id)
								.DefaultIfEmpty()
								.Max(x => x == null ? 0 : x.SortOrder);

			var sequentialNumber = max + 1;

			for (int i = 0; i < model.Quantity; i++)
			{
				var serialNumber = $"{product.Code}-{DateTime.Now:yyyyMMdd}-{(sequentialNumber + i):D4}-{(char)random.Next(65, 90)}{random.Next(0, 9)}";

				appProInstances.Add(new AppProInstances
				{
					IdProduct = product.Id,
					SeriNumber = serialNumber,
					Name = product.Name + $" - {(sequentialNumber + i):D4}",
					Status = Status.Active,
					SortOrder = sequentialNumber + i,
					Port = 5000,
					CreatedDate = DateTime.Now
				});
			}

			if (product.appProInstances == null)
			{
				product.appProInstances = new List<AppProInstances>();
			}

			foreach (var instance in appProInstances)
			{
				product.appProInstances.Add(instance);
			}

			_DbContext.Update(product);
			_DbContext.SaveChanges();

			SetSuccessMesg("Thêm phiên bản thành công");
			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult Delete(int id)
		{
			var product = _DbContext.AppProducts.Include(x => x.appProInstances).FirstOrDefault(x => x.Id == id);
			if (product == null)
			{
				SetErrorMesg("Sản phẩm không tồn tại");
				return RedirectToAction("Index");
			}

			return View(product);
		}

		[HttpPost]
		public IActionResult DeleteConfirm(int id)
		{
			var product = _DbContext.AppProducts.Include(x => x.appProInstances).FirstOrDefault(x => x.Id == id);
			if (product == null)
			{
				SetErrorMesg("Sản phẩm không tồn tại");
				return RedirectToAction("Index");
			}

			product.Status = Status.Deleted;
			product.DeletedDate = DateTime.Now;
			_DbContext.Update(product);
			_DbContext.SaveChanges();
			SetSuccessMesg("Xóa sản phẩm thành công");
			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult Detail(int id)
		{
			var proInstan = _DbContext.AppProInstances.Include(x => x.appProducts)
												.ThenInclude(x => x.appCategory)
												.Include(x => x.appFarm)
												.Where(x => x.IdProduct == id & x.Status != Status.Deleted)
												.AsQueryable();
			if (proInstan == null)
			{
				SetErrorMesg("Sản phẩm không tồn tại");
				return RedirectToAction("Index");
			}
			return View(proInstan.ToPagedList());
		}

		public IActionResult DeleteInstance(int id)
		{
			var proInstan = _DbContext.AppProInstances.FirstOrDefault(x => x.Id == id);
			if (proInstan == null)
			{
				SetErrorMesg("Phiên bản sản phẩm không tồn tại");
				return RedirectToAction("Index");
			}
			proInstan.Status = Status.Deleted;
			proInstan.DeletedDate = DateTime.Now;
			_DbContext.Update(proInstan);
			_DbContext.SaveChanges();
			SetSuccessMesg("Xóa phiên bản sản phẩm thành công");
			return RedirectToAction("Detail", new { id = proInstan.IdProduct });
		}
		public IActionResult Plus(int id)
		{
			var products = _DbContext.AppProducts.OrderBy(x => x.SortOrder).ToList();

			var currentItem = products.FirstOrDefault(x => x.Id == id);

			int currentIndex = products.IndexOf(currentItem);
			if (currentIndex == products.Count - 1)
			{
				// Nếu là phần tử cuối cùng, giữ nguyên
				return RedirectToAction("Index");
			}

			var nextItem = products[currentIndex + 1];

			(currentItem.SortOrder, nextItem.SortOrder) = (nextItem.SortOrder, currentItem.SortOrder);

			_DbContext.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult Subtr(int id)
		{
			var products = _DbContext.AppProducts.OrderBy(x => x.SortOrder).ToList();

			var currentItem = products.FirstOrDefault(x => x.Id == id);
			int currentIndex = products.IndexOf(currentItem);

			if (currentIndex == 0)
			{
				// Giữ nguyên nếu là phần tử đầu tiên
				return RedirectToAction("Index");
			}

			var previousItem = products[currentIndex - 1];

			(currentItem.SortOrder, previousItem.SortOrder) = (previousItem.SortOrder, currentItem.SortOrder);

			_DbContext.SaveChanges();
			return RedirectToAction("Index");
		}


		private string UploadFileToBothProjects(IFormFile file, string adminWebRoot, string clientWebRoot)
		{
			var fName = Path.GetFileNameWithoutExtension(file.FileName)
						+ DateTime.Now.Ticks
						+ Path.GetExtension(file.FileName);

			var relativePath = "/images/products/" + fName; // Đường dẫn như cũ

			// Lưu vào thư mục của Admin
			var adminPath = Path.Combine(adminWebRoot, "images", "products", fName);
			Directory.CreateDirectory(Path.GetDirectoryName(adminPath));
			using (var stream = new FileStream(adminPath, FileMode.Create))
			{
				file.CopyTo(stream);
			}

			// Lưu vào thư mục của Client
			var clientPath = Path.Combine(clientWebRoot, "images", "products", fName);
			Directory.CreateDirectory(Path.GetDirectoryName(clientPath));
			using (var stream = new FileStream(clientPath, FileMode.Create))
			{
				file.CopyTo(stream);
			}

			return relativePath; // Trả về đường dẫn tương đối
		}

	}
}
