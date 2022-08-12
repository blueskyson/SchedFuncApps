using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using FunctionApps.Schedule;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace FunctionApps.SubscriptionStatusScheduler
{
    public class SubscriptionStatusScheduler : BaseScheduler
    {
        private HttpClient _client;

        public SubscriptionStatusScheduler(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient();
        }

        [FunctionName("SubscriptionStatusScheduler")]
        public async Task Run([TimerTrigger("*/10 * * * * *")] TimerInfo myTimer, ILogger log)
        {
            string url = Environment.GetEnvironmentVariable("DemoUrl");
            var response = await _client.GetAsync(url);
            string strResult = await response.Content.ReadAsStringAsync();
            log.LogInformation($"Get at: {DateTime.Now}. Data: {strResult}");
        }
    }
}
