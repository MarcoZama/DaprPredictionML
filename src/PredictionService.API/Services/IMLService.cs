using Microsoft.ML;
using PredictionService.Console.Models;

namespace PredictionService.API.Services
{
    public interface IMLService
    {
        ModelOutput Predict(ModelInput input);

        IEstimator<ITransformer> BuildPipeline(MLContext mlContext);

        ITransformer RetrainPipeline(MLContext context, IDataView trainData);

    }
}
