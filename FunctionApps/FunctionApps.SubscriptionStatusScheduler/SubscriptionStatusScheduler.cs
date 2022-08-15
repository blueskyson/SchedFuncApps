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
        public async Task Run([TimerTrigger("%Schedule%")] TimerInfo myTimer, ILogger log)
        {
            string url = Environment.GetEnvironmentVariable("DemoUrl");
            if (String.IsNullOrEmpty(url))
            {
                log.LogError($"Error loading environment variable \"DemoUrl\".");
                return;
            }

            try
            {
                string result = await this.SendHttpGet(url);
                log.LogInformation($"Get at: {DateTime.Now}. Response: {result}");
            }
            catch (Exception e)
            {
                log.LogError($"Error. Source: {e.Source}. Message: {e.Message}");
            }
        }

        private async Task<string> SendHttpGet(string url)
        {
            try
            {
                var response = await _client.GetAsync(url);
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
