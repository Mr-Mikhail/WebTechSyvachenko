using System.Threading.Tasks;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Sivach.Function;

public static class AsynchronousProcess
{
    private static readonly HttpClient HttpClient = new();
    
    [FunctionName("AsynchronousProcess")]
    public static async Task RunAsync(
        [QueueTrigger("sivach",
            Connection =
                "sivach_STORAGE")]
        string myQueueItem, ILogger log)
    {
        log.LogInformation($"{myQueueItem}");
        await HttpClient.PostAsync("https://sivach.azurewebsites.net/fortune-teller/add-fortune", JsonContent.Create(new
        FortuneModel
        {
            Name = myQueueItem,
            Date = RandomDay()
        }));
    }
    
    private static DateTime RandomDay()
    {
        var gen = new Random();
        var start = new DateTime(1850, 1, 1);
        var range = (DateTime.Today - start).Days;           
        return DateTime.Today.AddDays(gen.Next(range));
    }
}