using Dapr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PredictionService.API.Services;

namespace PredictionService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        private readonly ILogger<TrainingController> _logger;

        private readonly IMLService _mlService;


        public TrainingController(ILogger<TrainingController> logger, IMLService mlService)
        {
            _mlService = mlService;
            _logger = logger;
        }

        [Topic("pubsub", "trainingModel")]
        [Route("training")]
        [HttpPost()]
        public async Task<IActionResult> Predictions(ModelInput model)
        {
            _logger.LogDebug($"data for training at {model.Id} - riceved");
            return Ok(model);
        }
    }
}
