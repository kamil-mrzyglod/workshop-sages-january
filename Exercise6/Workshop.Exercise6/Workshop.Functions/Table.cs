using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace Workshop.Functions
{
    public static class Table
    {
        [FunctionName("Table")]
        [return: Table("MyTable", Connection = "StorageConnectionAppSetting")]
        public static async Task<DataEntity> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            return new DataEntity()
            {
                PartitionKey = "PK",
                RowKey = Guid.NewGuid().ToString(),
                Text = "Some text"
            };
        }
    }

    public class DataEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string Text { get; set; }
    }
}
