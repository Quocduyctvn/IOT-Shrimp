using Microsoft.ML;
using Microsoft.ML.Data;

namespace EcoShrimp.API.Model
{
	public class WaterQualityTrainer
	{
		private readonly MLContext _mlContext;

		public WaterQualityTrainer()
		{
			_mlContext = new MLContext(seed: 0);
		}

	}

	public class WaterQualityPrediction
	{
		public float water_pH { get; set; }
		public float TDS { get; set; }
		public float water_temp { get; set; }
		public string quality { get; set; }
	}


	public class WaterQualityPredictor
	{
		private readonly PredictionEngine<WaterQualityPrediction, WaterQualityOutput> _predictionEngine;

		public WaterQualityPredictor(string modelPath)
		{
			var mlContext = new MLContext();
			ITransformer loadedModel = mlContext.Model.Load(modelPath, out var modelInputSchema);

			_predictionEngine = mlContext.Model.CreatePredictionEngine<WaterQualityPrediction, WaterQualityOutput>(loadedModel);
		}

		public string Predict(float ph, float tds, float temp)
		{
			var input = new WaterQualityPrediction
			{
				water_pH = ph,
				TDS = tds,
				water_temp = temp // water_pH,TDS,water_temp
			};

			if ((ph >= 6.5 && ph <= 8.5) && (tds >= 300 && tds <= 700) && (temp >= 28 && temp <= 32))
			{
				return "good";
			}
			else if (
				(ph >= 6.0 && ph < 6.5) || (ph > 8.5 && ph <= 9.0) ||
				(tds > 100 && tds < 300) || (tds > 700 && tds <= 1000) ||
				(temp >= 26 && temp < 28) || (temp > 32 && temp <= 33)
			)
			{
				return "medium";
			}
			else
			{
				return "bad";
			}
		}
	}
	public class WaterQualityOutput
	{
		public string PredictedLabel { get; set; }
	}

}
