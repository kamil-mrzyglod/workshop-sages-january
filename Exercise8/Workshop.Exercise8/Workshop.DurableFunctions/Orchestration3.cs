using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;

namespace Workshop.DurableFunctions
{
    public static class Orchestration3
    {
        [FunctionName("OrchestrationStart3")]
        public static async Task<HttpResponseMessage> Start(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestMessage input,
            [OrchestrationClient] DurableOrchestrationClient starter)
        {
            var message = await input.Content.ReadAsStringAsync();
            var orchestration = await starter.StartNewAsync("HelloWorld3", message);

            return starter.CreateCheckStatusResponse(input, orchestration);
        }

        [FunctionName("HelloWorld3")]
        public static async Task<string> Run(
            [OrchestrationTrigger] DurableOrchestrationContext context)
        {
            var name = JsonConvert.DeserializeObject<InputModel>(context.GetInput<string>()).Name;
            var result = await context.CallActivityWithRetryAsync<string>("SayHello3", new RetryOptions(TimeSpan.FromMilliseconds(100), 5), name);
            return result;
        }

        [FunctionName("SayHello3")]
        public static string SayHello([ActivityTrigger] DurableActivityContext helloContext)
        {
            try
            {
                var name = helloContext.GetInput<string>();
                return $"Hello {name}!";
            }
            catch (Exception)
            {
                return "Hello unknown!";
            }
        }
    }
}
