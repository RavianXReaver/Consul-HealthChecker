using System;
using System.Threading.Tasks;
using ConsulHealthChecker.Classes;

namespace ConsulHealthChecker
{  
    class Program
    {
        static async Task Main(string[] args)
        {
            //string [] argss = {"http://localhost:8500;TestAPI;2000"};
            var host = ArgumentAnalyzer.Checker(args);

            if (host == null)
            {
                return;
            }
            
            do
            {
                var serviceList = await HealthChecker.GetServiceList(host);

                if (serviceList.Count < 1)
                {
                    Console.WriteLine($"Could not detect any service on : {host.ConsulServerAddress}");
                }
                
                foreach (var l in serviceList)
                {
                    if ((await HealthChecker.CheckWebApiHealth(l)) != 200)
                    {
                        await HealthChecker.RemoveService(host, l);
                    }
                    else
                    {
                        Console.WriteLine($"Health Check pass for ID: {l.Service.ID}");
                    }
                }
                
                await Task.Delay(TimeSpan.FromMilliseconds(host.Interval));
            } 
            while (true);
        }
    }
}