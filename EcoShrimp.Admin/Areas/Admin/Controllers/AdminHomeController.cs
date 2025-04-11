using AutoMapper;
using EcoShrimp.Admin.Areas.Admin.Controllers.Base;
using EcoShrimp.Data;
using Microsoft.AspNetCore.Mvc;

namespace EcoShrimp.Admin.Areas.Admin.Controllers
{
	public class AdminHomeController : AdminControllerBase
	{
		public AdminHomeController(ApplicationDbContext DbContext, IMapper mapper) : base(DbContext, mapper)
		{
		}

		public IActionResult Index()
		{
			var shrimp = _DbContext.AppShrimps.FirstOrDefault();
			return View(shrimp);
		}
	}
}
