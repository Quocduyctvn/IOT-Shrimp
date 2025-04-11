using AutoMapper;
using EcoShrimp.Admin.Areas.Admin.Controllers.Base;
using EcoShrimp.Admin.Areas.Admin.ViewModels.Farms;
using EcoShrimp.Data;
using EcoShrimp.Data.Entities;
using EcoShrimp.Share.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList.Extensions;

namespace EcoShrimp.Admin.Areas.Admin.Controllers
{
	public class AdminFarmController : AdminControllerBase
	{
		public AdminFarmController(ApplicationDbContext DbContext, IMapper mapper) : base(DbContext, mapper)
		{
		}

		public IActionResult Index(string keyword, int page = 1, int size = DEFAULT_PAGE_SIZE)
		{
			var products = _DbContext.AppFarms.Include(x => x.appProInstances)
												.Include(x => x.appPonds).Where(x => x.Status != Status.Deleted)
												.OrderBy(x => x.SortOrder).AsQueryable();
			if (keyword != null)
			{
				keyword = keyword.Trim().ToUpper();
				products = products.Where(x => x.FarmName.ToUpper().Contains(keyword) || x.OwnerName.ToUpper().Contains(keyword));
				TempData["searched"] = "searched";
			}

			return View(products.ToPagedList(page, size));
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		public IActionResult Create(FarmVM model, [FromServices] IWebHostEnvironment envi)
		{
			if (!ModelState.IsValid)
			{
				SetErrorMesg("Dữ liệu không hợp lệ!!");
				return View(model);
			}
			bool isPhoneExist = _DbContext.AppFarms.Any(x => x.Phone == model.Phone);
			if (isPhoneExist)
			{
				SetErrorMesg("Số điện thoại đã được đăng ký");
				return View(model);
			}
			var maxSort = _DbContext.AppFarms.DefaultIfEmpty()
										.Max(x => x == null ? 0 : x.SortOrder);
			Random random = new Random();

			var farm = new AppFarms();
			farm.Code = $"{(char)random.Next(65, 90)}{random.Next(0, 9)}";
			farm.OwnerName = model.OwnerName;
			farm.FarmName = model.FarmName;
			farm.Phone = model.Phone;
			farm.Email = model.Email != null ? model.Email : "";
			farm.PassWord = BCrypt.Net.BCrypt.HashPassword(model.PassWord);
			farm.City = model.City != "0" ? model.City : "";
			farm.District = model.District != "0" ? model.District : "";
			farm.Ward = model.Ward != "0" ? model.Ward : "";
			farm.NumberHouse = model.NumberHouse != null ? model.NumberHouse : "";
			farm.Desc = model.Desc != null ? model.Desc : "";
			farm.Status = Status.Active;
			farm.IdTime = 1;
			farm.SortOrder = maxSort + 1;
			farm.CreatedDate = DateTime.Now;
			if (model.FormFile != null)
			{
				string adminRootPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "EcoShrimp.Admin", "wwwroot");
				string clientRootPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "EcoShrimp.Client", "wwwroot");

				farm.Avatar = UploadFileToBothProjects(model.FormFile, adminRootPath, clientRootPath);
			}
			_DbContext.AppFarms.Add(farm);
			_DbContext.SaveChanges();
			SetSuccessMesg("Thêm mới thành công.");
			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult Update(int id)
		{
			var farm = _DbContext.AppFarms.Find(id);
			if (farm == null)
			{
				SetErrorMesg("Không tìm thấy trang trại");
				return RedirectToAction("Index");
			}
			var farmVM = new FarmVM();
			farmVM.OwnerName = farm.OwnerName;
			farmVM.FarmName = farm.FarmName;
			farmVM.Phone = farm.Phone;
			farmVM.Email = farm.Email;
			farmVM.City = farm.City;
			farmVM.District = farm.District;
			farmVM.Ward = farm.Ward;
			farmVM.NumberHouse = farm.NumberHouse;
			farmVM.Desc = farm.Desc;
			farmVM.Avatar = farm.Avatar;
			return View(farmVM);
		}

		[HttpPost]
		public IActionResult Update(FarmVM model, [FromServices] IWebHostEnvironment envi)
		{
			ModelState.Remove("PassWord");
			if (!ModelState.IsValid)
			{
				SetErrorMesg("Dữ liệu không hợp lệ!!");
				return View(model);
			}
			var farm = _DbContext.AppFarms.Find(model.Id);
			if (farm == null)
			{
				SetErrorMesg("Không tìm thấy trang trại");
				return RedirectToAction("Index");
			}

			farm.OwnerName = model.OwnerName;
			farm.FarmName = model.FarmName;
			farm.Phone = model.Phone;
			farm.Email = model.Email != null ? model.Email : "";
			farm.City = model.City != "0" ? model.City : "";
			farm.District = model.District != "0" ? model.District : "";
			farm.Ward = model.Ward != "0" ? model.Ward : "";
			farm.NumberHouse = model.NumberHouse != null ? model.NumberHouse : "";
			farm.Desc = model.Desc != null ? model.Desc : "";
			farm.UpdatedDate = DateTime.Now;
			if (model.FormFile != null)
			{
				if (farm.Avatar != null)
				{
					string filePath = Path.Combine(envi.WebRootPath, farm.Avatar);
					if (System.IO.File.Exists(filePath))
					{
						System.IO.File.Delete(filePath);
					}
				}

				string adminRootPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "EcoShrimp.Admin", "wwwroot");
				string clientRootPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "EcoShrimp.Client", "wwwroot");

