using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Table;

namespace Workshop.Functions
{
    public static class Table
    {
        [FunctionName("Table")]
        [StorageAccount("FunctionLevelStorageAppSetting")]
        public static HttpResponseMessage Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "HttpTriggerCSharp/name/{name}")]HttpRequestMessage req, string name, [Table("MyTable")] CloudTable table, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            var op = TableOperation.Delete(new DataEntity()
            {
                PartitionKey = "PK",
                RowKey = name,
                ETag = "*"
            });

            table.Execute(op);

            return req.CreateResponse(HttpStatusCode.OK, "Entity with RK " + name + " deleted!");
        }

        public class DataEntity : TableEntity
        {
            public string Text { get; set; }
        }
    }
}
