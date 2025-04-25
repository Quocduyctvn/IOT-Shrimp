using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace EcoShrimp.Client.Services
{
	public class SmsService
	{
		private static readonly string ApiUrl = ".........";
		private static readonly string ApiKey = ".............";

		public async Task SendSmsAsync(string phoneNumber, string message)
		{
			using var client = new HttpClient();

			// Gán Header Authorization kiểu Basic
			var authValue = Convert.ToBase64String(Encoding.ASCII.GetBytes($"apikey:{ApiKey}"));
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authValue);

			// Tạo nội dung JSON
			var requestData = new[]
			{
				new { mobile = phoneNumber, text = message }
			};

			var json = JsonSerializer.Serialize(requestData);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			try
			{
				var response = await client.PostAsync(ApiUrl, content);

				if (response.IsSuccessStatusCode)
				{
					var responseContent = await response.Content.ReadAsStringAsync();
					Console.WriteLine($"✅ Gửi thành công: {responseContent}");
				}
				else
				{
					Console.WriteLine($"❌ Gửi thất bại: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"⚠️ Lỗi khi gọi API: {ex.Message}");
			}
		}
	}
}