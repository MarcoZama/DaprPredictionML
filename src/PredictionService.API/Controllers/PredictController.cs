using Dapr;
using Microsoft.AspNetCore.Mvc;

namespace PredictionService.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PredictController : ControllerBase
{
  

    private readonly ILogger<PredictController> _logger;

    public PredictController(ILogger<PredictController> logger)
    {
        _logger = logger;
    }


    [Topic("pubsub", "predictions")]
    [Route("Predictions")]
    [HttpPost()]
    public async Task<IActionResult> Predictions(ModelInput model)
    {
        _logger.LogDebug($"{model.Name} - {model.Humidity}");
       return Ok(model);
    }
}
