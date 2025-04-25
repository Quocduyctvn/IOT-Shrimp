using AutoMapper;
using EcoShrimp.Client.Areas.Client.Controllers.Base;
using EcoShrimp.Data;
using EcoShrimp.Data.Entities;
using EcoShrimp.Share.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Security.Claims;
using X.PagedList.Extensions;

namespace EcoShrimp.Client.Areas.Client.Controllers
{
	public class ClientHistoryController : ClientControllerBase
	{
		public ClientHistoryController(ApplicationDbContext DbContext, IMapper mapper) : base(DbContext, mapper)
		{
		}

		public IActionResult Index()
		{
			ClaimsIdentity identity = (ClaimsIdentity)User.Identity!;
			var IdClaim = identity.FindFirst("ID")?.Value;
			int IdFarm = int.Parse(IdClaim);
			var ponds = _DbContext.AppPonds.Where(x => x.IdFarm == IdFarm && x.Status != Status.Deleted)
											.Include(x => x.appSeasons)
											.ThenInclude(x => x.appConnects).ToList();

			return View(ponds);
		}

		public IActionResult Detail(int id)
		{
			if (id <= 0)
			{
				SetErrorMesg("Truy cập không hợp lệ!!");
				return RedirectToAction("Index");
			}

			var seasons = _DbContext.AppSeasons.Where(x => x.IdPond == id)
												.Include(x => x.appPond)
												.Include(x => x.appConnects)
												.ThenInclude(x => x.appDataSensors)
												.Include(x => x.appConnects)
												.ThenInclude(x => x.appProInstances)
												.AsQueryable();
			return View(seasons.ToPagedList());
		}

		public IActionResult ExportExcel(int id)
		{
			if (id <= 0)
			{
				SetErrorMesg("Đã xẩy ra lỗi trong quá trình xử lí!!");
				return Redirect(Request.Headers["Referer"].ToString() ?? "/");
			}
			var data = _DbContext.AppDataSensor.Where(x => x.IdConnect == id)
													.Include(x => x.appConnect)
													.ThenInclude(x => x.appProInstances)
													.ThenInclude(x => x.appFarm)
													.Include(x => x.appConnect)
													.ThenInclude(x => x.appSeasons)
													.ThenInclude(x => x.appPond)
													.ThenInclude(x => x.appFarm)
													.ToList();


			var dataSensor = data.Select(b => new
			{
				b.PH, // Ngày nhận phòng
				b.Temp, // Ngày trả phòng
				b.Tds,           // Mã đặt phòng, // Tên phòng
				b.CreatedDate // Loại phòng
			}).Cast<dynamic>().ToList();

			return GenerateExcelFile(dataSensor, data);
		}


