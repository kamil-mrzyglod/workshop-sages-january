using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;

namespace Workshop.DurableFunctions
{
    public static class Orchestration2
    {
        [FunctionName("OrchestrationStart2")]
        public static async Task<HttpResponseMessage> Start(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestMessage input,
            [OrchestrationClient] DurableOrchestrationClient starter)
        {
            var message = await input.Content.ReadAsStringAsync();
            var orchestration = await starter.StartNewAsync("HelloWorld2", message);

            return starter.CreateCheckStatusResponse(input, orchestration);
        }

        [FunctionName("HelloWorld2")]
        public static async Task<string> Run(
            [OrchestrationTrigger] DurableOrchestrationContext context)
        {
            var name = JsonConvert.DeserializeObject<InputModel>(context.GetInput<string>()).Name;
            var result = await context.CallActivityAsync<string>("SayHello2", name);
            return result;
        }

        [FunctionName("SayHello2")]
        public static string SayHello([ActivityTrigger] DurableActivityContext helloContext)
        {
            var name = helloContext.GetInput<string>();
            return $"Hello {name}!";
        }
    }
}
