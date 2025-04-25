using EcoShrimp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Globalization;
using System.Net.Mail;

namespace EcoShrimp.Client.Services
{
	public class TimedHostedService : BackgroundService
	{
		private readonly IServiceScopeFactory _scopeFactory;
		private readonly ILogger<TimedHostedService> _logger;
		private Timer _timer;

		public TimedHostedService(ILogger<TimedHostedService> logger, IServiceScopeFactory scopeFactory)
		{
			_logger = logger;
			_scopeFactory = scopeFactory;
		}

		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			ScheduleNextRun();
			return Task.CompletedTask;
		}

		private void ScheduleNextRun()
		{
			var now = DateTime.UtcNow;

			// Tính thời điểm: phút kế tiếp + 5 giây
			var nextMinutePlus5Sec = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0)
				.AddMinutes(1)
				.AddSeconds(5);

			var delay = nextMinutePlus5Sec - now;

			_timer = new Timer(DoWork, null, delay, Timeout.InfiniteTimeSpan);
		}

		private void DoWork(object state)
		{
			_ = DoWorkAsync(); // Fire-and-forget
		}

		private async Task DoWorkAsync()
		{
			try
			{
				using var scope = _scopeFactory.CreateScope();
				var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

				var now = DateTime.Now;
				var startOfMinute = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
				var endOfMinute = startOfMinute.AddMinutes(1);

				var newRecords = dbContext.AppDataSensor
					.Include(x => x.appConnect)
					.ThenInclude(x => x.appProInstances)
					.ThenInclude(x => x.appFarm)
					.Include(x => x.appConnect)
					.ThenInclude(x => x.appSeasons)
					.ThenInclude(x => x.appPond)
					.Where(x => x.CreatedDate.HasValue &&
								x.CreatedDate >= startOfMinute &&
								x.CreatedDate < endOfMinute &&
								x.appConnect.appProInstances.appFarm.IsNotify == true)
					.ToList();

				var handler = new HttpClientHandler()
				{
					ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
				};
				using var httpClient = new HttpClient(handler);
				httpClient.BaseAddress = new Uri("https://localhost:44382");

				foreach (var record in newRecords)
				{
					var farm = dbContext.AppFarms.FirstOrDefault(x => x.Id == record.appConnect.appProInstances.appFarm.Id);

					if (farm != null && !string.IsNullOrEmpty(farm.Email))
					{
						string url = $"/api/Prediction/predict?ph={record.PH}&tds={record.Tds?.ToString(CultureInfo.InvariantCulture)}&temp={record.Temp?.ToString(CultureInfo.InvariantCulture)}";
						if (record.PH > 0 && record.Temp > 0 && record.Tds >= 0)
						{
							try
							{
								var response = await httpClient.GetAsync(url);

								if (response.IsSuccessStatusCode)
								{
									var predictionJson = await response.Content.ReadAsStringAsync();
									Console.WriteLine($"✅ Prediction for {farm.Email}: {predictionJson}");

									// Deserialize JSON to C# object
									var predictionResult = JsonConvert.DeserializeObject<PredictionResult>(predictionJson);

									var quantity = "";

									await SendWaterQualityReportEmail(farm.Email, record.appConnect.appSeasons.appPond.Name, (double)record.PH, (double)record.Tds, (double)record.Temp, quantity);
									string message =
										".........";
									string formattedPhone = farm.Phone;
									await SendSmsAsync(formattedPhone, message);
								}
								else
								{
									Console.WriteLine($"❌ Lỗi gọi API: {response.StatusCode}");
								}
							}
							catch (Exception ex)
							{
								Console.WriteLine($"❗ Exception: {ex.Message}");
								throw new ApplicationException("Lỗi khi gọi API dự đoán chất lượng nước.", ex);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Lỗi trong DoWorkAsync");
			}
			finally
			{
				ScheduleNextRun();
			}
		}


		public override Task StopAsync(CancellationToken stoppingToken)
		{
			_timer?.Change(Timeout.Infinite, 0);
			return base.StopAsync(stoppingToken);
		}

		public async Task SendSmsAsync([FromQuery] string phone, [FromQuery] string message)
		{
			var smsService = new SmsService();
			await smsService.SendSmsAsync(phone, message);
		}

		public async Task SendWaterQualityReportEmail(string email, string name, double ph, double tds, double temperature, string quality)
		{
			var username = ".........";
			var password = ".........";
			var host = "smtp.gmail.com";
			var port = 587;
			var fromEmail = ".........";

			// Tạo đối tượng MailMessage để gửi mail
			MailMessage message = new MailMessage();
			message.From = new MailAddress(fromEmail);
			message.To.Add(email);
			message.Subject = "📊 Báo cáo chất lượng nước từ EcoShrimp.com";
			message.IsBodyHtml = true;
			message.Body = $@".........";

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
			}
			catch (Exception ex)
			{
				Console.WriteLine($"❗ Lỗi khi gửi email: {ex.Message}");
			}
		}
	}

	public class PredictionResult
	{
		public float pH { get; set; }
		public float Tds { get; set; }
		public float Temperature { get; set; }
		public string PredictedQuality { get; set; }
	}

}
