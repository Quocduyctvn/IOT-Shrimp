using AutoMapper;
using EcoShrimp.Client.Controllers.Base;
using EcoShrimp.Client.ViewModels;
using EcoShrimp.Client.ViewModels.Request;
using EcoShrimp.Data;
using EcoShrimp.Data.Entities;
using EcoShrimp.Share.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Mail;

namespace EcoShrimp.Client.Controllers
{
	public class HomeController : ShrimpControllerBase
	{
		public HomeController(ApplicationDbContext DbContext, IMapper mapper) : base(DbContext, mapper)
		{
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Contact(RequestVM model)
		{
			if (!ModelState.IsValid)
			{
				var errors = ModelState.Values
					.SelectMany(v => v.Errors)
					.Select(e => e.ErrorMessage)
					.ToList();

				SetErrorMesg(string.Join("\n", errors)); // Gộp tất cả lỗi thành một chuỗi

				return Redirect(Request.Headers["Referer"].ToString() ?? "/");
			}

			// Tạo đối tượng yêu cầu
			var request = new AppRequests
			{
				CompanyName = model.CompanyName,
				Email = model.Email,
				Phone = model.Phone,
				Address = model.Address,
				Content = model.Content,
				CreatedDate = DateTime.Now,
				Status = RequestStatus.Pending
			};

			// Lưu vào Cơ sở dữ liệu
			_DbContext.Add(request);
			_DbContext.SaveChanges();

			// Nội dung email
			string content = $@"
							<div class=""container bg-secondary d-flex justify-content-center"">
								<div class=""bg-light mt-1 rounded-2 p-3"" style=""width: 1200px"">
									<div class=""row px-4"">
										<div class=""col p-0"">
											<div class=""row"">Thân gửi {model.CompanyName},</div>
											<div class=""row"">
												<span class=""p-0 col-auto fw-bold"" style=""color:#c88321"">Booking.com</span>
												<div class=""col-auto ps-1"">vừa nhận được một Nộ dung liên hệ từ của bạn trên <span style=""color:#c88321"">EcoShrimp.com</span></div>
											</div>
											<div class=""row mt-2""><i class=""p-0"">Thông tin liên hệ:</i></div>
											<div class=""row ms-3"">Tên khách hàng: {model.CompanyName}</div>
											<div class=""row ms-3"">Email: {model.Email}</div>
											<div class=""row ms-3"">Địa chỉ: {model.Address}</div>
											<div class=""row ms-3"">Nội dung liên hệ: {model.Content}</div>
											<div class=""row mt-2"">
												Chúng tôi đã nhận được thông tin liên hệ của bạn và sẽ xử lý nhanh chóng. Đội ngũ của chúng tôi sẽ liên hệ lại với bạn trong thời gian sớm nhất để hỗ trợ và giải đáp mọi thắc mắc. Chúng tôi rất trân trọng sự tin tưởng và hợp tác của bạn. Cảm ơn bạn đã lựa chọn dịch vụ của chúng tôi!
											</div>
											<div class=""row mt-2"">Trân trọng cảm ơn,</div>
											<div class=""row"">EcoShrimp.com</div>
										</div>
									</div>
								</div>
							</div>
							";

			// Thông tin người gửi email
			var username = "quocduyctvn@gmail.com";
			var password = "ylya rfag tclg nhae";
			var host = "smtp.gmail.com";
			var port = 587;
			var fromEmail = "quocduyctvn@gmail.com";

			// Tạo đối tượng MailMessage để gửi mail
			MailMessage message = new MailMessage();
			message.From = new MailAddress(fromEmail); // Người gửi email
			message.To.Add(model.Email); // Người nhận mail (từ model, có thể là khách hàng)
			message.Subject = "Yêu cầu liên hệ của khách hàng trên EcoShrimp.com";
			message.IsBodyHtml = true;
			message.Body = content;

			// Tạo đối tượng SmtpClient để cấu hình gửi mail
			SmtpClient mailClient = new SmtpClient
			{
				EnableSsl = true,
				UseDefaultCredentials = false,
				Credentials = new System.Net.NetworkCredential(username, password),
				Host = host,
				Port = port
			};

			// Gửi email
			try
			{
				mailClient.Send(message);
				SetSuccessMesg("Liên hệ của bạn đã được gửi đi thành công!");
			}
			catch (Exception ex)
			{
				SetErrorMesg($"Có lỗi xảy ra trong quá trình gửi: {ex.Message}");
			}

			return Redirect(Request.Headers["Referer"].ToString() ?? "/");
		}




		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
