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
            string url = Environment.GetEnvironmentVariable("DemoUrl");
            if (String.IsNullOrEmpty(url))
            {
                log.LogError($"Error loading environment variable \"DemoUrl\".");
                return;
            }

            try
            {
                var jsonContent = this.GetPayload();
                string result = await this.SendHttpPost(url, jsonContent);
                log.LogInformation($"Post at: {DateTime.Now}. Response: {result}");
            }
            catch (Exception e)
            {
                log.LogError($"Error. Source: {e.Source}. Message: {e.Message}");
            }
        }

        private HttpContent GetPayload()
        {
            var payload = new Payload { Name = "SubscriptionStatusScheduler", Version = "1" };
            var stringPayload = JsonConvert.SerializeObject(payload);
            var jsonContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            return jsonContent;
        }

        private async Task<string> SendHttpPost(string url, HttpContent jsonContent)
        {
            try
            {
                var response = await _client.PostAsync(url, jsonContent);
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