				farm.Avatar = UploadFileToBothProjects(model.FormFile, adminRootPath, clientRootPath);
			}
			_DbContext.Update(farm);
			_DbContext.SaveChanges();
			SetSuccessMesg("Cập nhật thành công.");
			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult Delete(int id)
		{
			var farm = _DbContext.AppFarms.Where(x => x.Status != Status.Deleted).FirstOrDefault(x => x.Id == id);
			if (farm == null)
			{
				SetErrorMesg("Không tìm thấy trang trại");
				return RedirectToAction("Index");
			}

			return View(farm);
		}

		[HttpPost]
		public IActionResult DeleteConfirm(int id)
		{
			var farm = _DbContext.AppFarms.Find(id);
			if (farm == null)
			{
				SetErrorMesg("Không tìm thấy trang trại");
				return RedirectToAction("Index");
			}
			farm.Status = Status.Deleted;
			farm.DeletedDate = DateTime.Now;
			_DbContext.Update(farm);
			_DbContext.SaveChanges();
			SetSuccessMesg("Xóa thành công.");
			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult Detail(int id)
		{
			var farm = _DbContext.AppFarms.Include(x => x.appPonds).Include(x => x.appProInstances).FirstOrDefault(x => x.Id == id);
			if (farm == null)
			{
				SetErrorMesg("Không tìm thấy trang trại");
				return RedirectToAction("Index");
			}
			TempData["ProInstances"] = _DbContext.AppProInstances.Include(x => x.appProducts)
												.ThenInclude(x => x.appCategory)
												.Where(x => x.Status == Status.Active).GroupBy(x => x.appProducts.Name).ToList();
			return View(farm);
		}

		public IActionResult AddProInstan(int proInstanId, int farmId)
		{
			var proInstan = _DbContext.AppProInstances.Where(x => x.Status == Status.Active).FirstOrDefault(x => x.Id == proInstanId);
			if (proInstan == null)
			{
				SetErrorMesg("Không tìm thấy sản phẩm");
				return RedirectToAction("Detail", new { id = farmId });
			}
			var farm = _DbContext.AppFarms.FirstOrDefault(x => x.Id == farmId);
			if (farm == null)
			{
				SetErrorMesg("Không tìm thấy trang trại");
				return RedirectToAction("Index");
			}

			proInstan.IdFarm = farm.Id;
			proInstan.Status = Status.Bought;
			proInstan.BuyDate = DateTime.Now;
			_DbContext.Update(proInstan);
			_DbContext.SaveChanges();
			SetSuccessMesg("Thêm Thiết bị cho trang trại thành công.");
			return RedirectToAction("Detail", new { id = farm.Id });
		}

		public IActionResult ConfirmUndo(int id)
		{
			if (id == 0 || id == null)
			{
				SetErrorMesg("Đã xảy ra lỗi trong quá trình xử lí");
				return RedirectToAction("Index");
			}
			var proInstan = _DbContext.AppProInstances.FirstOrDefault(x => x.Id == id);
			if (proInstan == null)
			{
				SetErrorMesg("Không tìm thấy thiết bị");
				return RedirectToAction("Index");
			}
			proInstan.IdFarm = null;
			proInstan.Status = Status.Active;
			proInstan.BuyDate = null;
			_DbContext.Update(proInstan);
			_DbContext.SaveChanges();
			SetSuccessMesg("Thu hồi thiết bị cho trang trại thành công.");
			return RedirectToAction("Detail", new { id = proInstan.IdFarm });
		}
		public IActionResult Plus(int id)
		{
			var farms = _DbContext.AppFarms.OrderBy(x => x.SortOrder).ToList();

			var currentItem = farms.FirstOrDefault(x => x.Id == id);

			int currentIndex = farms.IndexOf(currentItem);
			if (currentIndex == farms.Count - 1)
			{
				// Nếu là phần tử cuối cùng, giữ nguyên
				return RedirectToAction("Index");
			}

			var nextItem = farms[currentIndex + 1];

			(currentItem.SortOrder, nextItem.SortOrder) = (nextItem.SortOrder, currentItem.SortOrder);

			_DbContext.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult Subtr(int id)
		{
			var farms = _DbContext.AppFarms.OrderBy(x => x.SortOrder).ToList();

			var currentItem = farms.FirstOrDefault(x => x.Id == id);
			int currentIndex = farms.IndexOf(currentItem);

			if (currentIndex == 0)
			{
				// Giữ nguyên nếu là phần tử đầu tiên
				return RedirectToAction("Index");
			}

			var previousItem = farms[currentIndex - 1];

			(currentItem.SortOrder, previousItem.SortOrder) = (previousItem.SortOrder, currentItem.SortOrder);

			_DbContext.SaveChanges();
			return RedirectToAction("Index");
		}

		private string UploadFileToBothProjects(IFormFile file, string adminWebRoot, string clientWebRoot)
		{
			var fName = Path.GetFileNameWithoutExtension(file.FileName)
						+ DateTime.Now.Ticks
						+ Path.GetExtension(file.FileName);

			var relativePath = "/images/farm/" + fName; // Đường dẫn như cũ

			// Lưu vào thư mục của Admin
			var adminPath = Path.Combine(adminWebRoot, "images", "farm", fName);
			Directory.CreateDirectory(Path.GetDirectoryName(adminPath));
			using (var stream = new FileStream(adminPath, FileMode.Create))
			{
				file.CopyTo(stream);
			}

			// Lưu vào thư mục của Client
			var clientPath = Path.Combine(clientWebRoot, "images", "farm", fName);
			Directory.CreateDirectory(Path.GetDirectoryName(clientPath));
			using (var stream = new FileStream(clientPath, FileMode.Create))
			{
				file.CopyTo(stream);
			}

			return relativePath; // Trả về đường dẫn tương đối
		}
	}
}
