using Microsoft.ML;
using Microsoft.ML.Trainers.FastTree;
using PredictionService.Console.Models;

namespace PredictionService.API.Services
{
    public class MLService : IMLService
    {
        private readonly ILogger<MLService> _logger;

        private static string MLNetModelPath = Path.GetFullPath("MLModel\\SampleClassification.zip");

        public static readonly Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(() => CreatePredictEngine(), true);


        public MLService(ILogger<MLService> logger)
        {
            _logger = logger;
        }


        /// <summary>
        /// Predict method
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ModelOutput Predict(ModelInput input)
        {
            var predEngine = PredictEngine.Value;
            return predEngine.Predict(input);
        }
           
        /// <summary>
        /// Method to build pipeline
        /// </summary>
        /// <param name="mlContext"></param>
        /// <returns></returns>
        public IEstimator<ITransformer> BuildPipeline(MLContext mlContext)
        {
            // Data process configuration with pipeline data transformations
            var pipeline = mlContext.Transforms.ReplaceMissingValues(new[] { 
                                        new InputOutputColumnPair(@"id", @"id"), 
                                        new InputOutputColumnPair(@"Compression1", @"Compression1"), 
                                        new InputOutputColumnPair(@"Compression2", @"Compression2"), 
                                        new InputOutputColumnPair(@"Wait_Time", @"Wait_Time"), 
                                        new InputOutputColumnPair(@"Withdrawal1", @"Withdrawal1"), 
                                        new InputOutputColumnPair(@"Withdrawal2", @"Withdrawal2"), 
                                        new InputOutputColumnPair(@"adhesiveness", @"adhesiveness"), 
                                        new InputOutputColumnPair(@"chewiness", @"chewiness"), 
                                        new InputOutputColumnPair(@"cohesiveness", @"cohesiveness"), 
                                        new InputOutputColumnPair(@"depth", @"depth"), 
                                        new InputOutputColumnPair(@"externalHumidity", @"externalHumidity"),
                                        new InputOutputColumnPair(@"externalTemperature", @"externalTemperature"), 
                                        new InputOutputColumnPair(@"fracturability", @"fracturability"), 
                                        new InputOutputColumnPair(@"gumminess", @"gumminess"), 
                                        new InputOutputColumnPair(@"hardness", @"hardness"), 
                                        new InputOutputColumnPair(@"height", @"height"), 
                                        new InputOutputColumnPair(@"idRecipe", @"idRecipe"), 
                                        new InputOutputColumnPair(@"internalHumidity", @"internalHumidity"), 
                                        new InputOutputColumnPair(@"internalTemperature", @"internalTemperature"),
                                        new InputOutputColumnPair(@"referencePLC", @"referencePLC"), 
                                        new InputOutputColumnPair(@"resilience", @"resilience"), 
                                        new InputOutputColumnPair(@"springiness", @"springiness"), 
                                        new InputOutputColumnPair(@"volume", @"volume"), 
                                        new InputOutputColumnPair(@"weight", @"weight"), 
                                        new InputOutputColumnPair(@"width", @"width") })
                                    .Append(mlContext.Transforms.Concatenate(@"Features", new[] { @"id", @"Compression1", @"Compression2", @"Wait_Time", @"Withdrawal1", @"Withdrawal2", @"adhesiveness", @"chewiness", @"cohesiveness", @"depth", @"externalHumidity", @"externalTemperature", @"fracturability", @"gumminess", @"hardness", @"height", @"idRecipe", @"internalHumidity", @"internalTemperature", @"referencePLC", @"resilience", @"springiness", @"volume", @"weight", @"width" }))
                                    .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: @"qualityScore", inputColumnName: @"qualityScore"))
                                    .Append(mlContext.MulticlassClassification.Trainers.OneVersusAll(binaryEstimator: mlContext.BinaryClassification.Trainers.FastTree(new FastTreeBinaryTrainer.Options() { NumberOfLeaves = 4, MinimumExampleCountPerLeaf = 20, NumberOfTrees = 4, MaximumBinCountPerFeature = 256, FeatureFraction = 1, LearningRate = 0.1, LabelColumnName = @"qualityScore", FeatureColumnName = @"Features" }), labelColumnName: @"qualityScore"))
                                    .Append(mlContext.Transforms.Conversion.MapKeyToValue(outputColumnName: @"PredictedLabel", inputColumnName: @"PredictedLabel"));

            return pipeline;
        }


        /// <summary>
        /// MEthod for retrain the pipeline
        /// </summary>
        /// <param name="context"></param>
        /// <param name="trainData"></param>
        /// <returns></returns>
        public ITransformer RetrainPipeline(MLContext context, IDataView trainData)
        {
            var pipeline = BuildPipeline(context);
            var model = pipeline.Fit(trainData);

            return model;
        }

        private static PredictionEngine<ModelInput, ModelOutput> CreatePredictEngine()
        {
            var mlContext = new MLContext();
            ITransformer mlModel = mlContext.Model.Load(MLNetModelPath, out var _);
            return mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
        }
    }
}
