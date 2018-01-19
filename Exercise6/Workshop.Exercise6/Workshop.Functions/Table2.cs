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
    public static class Table2
    {
        [FunctionName("Table2")]
        [return: Table("MyTable", Connection = "StorageConnectionAppSetting")]
        public static async Task<DataEntity> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            var json = await req.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<RequestModel>(json);

            return new DataEntity()
            {
                PartitionKey = "PK",
                RowKey = Guid.NewGuid().ToString(),
                Text = model.Text
            };
        }

        public class RequestModel
        {
            public string Text { get; set; }
        }
    }
}
