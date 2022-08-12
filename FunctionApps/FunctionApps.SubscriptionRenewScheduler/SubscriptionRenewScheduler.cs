using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using FunctionApps.Schedule;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace FunctionApps.SubscriptionRenewScheduler
{
    public class SubscriptionRenewScheduler
    {
        private HttpClient _client;

        public SubscriptionRenewScheduler(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient();
        }

        [FunctionName("SubscriptionRenewScheduler")]
        public async Task Run([TimerTrigger("*/20 * * * * *")] TimerInfo myTimer, ILogger log)
        {
            var payload = new Payload { Name = "SubscriptionStatusScheduler", Version = "1" };
            var stringPayload = JsonConvert.SerializeObject(payload);
            var jsonContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            string url = Environment.GetEnvironmentVariable("DemoUrl");
            var response = await _client.PostAsync(url, jsonContent);
            string strResult = await response.Content.ReadAsStringAsync();
            log.LogInformation($"Post at: {DateTime.Now}. Data: {strResult}");
        }
    }
}
