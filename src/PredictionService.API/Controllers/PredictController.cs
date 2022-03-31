using Dapr;
using Microsoft.AspNetCore.Mvc;
using PredictionService.API.Services;

namespace PredictionService.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PredictController : ControllerBase
{
    private readonly ILogger<PredictController> _logger;

    private readonly IMLService _mlService;

    public PredictController(ILogger<PredictController> logger, IMLService mlService)
    {
        _mlService = mlService;
        _logger = logger;
    }


    [Topic("pubsub", "predictions")]
    [Route("Predictions")]
    [HttpPost()]
    public async Task<IActionResult> Predictions(ModelInput model)
    {
        _logger.LogDebug($"data for {model.Id} - riceved");
       return Ok(model);
    }
}
