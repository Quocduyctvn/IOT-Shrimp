using EcoShrimp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EcoShrimp.Admin.Controllers.Base
{
	public class AdControllerBase : Controller
	{
		protected readonly ApplicationDbContext _DbContext;  // khai báo protected quy định không cho phép truy cập ngoài lớp - trừ kế thừa 
		protected const int DEFAULT_PAGE_SIZE = 10; // Số phần tử trên 1 trang
		protected const int DEFAULT_PAGE_NUMBER = 1;
		protected const int DEFAULT_PAGE_NUMBER_QUERY_STRING = 1;

		public AdControllerBase(ApplicationDbContext DbContext)
		{
			_DbContext = DbContext;
		}

		public override void OnActionExecuting(ActionExecutingContext context)
		{

		}

		protected void SetErrorMesg(string mesg, bool modelStateIsInvalid = false)
		{
			TempData["Err"] = mesg;
		}
		protected void SetWrnMesg(string mesg, bool modelStateIsInvalid = false)
		{
			TempData["Wrn"] = mesg;
		}

		protected void SetSuccessMesg(string mesg) => TempData["Success"] = mesg;

	}
}
