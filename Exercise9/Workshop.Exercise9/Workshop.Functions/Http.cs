using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace Workshop.Functions
{
    public static class Http
    {
        [FunctionName("HttpEG")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequestMessage req,
            TraceWriter log)
        {
            var input = await req.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<Event[]>(input)[0];

            return req.CreateResponse(HttpStatusCode.OK, "Hello " + model.Data.Name);
        }
        
        public class RequestModel
        {
            public string Name { get; set; }
        }

        public class Event
        {
            public string Topic { get; set; }
            public string Subject { get; set; }
            public string EventType { get; set; }
            public DateTime EventTime { get; set; }
            public int Id { get; set; }

            public RequestModel Data { get; set; }
        }
    }
}
