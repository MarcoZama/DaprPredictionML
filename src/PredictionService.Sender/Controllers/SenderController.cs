using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using PredictionService.Sender.Models;

namespace PredictionService.Sender.Controllers;

[ApiController]
[Route("[controller]")]
public class SenderController : ControllerBase
{
    private readonly ILogger<SenderController> _logger;

    public SenderController(ILogger<SenderController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetPrediction")]
    public async Task<IActionResult> GetPrediction([FromServices] DaprClient daprClient)
    {
        for(int i = 0;i<50; i++)
        {
            var input = new ModelInput
            {
                Name = "nome",
                Humidity = 10+i
            };
            await daprClient.PublishEventAsync("pubsub", "predictions", input);
        }       
       
        return Ok();
    }
}
