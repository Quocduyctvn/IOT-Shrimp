using EcoShrimp.API.Model;
using Microsoft.AspNetCore.Mvc;

namespace EcoShrimp.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PredictionController : ControllerBase
	{
		private static WaterQualityPredictor _predictor;

		public PredictionController()
		{
		}

		[HttpGet("predict")]
		public IActionResult Predict([FromQuery] float ph, [FromQuery] float tds, [FromQuery] float temp)
		{
			if (_predictor == null)
				return StatusCode(500, "⚠️ Mô hình chưa được huấn luyện. Vui lòng train trước.");

			if (ph <= 0 || tds <= 0 || temp <= 0)
				return BadRequest("❌ Giá trị cảm biến không hợp lệ (ph, tds, temp phải > 0)");

			var result = _predictor.Predict(ph, tds, temp);

			return Ok(new
			{
				pH = ph,
				TDS = tds,
				Temperature = temp,
				PredictedQuality = result
			});
		}

	}
}
