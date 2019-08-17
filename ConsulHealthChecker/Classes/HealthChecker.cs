using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ConsulHealthChecker.Models;
using Newtonsoft.Json;


namespace ConsulHealthChecker.Classes
{
    public class HealthChecker
    {
        public static async Task<List<ServiceListModel>> GetServiceList(ServiceModel serviceModel)
        {
            string json;

            using (var client = new HttpClient())
            {
                var get = await client.GetAsync(
                    $"{serviceModel.ConsulServerAddress}/v1/health/service/{serviceModel.ServiceName}");
                json = await get.Content.ReadAsStringAsync();
            }
            
            var servModel = new List<ServiceListModel>();
            servModel = JsonConvert.DeserializeObject<List<ServiceListModel>>(json);

            return servModel;
        }

        public static async Task<int> CheckWebApiHealth(ServiceListModel service)
        {
            var host = $"http://{service.Service.Address}:{service.Service.Port}/api/health";
            string response;

            try
            {
                using (var client = new HttpClient())
                {
                    var get = await client.GetAsync(host);
                    response = await get.Content.ReadAsStringAsync();
                }
                
                return Convert.ToInt32(response);
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public static async Task RemoveService(ServiceModel consulService, ServiceListModel service)
        {
            HttpResponseMessage content;
            var host = $"{consulService.ConsulServerAddress}/v1/agent/service/deregister/{service.Service.ID}";
            using (var client = new HttpClient())
            {
                content  = await client.PutAsync(host, new StringContent("", Encoding.UTF8, "text/plain"));
            }

            if (content.IsSuccessStatusCode)
            {
                Console.WriteLine($"Health Check Failed for ID: {service.Service.ID}. Removed from the Service list.");
            }
            else
            {
                Console.WriteLine($"Health Check Failed for ID: {service.Service.ID}. Failed to Remove from the Service list.");
            }
            
        }
    }
}