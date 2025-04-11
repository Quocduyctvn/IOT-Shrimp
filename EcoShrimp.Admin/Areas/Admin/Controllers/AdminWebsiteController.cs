using AutoMapper;
using EcoShrimp.Admin.Areas.Admin.Controllers.Base;
using EcoShrimp.Admin.Areas.Admin.ViewModels.Shrimp;
using EcoShrimp.Data;
using Microsoft.AspNetCore.Mvc;

namespace EcoShrimp.Admin.Areas.Admin.Controllers
{
	public class AdminWebsiteController : AdminControllerBase
	{
		public AdminWebsiteController(ApplicationDbContext DbContext, IMapper mapper) : base(DbContext, mapper)
		{
		}

		public IActionResult Index()
		{
			var shrimp = _DbContext.AppShrimps.FirstOrDefault();
			var shrimpVM = new ShrimpVM
			{
				Id = shrimp.Id,
				WebsiteName = shrimp.WebsiteName,
				Logan = shrimp.Logan,
				LogoUrl = shrimp.LogoUrl,

				Phone = shrimp.Phone,
				SubPhone = shrimp.SubPhone,
				Email = shrimp.Email,
				Address = shrimp.Address,
				Map = shrimp.Map,
				OpentTime = shrimp.OpentTime,

				FacebookUrl = shrimp.FacebookUrl,
				TwitterUrl = shrimp.TwitterUrl,
				InstagramUrl = shrimp.InstagramUrl,
				YouTubeUrl = shrimp.YouTubeUrl
			};
			return View(shrimpVM);
		}


		public IActionResult Update(ShrimpVM model, [FromServices] IWebHostEnvironment envi)
		{
			if (!ModelState.IsValid)
			{
				SetErrorMesg("Vui lòng kiểm tra thông tin!!");
				return View("Index", model);
			}

			var shrimp = _DbContext.AppShrimps.FirstOrDefault(x => x.Id == model.Id);
			if (shrimp == null)
			{
				SetErrorMesg("Đã xảy ra lỗi trong quá trình xử lí!!");
				return View("Index", model);
			}

			shrimp.WebsiteName = model.WebsiteName;
			shrimp.Logan = model.Logan;

			shrimp.Phone = model.Phone;
			shrimp.SubPhone = model.SubPhone;
			shrimp.Email = model.Email;
			shrimp.Address = model.Address;
			shrimp.Map = model.Map;
			shrimp.OpentTime = model.OpentTime;

			shrimp.FacebookUrl = model.FacebookUrl;
			shrimp.TwitterUrl = model.TwitterUrl;
			shrimp.InstagramUrl = model.InstagramUrl;
			shrimp.YouTubeUrl = model.YouTubeUrl;
			if (model.FormFile != null)
			{
				string adminRootPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "EcoShrimp.Admin", "wwwroot");
				string clientRootPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "EcoShrimp.Client", "wwwroot");

				var file = UploadFileToBothProjects(model.FormFile, adminRootPath, clientRootPath);
				if (!string.IsNullOrEmpty(file)) // Kiểm tra file hợp lệ
				{
					shrimp.LogoUrl = file;
				}
			}
			_DbContext.Update(shrimp);
			_DbContext.SaveChanges();
			SetSuccessMesg("Cập nhật thông tin thành công!!");
			return RedirectToAction("Index");
		}


		private string UploadFileToBothProjects(IFormFile file, string adminWebRoot, string clientWebRoot)
		{
			var fName = Path.GetFileNameWithoutExtension(file.FileName)
						+ DateTime.Now.Ticks
						+ Path.GetExtension(file.FileName);

			var relativePath = "/images/logo/" + fName; // Đường dẫn như cũ

			// Lưu vào thư mục của Admin
			var adminPath = Path.Combine(adminWebRoot, "images", "logo", fName);
			Directory.CreateDirectory(Path.GetDirectoryName(adminPath));
			using (var stream = new FileStream(adminPath, FileMode.Create))
			{
				file.CopyTo(stream);
			}

			// Lưu vào thư mục của Client
			var clientPath = Path.Combine(clientWebRoot, "images", "logo", fName);
			Directory.CreateDirectory(Path.GetDirectoryName(clientPath));
			using (var stream = new FileStream(clientPath, FileMode.Create))
			{
				file.CopyTo(stream);
			}

			return relativePath; // Trả về đường dẫn tương đối
		}
	}
}
