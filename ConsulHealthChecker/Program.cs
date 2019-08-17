using System;
using System.Threading.Tasks;
using ConsulHealthChecker.Classes;

namespace ConsulHealthChecker
{  
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = ArgumentAnalyzer.Checker(args);

            if (host == null)
            {
                return;
            }
            
            do
            {
                await Task.Delay(TimeSpan.FromMilliseconds(host.Interval));
                Console.WriteLine("Test");
            } 
            while (true);
        }
    }
}