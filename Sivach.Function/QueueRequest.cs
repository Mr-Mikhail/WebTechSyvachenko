using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Sivach.Function;

[StorageAccount("sivach_STORAGE")]
public static class QueueRequest
{
    [FunctionName("QueueRequest")]
    [return: Queue("sivach")]
    public static string QueueOutput([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,  ILogger log)
    {
        var isName = req.HttpContext.Request.Query.TryGetValue("name", out var name);
        
        log.LogInformation($"C# function processed: {isName.ToString()}");

        if (!isName)
            return "Please pass a name on the query string or in the request body";
        
        return name;
    }
}
