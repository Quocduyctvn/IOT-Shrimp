using AutoMapper;
using EcoShrimp.Admin.Areas.Admin.Controllers.Base;
using EcoShrimp.Admin.Areas.Admin.ViewModels.Requests;
using EcoShrimp.Data;
using EcoShrimp.Share.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using X.PagedList.Extensions;

namespace EcoShrimp.Admin.Areas.Admin.Controllers
{
	public class AdminRequestController : AdminControllerBase
	{
		public AdminRequestController(ApplicationDbContext DbContext, IMapper mapper) : base(DbContext, mapper)
		{
		}

		public IActionResult Index(string keyword, int page = 1, int size = DEFAULT_PAGE_SIZE)
		{
			var request = _DbContext.AppRequests.Where(x => x.Status != RequestStatus.Deleted).AsQueryable();

			if (keyword != null)
			{
				keyword = keyword.Trim().ToUpper();
				request = request.Where(x => x.CompanyName.ToUpper().Contains(keyword) || x.Email.ToUpper().Contains(keyword));
				TempData["searched"] = "searched";
			}

			return View(request.ToPagedList(page, size));
		}

		public IActionResult Details(int id)
		{
			var request = _DbContext.AppRequests.FirstOrDefault(x => x.Id == id);
			return View(request);
		}

		[HttpPost]
		public IActionResult Feedback(FeedbackVM model)
		{
			if (!ModelState.IsValid)
			{
				SetErrorMesg("Dữ liệu không hợp lệ!!");
				return Redirect(Request.Headers["Referer"].ToString() ?? "/");
			}

			var request = _DbContext.AppRequests.FirstOrDefault(x => x.Id == model.IdRequest);

			var Username = "quocduyctvn@gmail.com";
			var Password = "ylya rfag tclg nhae";
			var Host = "smtp.gmail.com";
			var Port = 587;
			var FromEmail = "quocduyctvn@gmail.com";


			MailMessage message = new MailMessage();

			message.From = new MailAddress(FromEmail);                      // nguoi gui 
			message.To.Add(request.Email);                                  // nguoi nhan 
			message.Subject = model.Subject;
			message.IsBodyHtml = true;
			message.Body = model.Body;

			SmtpClient mailClient = new SmtpClient();
			try
			{
				mailClient.EnableSsl = true;

				mailClient.UseDefaultCredentials = false;
				mailClient.Credentials = new System.Net.NetworkCredential(Username, Password);
				mailClient.Host = Host;
				mailClient.Port = Port;

				mailClient.Send(message);

				request.TitleFeedback = model.Subject;
				request.ContentFeedback = model.Body;
				request.Status = RequestStatus.Resolved;
				request.FeedbackDate = DateTime.Now;
				_DbContext.Update(request);
				_DbContext.SaveChanges();
				SetSuccessMesg("Gửi phản hồi thành công!!");
			}
			catch (Exception ex)
			{
				SetErrorMesg("Gửi phản hồi thất bại!!");
			}

			return Redirect(Request.Headers["Referer"].ToString() ?? "/");
		}
	}
}
