using EcoShrimp.Admin.Controllers.Base;
using EcoShrimp.Admin.Models;
using EcoShrimp.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EcoShrimp.Admin.Controllers
{
	public class HomeController : AdControllerBase
	{
		public HomeController(ApplicationDbContext DbContext) : base(DbContext)
		{
		}

		public IActionResult Index()
		{
			SetWrnMesg("Tên ti?n nghi là b?t bu?t");
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
