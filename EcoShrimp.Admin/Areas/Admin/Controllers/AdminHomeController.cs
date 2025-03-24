using Microsoft.AspNetCore.Mvc;

namespace EcoShrimp.Admin.Areas.Admin.Controllers
{
	public class AdminHomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
