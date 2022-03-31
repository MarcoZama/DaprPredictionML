using Dapr.Client;

public class Program
{
    protected static readonly DaprClient _daprClient;

    public static async void Main(string[] args)
    {
        for (int i = 0; i < 10; i++)
        {
            var input = new ModelInput()
            {

            };

            await _daprClient.PublishEventAsync("pubsub", "predictions", input);
        }
    }
}