		private IActionResult GenerateExcelFile(List<dynamic> dataSensor, List<AppDataSensor> model)
		{
			using (var package = new ExcelPackage())
			{
				var worksheet = package.Workbook.Worksheets.Add("Export Data sensor");
				string websiteName = "Hệ thống giám sát môi trường ao nuôi";
				string farmName = "Trang trại: " + model.FirstOrDefault().appConnect.appSeasons.appPond.appFarm.FarmName;
				string pondName = "Ao nuôi: " + model.FirstOrDefault().appConnect.appSeasons.appPond.Name;
				string deviceName = "Thiết bị: " + model.FirstOrDefault().appConnect.appProInstances.Name;
				string exportDate = $"Ngày xuất file: {DateTime.Now:dd/MM/yyyy HH:mm}";


				// Dòng 3 - Tiêu đề lớn
				worksheet.Cells["A3:E3"].Merge = true;
				worksheet.Cells["A3"].Value = "THỐNG KÊ DỮ LIỆU QUAN TRẮC MÔI TRƯỜNG AO NUÔI";
				worksheet.Cells["A3"].Style.Font.Bold = true;
				worksheet.Cells["A3"].Style.Font.Size = 14;
				worksheet.Cells["A3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

				// Dòng 4 - Website
				worksheet.Cells["A4:E4"].Merge = true;
				worksheet.Cells["A4"].Value = websiteName;
				worksheet.Cells["A4"].Style.Font.Bold = true;
				worksheet.Cells["A4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

				// Dòng 5 - Trang trại, Ao, Thiết bị
				worksheet.Cells["A5:E5"].Merge = true;
				worksheet.Cells["A5"].Value = $"{farmName} | {pondName} | {deviceName}";
				worksheet.Cells["A5"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

				// Dòng 6 - Ngày xuất
				worksheet.Cells["A6:E6"].Merge = true;
				worksheet.Cells["A6"].Value = exportDate;
				worksheet.Cells["A6"].Style.Font.Italic = true;
				worksheet.Cells["A6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;


				// Định dạng cho các ô header
				worksheet.Cells[1, 1, 6, 1].Style.Font.Bold = true;
				worksheet.Cells[1, 1, 6, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
				worksheet.Cells[1, 1, 6, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
				worksheet.Cells[1, 1, 6, 1].Style.WrapText = true;

				// Thêm tiêu đề cột cho bảng dữ liệu
				// Tiêu đề bảng
				worksheet.Cells[8, 1].Value = "STT";
				worksheet.Cells[8, 2].Value = "Ph";
				worksheet.Cells[8, 3].Value = "Nhiệt độ (Temp)";
				worksheet.Cells[8, 4].Value = "Tổng chất rắn hòa tan (Tds)";
				worksheet.Cells[8, 5].Value = "Thời gian";

				// Định dạng tiêu đề
				var headerRange = worksheet.Cells[8, 1, 8, 5];
				headerRange.Style.Font.Bold = true;
				headerRange.Style.Font.Size = 14;
				headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
				headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
				headerRange.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
				headerRange.Style.Border.BorderAround(ExcelBorderStyle.Thin);
				headerRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
				headerRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
				headerRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
				headerRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

				worksheet.Row(8).Height = 25;

				// Thêm dữ liệu vào bảng
				for (int i = 0; i < dataSensor.Count; i++)
				{
					var item = dataSensor[i];
					worksheet.Cells[i + 9, 1].Value = i + 1;
					worksheet.Cells[i + 9, 2].Value = item.PH;
					worksheet.Cells[i + 9, 3].Value = item.Temp ?? "null";
					worksheet.Cells[i + 9, 4].Value = item.Tds ?? "null";
					worksheet.Cells[i + 9, 5].Value = item.CreatedDate?.ToString("dd/MM/yyyy HH:mm:ss") ?? "";

					// Định dạng từng dòng dữ liệu
					var dataRow = worksheet.Cells[i + 9, 1, i + 9, 4];
					dataRow.Style.Font.Size = 14;
					dataRow.Style.Border.Top.Style = ExcelBorderStyle.Thin;
					dataRow.Style.Border.Left.Style = ExcelBorderStyle.Thin;
					dataRow.Style.Border.Right.Style = ExcelBorderStyle.Thin;
					dataRow.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
					dataRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
				}


				// Xác định vùng dữ liệu (theo số lượng dòng dữ liệu thực tế)
				int dataStartRow = 9;
				int dataEndRow = dataStartRow + dataSensor.Count - 1;

				// Tạo biểu đồ đường
				var chart = worksheet.Drawings.AddChart("LineChart", eChartType.Line) as ExcelLineChart;
				chart.Title.Text = "Biểu đồ chất lượng nước";
				chart.SetPosition(7, 0, 6, 0); // Bắt đầu tại hàng 9, cột G
				chart.SetSize(800, 400); // Kích thước biểu đồ

				// Series PH
				var phSeries = chart.Series.Add(
					worksheet.Cells[$"B{dataStartRow}:B{dataEndRow}"], // Cột PH
					worksheet.Cells[$"E{dataStartRow}:E{dataEndRow}"]  // Trục X: Thời gian
				);
				phSeries.Header = "pH";

				// Series Nhiệt độ
				var tempSeries = chart.Series.Add(
					worksheet.Cells[$"C{dataStartRow}:C{dataEndRow}"],
					worksheet.Cells[$"E{dataStartRow}:E{dataEndRow}"]
				);
				tempSeries.Header = "Nhiệt độ";

				// Series TDS
				var tdsSeries = chart.Series.Add(
					worksheet.Cells[$"D{dataStartRow}:D{dataEndRow}"],
					worksheet.Cells[$"E{dataStartRow}:E{dataEndRow}"]
				);
				tdsSeries.Header = "TDS";

				// Cấu hình trục
				chart.XAxis.Title.Text = "Thời gian";
				chart.YAxis.Title.Text = "Giá trị";
				chart.Legend.Position = eLegendPosition.Right;


				// Auto-fit và căn giữa toàn bộ cột
				worksheet.Cells[8, 1, dataSensor.Count + 8, 4].AutoFitColumns();

				worksheet.Cells.AutoFitColumns();

				var stream = new MemoryStream(package.GetAsByteArray());
				string fileName = $"Du_lieu_quan_trac_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
				return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
			}
		}

	}
}
